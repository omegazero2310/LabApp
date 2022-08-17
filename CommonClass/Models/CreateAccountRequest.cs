using CommonClass.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.Models
{
    public class CreateAccountRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public GenderOption Gender { get; set; }
        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
