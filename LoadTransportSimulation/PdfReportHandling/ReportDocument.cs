using MigraDoc.DocumentObjectModel;

namespace PdfReportHandling
{
    public class ReportDocument
    {
        public static Document CreateDocument()
        {
            // Create a new MigraDoc document
            Document document = new Document();
            document.Info.Title = "Transport simulation report";
            //document.Info.Subject = "Demonstrates an excerpt of the capabilities of MigraDoc.";
            //document.Info.Author = "Stefan Lange";

            Styles.DefineStyles(document);

            Cover.DefineCover(document);

            DefineContentSection(document);

            //Paragraphs.DefineParagraphs(document);
            //Tables.DefineTables(document);
            //Charts.DefineCharts(document);

            return document;
        }

        /// <summary>
        /// Defines page setup, headers, and footers.
        /// </summary>
        static void DefineContentSection(Document document)
        {
            Section section = document.AddSection();
            section.PageSetup.OddAndEvenPagesHeaderFooter = false;
            section.PageSetup.StartingNumber = 1;

            //HeaderFooter header = section.Headers.FirstPage;
            //header.AddParagraph("Report result");

            //header = section.Headers.EvenPage;
            //header.AddParagraph("Report result");

            // Create a paragraph with centered page number. See definition of style "Footer".
            Paragraph paragraph = new Paragraph();
            paragraph.AddTab();
            paragraph.AddPageField();

            // Add paragraph to footer for odd pages.
            section.Footers.Primary.Add(paragraph);
            // Add clone of paragraph to footer for odd pages. Cloning is necessary because an object must
            // not belong to more than one other object. If you forget cloning an exception is thrown.
            section.Footers.EvenPage.Add(paragraph.Clone());
        }
    }
}
