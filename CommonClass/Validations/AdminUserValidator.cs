using CommonClass.Models;
using FluentValidation;

namespace CommonClass.Validations
{
    public class AdminUserValidator : AbstractValidator<AdminUser>
    {
        public AdminUserValidator()
        {
            RuleFor(user => user.UserID).NotEmpty().MaximumLength(50);
            RuleFor(user => user.HashedPassword).NotEmpty();
            RuleFor(user => user.Salt).NotEmpty();
        }
    }
}
