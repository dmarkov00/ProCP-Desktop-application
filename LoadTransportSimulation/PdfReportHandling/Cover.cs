using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using PdfReportHandling.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfReportHandling
{
    public class Cover
    {
        /// <summary>
        /// Defines the cover page.
        /// </summary>
        public static void DefineCover(Document document)
        {
            Section section = document.AddSection();

            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.SpaceAfter = "3cm";
                  
            Image image = section.AddImage("../../../PdfReportHandling/Images/LoadTransportQuality.png");

            image.Width = "10cm";

            paragraph = section.AddParagraph("Transport simulation report:");
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Format.SpaceAfter = "3cm";

            paragraph = section.AddParagraph("Time created: ");
            paragraph.AddDateField();
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Format.SpaceAfter = "3cm";

        }
    }
}

