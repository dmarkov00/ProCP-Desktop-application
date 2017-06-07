
namespace Models
{
    public class Company
    {
        private string companyName;
        private Address address;

        public Company(string companyName, Address address)
        {
            this.companyName = companyName;
            this.address = address;
        }

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
