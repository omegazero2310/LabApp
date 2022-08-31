using CommonClass.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.Models.Request
{

    /// <summary>Mẫu Request tạo tài khoản đăng nhập mới</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 17/08/2022 created
    /// </Modified>
    public class CreateAccountRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
