using Common;
using Newtonsoft.Json;

namespace Models
{
    public class User: IApiCallResult
    {

        public User(string name, string email, string phone, string token)
        {
            this.Name = name;
            this.email = email;
            this.phone = phone;
            this.token = token;
        }
        private string name;
        private string email;
        private string phone;
        private string password;
        private Company company;
        private string token;

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


        internal Company Company
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
        [JsonProperty("api_token")]
        public string Token
        {
            get
            {
                return token;
            }

            set
            {
                token = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

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





    }
}
