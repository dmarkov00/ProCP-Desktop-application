using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address
    {
        private int houseNr;
        private string street;
        private string city;
        private string country;
        private string zipcode;

        public int HouseNr
        {
            get
            {
                return houseNr;
            }

            set
            {
                houseNr = value;
            }
        }

        public string Street
        {
            get
            {
                return street;
            }

            set
            {
                street = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
            }
        }

        public string Zipcode
        {
            get
            {
                return zipcode;
            }

            set
            {
                zipcode = value;
            }
        }

        public Address(int houseNr, string street, string city, string country, string zipcode)
        {
            this.houseNr = houseNr;
            this.street = street;
            this.city = city;
            this.country = country;
            this.zipcode = zipcode;
        }
    }
}
