namespace AutoserviceBackCSharp.Validation
{
    public class SymbolValidator
    {
        private readonly char[] _symbolsToCheck;

        public SymbolValidator(char[] symbolsToCheck)
        {
            _symbolsToCheck = symbolsToCheck;
        }

        public bool IsValid(string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                return false;
            }

            foreach (char symbol in _symbolsToCheck)
            {
                if (propertyValue.Contains(symbol))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
