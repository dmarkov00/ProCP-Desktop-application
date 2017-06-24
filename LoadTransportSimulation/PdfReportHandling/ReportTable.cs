using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace PdfReportHandling
{
    public class ReportTable
    {
        public static void DefineTables(Document document)
        {
            Paragraph paragraph = document.LastSection.AddParagraph("Report result");
            paragraph.Format.Font.Size = 14;

            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.SpaceAfter = "3cm";


            GenerateReportTable(document);
        }

        public static void GenerateReportTable(Document document)
        {
            //document.LastSection.AddParagraph("Simple Tables", "Heading2");

            Table table = new Table();
            table.Borders.Width = 0.75;

            Column column = table.AddColumn(Unit.FromCentimeter(2));
            column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromCentimeter(5));

            Row row = table.AddRow();
            row.Shading.Color = Colors.PaleGoldenrod;
            Cell cell = row.Cells[0];
            cell.AddParagraph("Itemus");
            cell = row.Cells[1];
            cell.AddParagraph("Descriptum");

            row = table.AddRow();
            cell = row.Cells[0];
            cell.AddParagraph("1");
            cell = row.Cells[1];
            cell.AddParagraph("Andigna cons nonsectem accummo diamet nis diat.");

            row = table.AddRow();
            cell = row.Cells[0];
            cell.AddParagraph("2");
            cell = row.Cells[1];
            cell.AddParagraph("Loboreet autpat, quis adigna conse dipit la consed exeril et utpatetuer autat, voloboreet, consequamet ilit nos aut in henit ullam, sim doloreratis dolobore tat, venim quissequat. " +
          "Nisci tat laor ametumsan vulla feuisim ing eliquisi tatum autat, velenisit iustionsed tis dunt exerostrud dolore verae.");

            table.SetEdge(0, 0, 2, 3, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);

            document.LastSection.Add(table);
        }   
        
    }
}
