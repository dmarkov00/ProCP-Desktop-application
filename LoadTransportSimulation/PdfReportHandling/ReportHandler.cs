using Microsoft.Win32;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Diagnostics;

namespace PdfReportHandling
{
    public class ReportHandler
    {
        private static Document document;
        private static string filename;
        public static void GenerateReport()
        {
            document = ReportDocument.CreateDocument();

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = document;

            renderer.RenderDocument();

            // Save the document...

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Transportation report"; // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension 

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                filename = dlg.FileName;
            }
            renderer.PdfDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}
