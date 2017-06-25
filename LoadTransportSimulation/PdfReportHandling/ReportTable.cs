using Common.Enumerations;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Models;
using System.Collections.Generic;

namespace PdfReportHandling
{
    public class ReportTable
    {
        private static List<Route> routesList;
        public static void DefineTables(Document document)
        {
            Paragraph paragraph = document.LastSection.AddParagraph("Report result");
            paragraph.Format.Font.Size = 14;

            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.SpaceAfter = "3cm";
            paragraph.Format.Font.Bold = true;

            GenerateReportTable(document);        
        }

        public static void GenerateReportTable(Document document)
        {
            //document.LastSection.AddParagraph("Simple Tables", "Heading2");
            routesList = ReportData.GetRoutesList();


            for (int i = 0; i < routesList.Count; i++)
            {
                Paragraph paragraph = document.LastSection.AddParagraph("Route from: " + (City)routesList[i].StartLocationId + " to: " + (City)routesList[i].EndLocationId);
                paragraph.Format.Font.Size = 11;
                paragraph.Format.Font.Color = Colors.Black;
                paragraph.Format.Font.Italic = true;
                paragraph.Format.Font.Bold = true;
                ////////////////////////////////////////////////////

                Table table = new Table();
                table.Borders.Width = 0.1;
                table.Rows.Height = 15;

                Column column = table.AddColumn(Unit.FromCentimeter(8));
     
                table.AddColumn(Unit.FromCentimeter(8));

                Row row = table.AddRow();
                row.Shading.Color = Colors.Green;
                Cell cell = row.Cells[0];
                cell.AddParagraph("Type of data");
  
                cell = row.Cells[1];
                cell.AddParagraph("Result");
               //////////////////////////
                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Start time");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].StartTime.ToString());
      

                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("End time");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].EndTime.ToString());
           

                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Start time");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].StartTime.ToString());
       

                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Estimation driving time");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].EstTimeDrivingMin + " MIN");
 

                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Estimation distance");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].EstDistanceKm + " KM");


                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Estimation fuel consumption");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].EstFuelConsumptionLiters + " L");
 

                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Estimation fuel cost");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].EstFuelCost + " $");


                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Total estimated salary");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].TotalEstimatedSalary + " $");

                ////////////////////////////////////////////////////
                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Actual driving time");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].ActTimeDrivingMin + " MIN");


                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Actual distance");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].ActDistanceKm + " KM");

                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Actual fuel consumption");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].ActFuelConsumptionLiters + " L");

                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Actual fuel cost");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].ActFuelCost + " $");

                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph("Total actual salary");
                cell = row.Cells[1];
                cell.AddParagraph(routesList[i].TotalActualSalary + " $");


                table.SetEdge(0, 0, 0, 0, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);

                document.LastSection.Add(table);
            }
          
        }
       

    }
}
