namespace DriveX_Showcase_Admin.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string announcement { get; set; }

        public string attachment { get; set; }

        public Announcement(string announcement, string attachment)
        {
            this.announcement = announcement;
            this.attachment = attachment;
        }
    }
}
