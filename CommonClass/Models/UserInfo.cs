using CommonClass.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonClass.Models
{

    /// <summary>Save user Personal Info</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 16/08/2022 created
    /// </Modified>
    [Table("UserInfo")]
    public class UserInfo
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
        /// Gets or sets the User first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the User last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 17/08/2022 created
        /// </Modified>
        public GenderOption Gender { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the status. 1-Using; 2-Suppended; 3-Banned
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public AccountStatusOption Status { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is new.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public bool IsNew { get; set; }
        /// <summary>
        /// Gets or sets the total trips.
        /// </summary>
        /// <value>
        /// The total trips.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public long TotalTrips { get; set; }
    }
}
