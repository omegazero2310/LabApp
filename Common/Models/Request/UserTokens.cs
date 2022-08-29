using System;

namespace CommonClass.Models.Request
{
    /// <summary>
    /// Mẫu chứa thông tin token JWT
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public class UserTokens
    {
        public string Token
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public int Id { get; set; }
        public TimeSpan Validaty
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get;
            set;
        }
        public string EmailId
        {
            get;
            set;
        }
        public Guid GuidId
        {
            get;
            set;
        }
        public DateTime ExpiredTime
        {
            get;
            set;
        }
    }
}