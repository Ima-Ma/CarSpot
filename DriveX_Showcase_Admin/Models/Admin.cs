namespace DriveX_Showcase_Admin.Models
{
    public class Admin
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int Password { get; set; }

        public string Name { get; set; }

        public Admin(string email, int password, string name)
        {
            Email = email;
            Password = password;
            Name = name;
        }
    }
}
