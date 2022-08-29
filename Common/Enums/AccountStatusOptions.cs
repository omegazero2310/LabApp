namespace CommonClass.Enums
{
    /// <summary>
    /// Trạng thái tài khoản Admin.User
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public enum AccountStatusOptions
    {
        /// <summary>
        /// bình thường, tài khoản không bị hạn chế
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        Normal = 0,
        /// <summary>
        /// tài khoản bị tạm khóa, không thể đăng nhập tạm thời
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        Suppended = 1,
        /// <summary>
        /// tài khoản bị cấm, không thể đăng nhập vĩnh viễn
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        Banned = 2,
    }
}
