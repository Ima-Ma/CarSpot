namespace DriveX_Showcase_Admin.Models
{
    public class Comments
    {
        public int id { get; set; }

        public int CarName { get; set; }

        public string BrandName { get; set; }

        public string Comment { get; set; }

        public string UserName { get; set; }

        public Comments(int carName, string brandName, string comment, string userName)
        {
            CarName = carName;
            BrandName = brandName;
            Comment = comment;
            UserName = userName;
        }
    }
}
