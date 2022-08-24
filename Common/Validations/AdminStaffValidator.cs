using CommonClass.Enums;
using CommonClass.Models;
using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace CommonClass.Validations
{
    public class AdminStaffValidator : AbstractValidator<AdminStaff>
    {
        public AdminStaffValidator()
        {
            RuleFor(user => user.UserName).Custom((value, context) =>
            {
                if (string.IsNullOrEmpty(value))
                    context.AddFailure("MSG_USER_NAME_NOT_EMPTY");
                else if (value.Length > 50)
                    context.AddFailure("MSG_USER_NAME_OVER_CHARACTER");
            });
            RuleFor(user => user.PhoneNumber).Custom((st, context) =>
            {
                if (string.IsNullOrEmpty(st))
                    context.AddFailure("MSG_PHONE_NUMBER_NOT_EMPTY");
                else
                {
                    foreach (char c in st)
                        if (c < '0' || c > '9')
                        {
                            context.AddFailure("MSG_PHONE_NUMBER_NUMBER_ONLY");
                            break;
                        }
                }
            });
            RuleFor(user => user.Address).NotEmpty().WithMessage("MSG_ADDRESS_CANNOT_EMPTY");
            RuleFor(user => user.Gender).Custom((value, context) =>
            {
                if (!Enum.IsDefined(typeof(GenderOptions), value))
                    context.AddFailure("MSG_GENDER_NOT_VALID");
            });
            RuleFor(user => user.Email).Custom((value, context) =>
            {
                if (string.IsNullOrEmpty(value))
                    context.AddFailure("MSG_EMAIL_NOT_EMPTY");
                else if (!this.IsValidEmail(value))
                    context.AddFailure("MSG_EMAIL_NOT_VALID");
            });
            RuleFor(user => user.PositionID).Custom((value, context) =>
            {
                if (!Enum.IsDefined(typeof(PositionOptions), value))
                    context.AddFailure("MSG_POSITION_NOT_VALID");
            });
        }
        private bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
