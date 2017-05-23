using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoogleApiIntegration
{
    public class GoogleAPI
    {
        public List<string> publicNames;
        private List<string> privateNames;
        public GoogleAPI()
        {
            publicNames = new List<string>();
            publicNames.Add("Moscow");
            publicNames.Add("Seres");
            publicNames.Add("Sofia");
            publicNames.Add("Berlin");
            publicNames.Add("Munich");
            privateNames = new List<string>();
            privateNames.Add("Moscow");
            privateNames.Add("Seres");
            privateNames.Add("Sofia");
            privateNames.Add("Berlin");
            privateNames.Add("Munich");
        }
        //private string apiKEY;
        //string request = @"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=Washington,DC&destinations=New+York+City,NY&key=YOUR_API_KEY";
        public string calculatedistance(int indexStart, int indexDest)
        {
            string start = privateNames[indexStart];
            string end = privateNames[indexDest];
            string html = string.Empty;
            string url = @"https://maps.googleapis.com/maps/api/distancematrix/xml?origins="
            + start + "&destinations=" + end + "&key=AIzaSyB6R-ta400Xr4tVfEAqk3h7E1iUHB2rVOE";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }


            XDocument doc = XDocument.Load(url);
            var authors = doc.Descendants("distance").Descendants("text");
            foreach (var author in authors)
            {
                return author.Value.Remove(author.Value.Length - 3);
            }

            return html;
        }

        public double calculateFuelConsumption(string distance, double consumptionPerHundredKM)
        {
            double fuelCons = consumptionPerHundredKM;
            double dist = 0;
            var count = distance.Count(x => x == ',');
            if (Double.TryParse(distance, out dist))
            {
                if (count != 0)
                {
                    dist = dist * (10 ^ count);
                }
                else
                {
                    dist = dist / 100;
                }
                fuelCons = dist * fuelCons;
            }
            return fuelCons;
        }
    }
}
