namespace AutoserviceBackCSharp.Validation.ClientView
{
    public class ClientViewModel
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public ClientViewModel(string? Name, string? Phone, string? Email)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
        }
    }
}
