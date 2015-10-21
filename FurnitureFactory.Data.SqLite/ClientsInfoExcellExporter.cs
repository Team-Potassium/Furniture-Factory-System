namespace FurnitureFactory.Data.SqLite
{
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using Bytescout.Spreadsheet;
    using Bytescout.Spreadsheet.Constants;

    public class ClientsInfoSqLiteExporter
    {
        private string documentPath = "../../../Reports/Excell/CompaniesInfo.xls";
        private SqLiteData sqliteData;

        public ClientsInfoSqLiteExporter()
        {
            this.sqliteData = new SqLiteData();
        }

        public ClientsInfoSqLiteExporter Export()
        {
            
            var clientsInfo = this.sqliteData.GetAll();

            Spreadsheet document = new Spreadsheet();

            // add new worksheet
            Worksheet sheet = document.Workbook.Worksheets.Add("Client contacts");

            // headers to indicate purpose of the column
            sheet.Cell("A1").Value = "Company Name";
            sheet.Cell("B1").Value = "Email";
            sheet.Cell("C1").Value = "Address";
            sheet.Cell("D1").Value = "Iban";

            Color headerColor = Color.FromArgb(80, 80, 80);
            sheet.Rows[0].Height = 35;

            for (int i = 0; i < 4; i++)
            {
                sheet.Cell(0, i).FillPattern = PatternStyle.Solid;
                sheet.Cell(0, i).FillPatternForeColor = headerColor;
                sheet.Cell(0, i).FontColor = Color.White;
                sheet.Cell(0, i).Font = new Font("Arial", 14, FontStyle.Bold);
                sheet.Cell(0,i).AlignmentHorizontal = AlignmentHorizontal.Centered;
                sheet.Cell(0,i).AlignmentVertical = AlignmentVertical.Centered;
                sheet.Columns[i].Width = 250;
            }
           
            // delete output file if exists already
            if (File.Exists(documentPath))
            {
                File.Delete(documentPath);
            }

            var row = 1;
            foreach (var entity in clientsInfo)
            {
                sheet.Cell(row, 0).Value = entity.Name;
                sheet.Cell(row, 1).Value = entity.Email;
                sheet.Cell(row, 2).Value = entity.Address;
                sheet.Cell(row, 3).Value = entity.Iban;
                row++;
            }


            document.SaveAs(documentPath);

            // Close Spreadsheet
            document.Close();
            return this;
        }


        public void Open()
        {
            // open generated XLS document in default program
          //  Process.Start(documentPath);
        }
    }
}
