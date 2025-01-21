namespace DriveX_Showcase_Admin.Models
{
    public class All_Deals
    {
        public int Id { get; set; }

        public string CarName { get; set; }

        public string Dealer_Name { get; set; }
        public int Discount_Percent { get; set; }

        public DateTime Till_Date { get; set; }

        public All_Deals(string carName, string dealer_Name, int discount_Percent, DateTime till_Date)
        {
            CarName = carName;
            Dealer_Name = dealer_Name;
            Discount_Percent = discount_Percent;
            Till_Date = till_Date;
        }
    }
}
