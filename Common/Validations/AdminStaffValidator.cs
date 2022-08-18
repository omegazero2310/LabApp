using CommonClass.Models;
using FluentValidation;

namespace CommonClass.Validations
{
    public class AdminStaffValidator : AbstractValidator<AdminStaff>
    {
        public AdminStaffValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().MaximumLength(50);
            RuleFor(user => user.PhoneNumber).NotEmpty();
            RuleFor(user => user.Address).NotEmpty();
            RuleFor(user => user.Gender).IsInEnum().NotEmpty();
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.PositionID).NotEmpty().IsInEnum();
        }
    }
}
