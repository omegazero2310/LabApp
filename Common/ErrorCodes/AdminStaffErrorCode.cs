using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.ErrorCodes
{
    /// <summary>
    /// thông báo lỗi trả về khi lưu một dòng Admin.Staff
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public static class AdminStaffErrorCode
    {
        public const string DUPLICATE_EMAIL = "MSG_STAFF_DUPLICATE_EMAIL";
        public const string DUPLICATE_PHONE_NUMBER = "MSG_STAFF_PHONE_NUMBER";
    }
}
