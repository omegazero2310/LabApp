using CommonClass.Enums;
using CommonClass.Models;
using FluentValidation;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommonClass.Validations
{
    /// <summary>
    /// Lớp check dữ liệu hợp lệ bảng AdminStaff
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;CommonClass.Models.AdminStaff&gt;" />
    public class AdminStaffValidator : AbstractValidator<AdminStaff>
    {
        private const string SPECIALCHARACTERS = @"'/\%*‘;$£&#^@|?+=<>\""";
        public AdminStaffValidator()
        {
            RuleFor(user => user.UserName).Custom((value, context) =>
            {
                if (string.IsNullOrEmpty(value))
                    context.AddFailure("MSG_USER_NAME_NOT_EMPTY");
                else if (value.Length > 50)
                    context.AddFailure("MSG_USER_NAME_OVER_CHARACTER");
                else if (value.ToCharArray().Any(ch => SPECIALCHARACTERS.ToCharArray().Contains(ch)))
                    context.AddFailure("MSG_USER_NAME_CONTAIN_SPECIALCHARACTERS");
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
                if(!this.IsValidPhoneNumber(st))
                    context.AddFailure("MSG_PHONE_NUMBER_NOT_VALID");
                else if (st.ToCharArray().Any(ch => SPECIALCHARACTERS.ToCharArray().Contains(ch)))
                    context.AddFailure("MSG_PHONE_NUMBER_CONTAIN_SPECIALCHARACTERS");
            });
            RuleFor(user => user.Address).Custom((value, context) =>
            {
                if (string.IsNullOrEmpty(value))
                    context.AddFailure("MSG_ADDRESS_CANNOT_EMPTY");
                else if (value.ToCharArray().Any(ch => SPECIALCHARACTERS.ToCharArray().Contains(ch)))
                    context.AddFailure("MSG_ADDRESS_CONTAIN_SPECIALCHARACTERS");
            });
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
        }
        private bool IsValidEmail(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
                return false;
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        private bool IsValidPhoneNumber(string strIn)
        {
            if(string.IsNullOrEmpty(strIn))
                return false;
            //^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$
            return Regex.IsMatch(strIn, @"^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$");
        }
    }
}
