using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonClass.Models
{
    /// <summary>
    /// Inteface gốc để thêm ngày giờ tạo, chỉnh sửa, người tạo, người sửa cho các bảng
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 31/08/2022 created
    /// </Modified>
    public interface IBaseEntity
    {
        [Required]
        DateTime DateCreated { get; set; }
        [Required]
        DateTime DateModified { get; set; }
        [Required]
        string UserCreated { get; set; }
        [Required]
        string UserModified { get; set; }
    }
}
