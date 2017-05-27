using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Models
{
    public class Client : IApiCallResult
    {
        //private string companyName;
        private string name;
        private string phone;
        private string email;
        private string address;
        private List<Load> loads;
        private string id;

        public Client(string name, string phone, string email, string address)
        {
            this.Name = name;
            this.phone = phone;
            this.email = email;
            this.address = address;
            loads = new List<Load>();
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
