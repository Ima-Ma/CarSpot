namespace DriveX_Showcase_Admin.Models
{
    public class Brand
    {
        public int id { get; set; }
        public string brand_name { get; set; }
       
        public string Brand_Logo { get; set; }

        public Brand( string brand_name, string brand_Logo)
        {
           
            this.brand_name = brand_name;
            Brand_Logo = brand_Logo;
        }
    }
}
