namespace AutoserviceBackCSharp.Validation
{
    public static class PhoneValidator
    {
        private static readonly PhoneNumbers.PhoneNumberUtil phoneNumberUtil;

        static PhoneValidator() { 
            phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
        }

        public static bool Validate(string? phone)
        {
            if (phone != null)
            {
                try
                {
                    var phoneNumber = phoneNumberUtil.Parse(phone, "UA");
                    if (!phoneNumberUtil.IsValidNumber(phoneNumber))
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
