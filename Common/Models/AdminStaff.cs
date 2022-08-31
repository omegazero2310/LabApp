using CommonClass.Enums;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonClass.Models
{

    /// <summary>Bảng lưu thông tin nhân viên</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    [Table("Admin.Staffs")]
    public class AdminStaff
    {
        /// <summary>
        /// ID đăng nhập hệ thống
        /// </summary>
        /// <value>
        /// ID đăng nhập
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Key]
        [Column(TypeName = "int", Order =0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// <value>
        /// tên của nhân viên
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = false)]
        [Column(TypeName = "nvarchar(50)",Order =1)]
        public string StaffName { get; set; }

        /// <summary>
        /// Giới tính của nhân viên
        /// </summary>
        /// 0 - Nam, 1 - Nữ, 2 - Khác
        /// The gender.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Required]
        [Column(Order =2)]
        public GenderOptions Gender { get; set; }

        /// <summary>
        /// Email của nhân viên
        /// </summary>
        /// <value>
        /// địa chỉ Email hệ thống
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")]
        [Required]
        [Column(Order =3)]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại của nhân viên
        /// </summary>
        /// <value>
        /// số điện thoại hợp lệ
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Phone]
        [Required]
        [Column(Order = 4)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        /// <value>
        /// địa chỉ thường chú
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Required]
        [Column(Order = 5)]
        public string Address { get; set; }


        /// <summary>
        /// ID hình ảnh của nhân viên trên API
        /// </summary>
        /// <value>
        /// ảnh đại diện của nhân viên
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [Column(TypeName = "nvarchar(50)", Order =6)]
        public string ProfileImage { get; set; }

        /// <summary>
        /// Chức danh của nhân viên
        /// </summary>
        /// <value>
        /// Chức danh hiện tại
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [DefaultValue(1)]
        [Column(Order =7)]
        public int PartID { get; set; }
        [ForeignKey("PartID")]
        [JsonIgnore]
        public virtual AdminParts Parts { get; set; }

        [Required]
        [Column(Order = 8)]
        public DateTime DateCreated { get; set; }
        [Required]
        [Column(Order = 9)]
        public DateTime DateModified { get; set; }
        [Required]
        [Column(Order = 10)]
        public string UserCreated { get; set; }
        [Required]
        [Column(Order = 11)]
        public string UserModified { get; set; }


        [NotMapped]
        public byte[] ProfilePicture { get; set; }
        [NotMapped]
        public DateTime CurrentTime { get; set; } = DateTime.Now;
        [NotMapped]
        public string PositionName { get; set; }
    }
}
