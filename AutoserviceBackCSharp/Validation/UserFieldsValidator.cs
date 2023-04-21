using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AutoserviceBackCSharp.Validation
{
    public class UserFieldsValidator : Validator
    {
        public UserFieldsValidator() : base()
        {

        }

        public bool ValidatePhone(string? phone)
        {
            if (phone == null)
                return true;

            Regex regex = new Regex(@"\D");

            if (!regex.IsMatch(phone))
            {
                var phoneNumber = phoneNumberUtil.Parse(phone, "UA");

                return phoneNumberUtil.IsValidNumber(phoneNumber);
            }

            return false;

        }

        public bool ValidateEmail(string? email)
        {
            if (email == null)
                return true;

            return MailAddress.TryCreate(email, out MailAddress mailAddress);
        }
    }
}
