namespace DriveX_Showcase_Admin.Models
{
    public class Subscribers
    {
        public int id { get; set; }
        public string email { get; set; }

        public Subscribers(string email)
        {
            this.email = email;
        }
    }

}
