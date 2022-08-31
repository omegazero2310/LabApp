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
    public class AdminUser : IBaseEntity
    {
        /// <summary>
        /// GUID của tài khoản
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order =0)]
        [Required]
        public Guid Id { get; set; }
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
        [Column(TypeName = "nvarchar(50)", Order =1)]
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

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
        [Column(TypeName = "nvarchar(64)", Order =2)]
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
        [Column(TypeName = "nvarchar(64)", Order =3)]
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
        [Column(Order = 4)]
        public bool IsResetPassword { get; set; }

        /// <summary>
        /// ngày tạo tài khoản
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Required]
        [Column(Order = 5)]
        public DateTime DateCreated { get; set; }
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
        [Column(Order = 6)]
        public DateTime DateModified { get; set; }
        [Required]
        [Column(Order = 7)]
        public string UserCreated { get; set; }
        /// <summary>
        /// người chỉnh sửa tài khoản
        /// </summary>
        /// <value>
        /// The user modified.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Required]
        [Column(Order = 8)]
        public string UserModified { get; set; }

        /// <summary>
        /// trạng thái của tài khoản
        /// </summary>
        /// <value>
        /// The account status.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Required]
        [Column(TypeName ="varchar(10)",Order = 9)]
        [DefaultValue(AccountStatusOptions.Normal)]
        public AccountStatusOptions AccountStatus { get; set; }
        [Column(Order = 10)]
        public string ProfilePictureName { get; set; }
        /// <summary>
        /// Số điện thoại đăng kí với tài khoản
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Column(TypeName = "varchar(15)",Order = 11)]
        [Required]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Tên ảnh đại diện
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Column(TypeName = "nvarchar(50)", Order = 12)]
        [Required]
        public string DisplayName { get; set; }

        [NotMapped]
        public byte[] ProfileImg { get; set; }
    }
}
