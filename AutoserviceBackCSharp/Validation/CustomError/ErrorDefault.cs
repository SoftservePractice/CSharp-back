namespace AutoserviceBackCSharp.Validation.CustomError
{
    public class ErrorDefault : IError
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ErrorDefault(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
}
