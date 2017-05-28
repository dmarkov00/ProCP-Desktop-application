using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common.Enumerations;

namespace GoogleApiIntegration
{
    public class GoogleAPI
    {
        public List<string> publicNames;
        private List<string> privateNames;

        public GoogleAPI()
        {
            //Sorry for the change but it looks better this way
            publicNames = new List<string> { "Moscow" , "Seres", "Sofia", "Berlin" ,"Munich" };
            //publicNames.Add("Moscow");
            //publicNames.Add("Seres");
            //publicNames.Add("Sofia");
            //publicNames.Add("Berlin");
            //publicNames.Add("Munich");
            privateNames = new List<string> { "Moscow", "Seres", "Sofia", "Berlin", "Munich" };
            //privateNames.Add("Moscow");
            //privateNames.Add("Seres");
            //privateNames.Add("Sofia");
            //privateNames.Add("Berlin");
            //privateNames.Add("Munich");
        }
        //private string apiKEY;
        //string request = @"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=Washington,DC&destinations=New+York+City,NY&key=YOUR_API_KEY";
        public int calculatedistance(int indexStart, int indexDest)
        {
            //i exchanged the list for the enumeration and consume "value" instead of "text"
            City startcity = (City)indexStart;
            City endCity = (City)indexDest;

            string start = startcity.ToString();
            string end = endCity.ToString();
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
            var authors = doc.Descendants("distance").Descendants("value");
            foreach (var author in authors)
            {
                int dist = Convert.ToInt32(author.Value);
                return dist / 1000;
            }

            return 0;
        }

        public string calculatetime(int indexStart, int indexDest)
        {
            City startcity = (City)indexStart;
            City endCity = (City)indexDest;

            string start = startcity.ToString();
            string end = endCity.ToString();
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
            var authors = doc.Descendants("duration").Descendants("value");
            foreach (var author in authors)
            {
                return author.Value;
            }

            return html;
        }

        public double calculateFuelConsumption(int distance, double consumptionPerHundredKM)
        {
            double fuelCons = consumptionPerHundredKM;
            double dist = distance;


            //var count = distance.Count(x => x == ',');
            //if (Double.TryParse(distance, out dist))
            //{
            //    if (count != 0)
            //    {
            //        dist = dist * (10 ^ count);
            //    }
            //    else
            //    {
            //        dist = dist / 100;
            //    }
            //    fuelCons = dist * fuelCons;
            //}
            fuelCons = dist * (fuelCons/100);
            return fuelCons;
        }
    }
}
