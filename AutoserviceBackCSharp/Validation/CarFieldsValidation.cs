using AutoserviceBackCSharp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace AutoserviceBackCSharp.Validation
{
    public class CarFieldsValidation : Validator
    {
        private readonly int minMarkLength = 3;
        private readonly int maxMarkLength = 30;
        private readonly int minCarNumberLength = 1;
        private readonly int maxCarNumberLength = 30;
        private readonly int vinCodeLength = 17;
        private readonly int minCarMenufacturingYear = 1900;

        public CarFieldsValidation() : base(){ }

        public bool ValidateMark(string mark)
        {
            if (mark.Length < minMarkLength || mark.Length > maxMarkLength)
            {
                return false;
            }

            return true;
        }

        public bool ValidateVinCode(string vinCode)
        {
            if (vinCode.Length!= vinCodeLength)
            {
                return false;
            }

            return true;
        }

        public bool ValidateCarNumber(string carNumber)
        {
            if (carNumber.Length < minCarNumberLength || carNumber.Length > maxCarNumberLength)
            {
                return false;
            }

            return true;
        }

        public bool ValidateYear(DateTime year)
        {
            if (DateOnly.FromDateTime(year).Year < minCarMenufacturingYear || DateOnly.FromDateTime(year).Year > DateTime.Now.Year)
            {
                return false;
            }

            return true;
        }

        public bool ValidateClientID(int clientId, PracticedbContext context)
        {
            if (clientId != 0 && (context.Clients.FirstOrDefault(client => client.Id == clientId) == null))
            {
                return false;
            }

            return true;
        }
    }
}
