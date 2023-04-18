namespace AutoserviceBackCSharp.Validation
{
    public static class PhoneValidator
    {
        private static readonly PhoneNumbers.PhoneNumberUtil phoneNumberUtil;

        static PhoneValidator()
        {
            phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
        }

        public static bool Validate(string? phone)
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
    }
}
