namespace DriveX_Showcase_Admin.Models
{
    public class Dealers
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Dealers(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
