using System;
using System.Collections.Generic;
using System.Text;

namespace CommonClass.Models.Request
{
    public class UserInfo
    {
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }
    }
}
