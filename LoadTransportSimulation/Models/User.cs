using Common;
using System;
using Newtonsoft.Json;
using System.Windows;
using System.Collections.Generic;

namespace Models
{
    public class User: IApiCallResult
    {
        private static volatile User instance;
        private static object syncRoot = new Object();

        public static User GetInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }
        
        public static User Create(User u)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = u;
            }
            return instance;
        }

        private string name;
        private string email;
        private string phone;
        private string password;
        private Company company;
        private string token;

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

        [JsonProperty("password")]
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
    }
}
