using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonClass.Models
{
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
