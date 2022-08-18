﻿using System.ComponentModel.DataAnnotations;

namespace CommonClass.Models.Request
{
    /// <summary>Request class for login</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 17/08/2022 created
    /// </Modified>
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}