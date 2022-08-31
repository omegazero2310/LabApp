using System;
using System.Collections.Generic;
using System.Text;

namespace CommonClass.Models.Request
{
    /// <summary>
    /// Lớp chứa thông tin cá nhân của tài khoản đăng nhập
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 31/08/2022 created
    /// </Modified>
    public class UserInfo
    {
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }
    }
}
