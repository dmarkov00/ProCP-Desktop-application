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

            Styles.DefineStyles(document);

            Cover.DefineCover(document);

            ReportTable.DefineTables(document);
     
            return document;
        }

    }
}
