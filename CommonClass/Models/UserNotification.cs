using CommonClass.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonClass.Models
{
    /// <summary>Save user Notification (from system, other users,...)</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 16/08/2022 created
    /// </Modified>
    [Table("UserNotification")]
    public class UserNotification
    {
        /// <summary>
        /// Gets or sets the notification identifier.
        /// </summary>
        /// <value>
        /// The notification identifier.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public Guid NotificationId { get; set; }
        /// <summary>
        /// Gets or sets the name of the user. the UserName from the table UserLogin
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
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Gets or sets the type of the notification. 1-System; 2-OtherUser; 3-Admin
        /// </summary>
        /// <value>
        /// The type of the notification.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public NotificationTypeOption NotificationType { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public string Content { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is readed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is readed; otherwise, <c>false</c>.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public bool IsReaded { get; set; }

    }
}
