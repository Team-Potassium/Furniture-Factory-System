namespace FurnitureFactory.Logic.Exporters
{
    using System.Data.Entity;
    using System.IO;
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
            PdfPTable table = this.CreateShopReportsTable();
            var collection = this.context.Set<Models.Series>();
            table.AddCell(this.CreateHeaderCell("ID"));
            table.AddCell(this.CreateHeaderCell("Name"));
            foreach (var coll in collection)
            {
                table.AddCell(coll.Id.ToString());
                table.AddCell(coll.Name);
            }

            document.Open();
            document.Add(table);
            document.Close();
        }

        private PdfPTable CreateShopReportsTable()
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.LockedWidth = false;
            float[] widths = new float[] { 3f, 3f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;
            return table;
        }

        public PdfPCell CreateHeaderCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content));
            cell.BackgroundColor = BaseColor.GRAY;
            return cell;
        }
    }
}
