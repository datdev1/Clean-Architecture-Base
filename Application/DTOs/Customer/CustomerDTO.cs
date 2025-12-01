using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Customer
{
    public class CustomerDTO
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }    // 0 -> Female, 1 -> Male
        public string Address { get; set; }
    }
}
