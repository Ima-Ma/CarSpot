namespace DriveX_Showcase_Admin.Models
{
    public class Packages
    {
        public int id { get; set; }

        public string package_name { get; set; }

        public string price { get; set; }

        public string duration { get; set; }

        public string idea_for { get; set; }

        public string feature_1 { get; set; }

        public string feature_2 { get; set; }
        public string feature_3 { get; set; }

        public Packages(string package_name, string price, string duration, string idea_for, string feature_1, string feature_2, string feature_3)
        {
            this.package_name = package_name;
            this.price = price;
            this.duration = duration;
            this.idea_for = idea_for;
            this.feature_1 = feature_1;
            this.feature_2 = feature_2;
            this.feature_3 = feature_3;
        }
    }
}
