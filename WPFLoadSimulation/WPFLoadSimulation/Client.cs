using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLoadSimulation
{
    class Client
    {
        //private string companyName;
        private string firstName;
        private string lastName;
        private string phone;
        private string email;
        private Address address;
        private List<Load> loads;

        public Client(string fname, string lname, string phone, string email, Address address) 
        {
            this.firstName = fname;
            this.lastName = lname;
            this.phone = phone;
            this.email = email;
            this.address = address;
            loads = new List<Load>();
        }

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

        public Address Address
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
