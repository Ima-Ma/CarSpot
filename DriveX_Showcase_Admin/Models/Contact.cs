namespace DriveX_Showcase_Admin.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public Contact(string name, string email, string message)
        {
            Name = name;
            Email = email;
            Message = message;
        }
    }
}
