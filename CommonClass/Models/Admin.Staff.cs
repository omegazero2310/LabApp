﻿using CommonClass.Enums;
using DataAnnotationsExtensions;
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
    [Table("Admin.Staff")]
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
        [Column(TypeName = "nvarchar(50)")]
        [Required(AllowEmptyStrings = false)]
        public string UserID { get; set; }

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
        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; }

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
        public GenderOption Gender { get; set; }

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
        [Email]
        [Required]
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
        public string Address { get; set; }

        /// <summary>
        /// Phòng ban của nhân viên
        /// </summary>
        /// <value>
        /// Phòng ban hiện tại
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        [DefaultValue("")]
        public string DepartmentName { get; set; }

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
        [Required]
        public string PositionID { get; set; }
    }
}
