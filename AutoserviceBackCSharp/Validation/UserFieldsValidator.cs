using System.Net.Mail;

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

            try
            {
                var phoneNumber = phoneNumberUtil.Parse(phone, "UA");
                return phoneNumberUtil.IsValidNumber(phoneNumber);
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public bool ValidateEmail(string? email)
        {
            if (email == null)
                return true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
