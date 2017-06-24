using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Diagnostics;

namespace PdfReportHandling
{
    public class ReportHandler
    {
        private static Document document;

        public static void GenerateReport(string routesString)
        {
            document = ReportDocument.CreateDocument();
            //string ddl = MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToString(document);
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = document;

            renderer.RenderDocument();

            // Save the document...


            string filename = "HelloMigraDoc.pdf";
            renderer.PdfDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}
