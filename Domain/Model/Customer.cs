using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public bool Gender { get; set; }    // 0 -> Female, 1 -> Male
        public string Address { get; set; }
    }
}
