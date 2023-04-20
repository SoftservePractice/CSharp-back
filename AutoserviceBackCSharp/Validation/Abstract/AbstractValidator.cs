using System.Net.Mail;

namespace AutoserviceBackCSharp.Validation
{
    public abstract class Validator
    {
        protected readonly PhoneNumbers.PhoneNumberUtil phoneNumberUtil;
        public Validator()
        {
            phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
        }
    }
}