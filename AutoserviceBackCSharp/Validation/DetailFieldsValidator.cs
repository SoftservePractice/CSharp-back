using AutoserviceBackCSharp.Models;

namespace AutoserviceBackCSharp.Validation
{
    public class DetailFieldsValidator : Validator
    {
        private readonly int minModelLength = 3;
        private readonly int maxModelLength = 32;
        private readonly int minVendorCodeLength = 3;
        private readonly int maxVendorCodeLength = 32;
        private readonly int minDescriptionLength = 3;
        private readonly int maxDescriptionLength = 500;
        private readonly int minCompatibleVehiclesLength = 3;
        private readonly int maxCompatibleVehiclesLength = 500;

        public DetailFieldsValidator() : base() { }

        public bool ValidateModel(string model)
        {
            if (model.Length < minModelLength || model.Length > maxModelLength)
            {
                return false;
            }

            return true;
        }

        public bool ValidateVendorCode(string vendorCode)
        {
            if (vendorCode.Length < minVendorCodeLength || vendorCode.Length > maxVendorCodeLength)
            {
                return false;
            }

            return true;
        }

        public bool ValidateDescription(string description)
        {
            if (description.Length < minDescriptionLength || description.Length > maxDescriptionLength)
            {
                return false;
            }

            return true;
        }

        public bool ValidateCompatibleVehicles(string compatibleVehicles)
        {
            if (compatibleVehicles.Length < minCompatibleVehiclesLength || compatibleVehicles.Length > maxCompatibleVehiclesLength)
            {
                return false;
            }

            return true;
        }
    }
}
