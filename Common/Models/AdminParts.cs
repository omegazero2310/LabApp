using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonClass.Models
{
    [Table("Admin.Parts")]
    public class AdminParts
    {
        [Key]
        [Column(TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PartName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public string UserCreated { get; set; }
        [Required]
        public string UserModified { get; set; }
    }
}
