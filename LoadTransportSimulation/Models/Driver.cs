using System;
using Common;
using Newtonsoft.Json;

namespace Models
{
    public class Driver : IApiCallResult
    {
        private string id;
        private string firstName;
        private string lastName;
        private string phone;
        private string email;
        private bool isBusy;

        public Driver(string firstName, string lastName, string phone, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.email = email;
        }

        [JsonProperty("fName")]
        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        [JsonProperty("lName")]
        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

     
        [JsonProperty("phoneNbr")]
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

       

        [JsonProperty("taken")]
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }

            set
            {
                isBusy = value;
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

        public override string ToString()
        {
            return FirstName + " " + LastName ;
        }
    }
}
