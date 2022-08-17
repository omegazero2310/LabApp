using System.ComponentModel.DataAnnotations.Schema;

namespace CommonClass.Models
{
    /// <summary>Save User Login</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 16/08/2022 created
    /// </Modified>
    [Table("UserLogin")]
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the hashed password.
        /// </summary>
        /// <value>
        /// The hashed password.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string HashedPassword { get; set; }
        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string Salt { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is reset password.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is reset password; otherwise, <c>false</c>.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public bool IsResetPassword { get; set; }
        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public DateTime DateModified { get; set; }
    }
}
