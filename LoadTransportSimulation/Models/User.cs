using Common;
namespace Models
{
    public class User
    {
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private string password;
        private Company company;

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

        public string Password { get => password; set => password = value; }

        public User(string firstName, string lastName, string email, string phone, Company company)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
            this.company = company;
        }
    }
}
