namespace FurnitureFactory.Logic.Exporters
{
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using FurnitureFactory.Models;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PdfExporter
    {
        private DbContext context;

        public PdfExporter(DbContext context)
        {
            this.context = context;
        }

        public void GeneratePdf()
        {
            var document = new Document();
            PdfWriter.GetInstance(document, new FileStream("Reports.pdf", FileMode.Create));
            document.Open();
            document.Add(this.CreateProductQuantityPriceTable());
            document.Add(this.GenerateProductTypeQuantityTable());
            document.Add(this.GenerateMaterialQuantityPriceTable());
            document.Close();
        }

        private PdfPCell CreateHeaderCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content));
            cell.BackgroundColor = BaseColor.GRAY;
            return cell;
        }

        private PdfPTable CreateTable(string title, string[] columns)
        {
            PdfPTable table = new PdfPTable(columns.Length);
            table.WidthPercentage = 100;
            table.LockedWidth = false;
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 20f;

            table.AddCell(this.CreateTableTitle(title, columns.Length));
            foreach (var column in columns)
            {
                table.AddCell(column);
            }

            return table;
        }

        private PdfPTable CreateProductQuantityPriceTable()
        {
            PdfPTable table = this.CreateTable("Sales by products", new string[] { "Catalog Number", "Quantity", "Cost" });
            var result = this.context.Set<Order>().AsQueryable().Include("Product").GroupBy(o => o.Product).Select(g =>
                    new
                    {
                        CatalogNumber = g.Key.CatalogNumber,
                        Quantity = g.Sum(o => o.Quantity),
                        Cost = g.Sum(o => o.Price),
                    });
            foreach (var entry in result)
            {
                table.AddCell(entry.CatalogNumber.ToString());
                table.AddCell(entry.Quantity.ToString());
                table.AddCell(entry.Cost.ToString("F2"));
            }

            table.AddCell("Total");
            table.AddCell(result.Sum(g => g.Quantity).ToString());
            table.AddCell(result.Sum(g => g.Cost).ToString("F2"));

            return table;
        }

        private PdfPTable GenerateProductTypeQuantityTable()
        {
            PdfPTable table = this.CreateTable("Sales by categories", new string[] { "Category", "Quantity", "Cost" });
            var result = this.context.Set<Order>().AsQueryable().Include("Product").Include("FurnitureType").GroupBy(o => o.Product.FurnitureType).Select(g =>
                    new
                    {
                        Category = g.Key.Name,
                        Quantity = g.Sum(o => o.Quantity),
                        Cost = g.Sum(o => o.Price),
                    });
            foreach (var entry in result)
            {
                table.AddCell(entry.Category.ToString());
                table.AddCell(entry.Quantity.ToString());
                table.AddCell(entry.Cost.ToString("F2"));
            }

            table.AddCell("Total");
            table.AddCell(result.Sum(g => g.Quantity).ToString());
            table.AddCell(result.Sum(g => g.Cost).ToString("F2"));

            return table;
        }

        private PdfPTable GenerateMaterialQuantityPriceTable()
        {
            PdfPTable table = this.CreateTable("Expenses by materials", new string[] { "Material", "Quantity", "Cost" });
            var result = this.context.Set<Order>()
                .AsQueryable().Include("Product").Include("Materials").Include("ProductsMaterialsQuantity")
                .SelectMany(o => this.context.Set<ProductsMaterialsQuantity>().Where(entry => entry.ProductId == o.ProductId).Select(entry => new
                {
                    Material = entry.Material,
                    Quantity = entry.Quantity * o.Quantity,
                }))
                .GroupBy(m => m.Material).Select(g =>
                    new
                    {
                        Material = g.Key.Name,
                        Quantity = g.Sum(m => m.Quantity),
                        Cost = g.Key.PricePerUnit * g.Sum(m => m.Quantity),
                    });
            foreach (var entry in result)
            {
                table.AddCell(entry.Material.ToString());
                table.AddCell(entry.Quantity.ToString("F2"));
                table.AddCell(entry.Cost.ToString("F2"));
            }

            PdfPCell totalCell = new PdfPCell(new Phrase("Total"));
            totalCell.Colspan = table.NumberOfColumns - 1;
            table.AddCell(totalCell);
            table.AddCell(result.Sum(g => g.Cost).ToString("F2"));

            return table;
        }

        private PdfPCell CreateTableTitle(string title, int size)
        {
            PdfPCell cell = this.CreateHeaderCell(title);
            cell.Colspan = size;
            cell.HorizontalAlignment = 1;
            cell.BackgroundColor = BaseColor.GRAY;
            return cell;
        }
    }
}
