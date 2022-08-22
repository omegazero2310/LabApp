using CommonClass.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.Models.Request
{

    /// <summary>Request class to create new account</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 17/08/2022 created
    /// </Modified>
    public class CreateAccountRequest
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public GenderOptions Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
