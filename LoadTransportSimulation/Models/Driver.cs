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

        //Needs further attention as we need only date not datetime
        private DateTime birthDate;
        private string phone;
        private string email;
        private Truck currentTruck;
        private bool isBusy;
        private bool isInCompany;

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

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }

            set
            {
                birthDate = value;
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

        internal Truck CurrentTruck
        {
            get
            {
                return currentTruck;
            }

            set
            {
                currentTruck = value;
            }
        }

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

        public bool IsInCompany
        {
            get
            {
                return isInCompany;
            }

            set
            {
                isInCompany = value;
            }
        }

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

        public Driver(string firstName, string lastName, DateTime birthDate, string phone, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.phone = phone;
            this.email = email;
        }
    }
}
