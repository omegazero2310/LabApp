using CommonClass.Models;
using FluentValidation;

namespace CommonClass.Validations
{
    /// <summary>
    /// Lớp check dữ liệu hợp lệ bảng Admin.User
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;CommonClass.Models.AdminUser&gt;" />
    public class AdminUserValidator : AbstractValidator<AdminUser>
    {
        public AdminUserValidator()
        {
            RuleFor(user => user.UserID).NotEmpty().MaximumLength(50);
            RuleFor(user => user.HashedPassword).NotEmpty();
            RuleFor(user => user.Salt).NotEmpty();
            RuleFor(user => user.AccountStatus).IsInEnum();
        }
    }
}
