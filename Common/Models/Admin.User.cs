using CommonClass.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonClass.Models
{

    /// <summary>Lưu thông tin đăng nhập của người dùng</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    [Table("Admin.Users")]
    public class AdminUser
    {
        /// <summary>
        /// ID đăng nhập
        /// </summary>
        /// <value>
        /// ID đăng nhập được cấp
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        [Required(AllowEmptyStrings = false)]
        public string UserID { get; set; }

        /// <summary>
        /// Mật khẩu (đã được qua hàm băm)
        /// </summary>
        /// <value>
        /// 
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Column(TypeName = "nvarchar(64)")]
        [Required(AllowEmptyStrings = false)]
        public string HashedPassword { get; set; }

        /// <summary>
        /// Salt của mật khẩu đã qua hàm băm
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Column(TypeName = "nvarchar(64)")]
        [Required(AllowEmptyStrings = false)]
        public string Salt { get; set; }

        /// <summary>
        /// đặt giá trị tài khoản cần đổi lại mật khẩu
        /// </summary>
        /// <value>
        ///   <c>true</c> nếu tài khoản cần đổi mật khẩu; còn lại, <c>false</c>.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [DefaultValue(false)]
        [Required]
        public bool IsResetPassword { get; set; }

        /// <summary>
        /// Lần cuối đổi mật khẩu của nhân viên
        /// </summary>
        /// <value>
        /// 
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Required]
        public DateTime DateModified { get; set; }

        [Required]
        [DefaultValue(AccountStatusOptions.Normal)]
        public AccountStatusOptions AccountStatus { get; set; }
        public int? ID { get; set; }
        [ForeignKey("ID")]
        public virtual AdminStaff Staff { get; set; }
    }
}
