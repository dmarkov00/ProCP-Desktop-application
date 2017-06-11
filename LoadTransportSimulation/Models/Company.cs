using Common;
using Newtonsoft.Json;

namespace Models
{
    public class Company : IApiCallResult
    {
        private string id;
        private string companyName;
        private string address;

        public Company(string companyName, string address)
        {
            this.companyName = companyName;
            this.address = address;
        }

        [JsonProperty("companyName")]
        public string CompanyName
        {
            get
            {
                return companyName;
            }

            set
            {
                companyName = value;
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

    }
}
