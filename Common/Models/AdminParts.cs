using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonClass.Models
{
    /// <summary>
    /// Bảng lưu danh sách chức danh
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    [Table("Admin.Parts")]
    public class AdminParts: IBaseEntity
    {
        /// <summary>
        /// Mã chức danh
        /// </summary>
        /// <value>
        /// The part identifier.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Key]
        [Column(TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartID { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        /// <value>
        /// The name of the part.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Required(AllowEmptyStrings = false)]
        public string PartName { get; set; }

        /// <summary>
        /// Ngày giờ tạo
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Required]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Ngày giờ chỉnh sửa
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Required]
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// <value>
        /// The user created.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Required]
        public string UserCreated { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        /// <value>
        /// The user modified.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        [Required]
        public string UserModified { get; set; }
    }
}
