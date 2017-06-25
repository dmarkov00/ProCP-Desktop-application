using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace Models
{
    public class Client : IApiCallResult
    {
        private string name;
        private string phone;
        private string email;
        private string address;
        private List<Load> loads;
        private string id;
        private string company;

        
        public Client(string name, string phone, string email, string address, string company)
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
            this.Address = address;
            this.company = company;
            loads = new List<Load>();
        }

        [JsonProperty("company")]
        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
            }
        }

        [JsonProperty("id")]
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [JsonProperty("phone")]
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        [JsonProperty("email")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        [JsonProperty("address")]
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

    }
}
