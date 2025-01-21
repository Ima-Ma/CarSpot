namespace DriveX_Showcase_Admin.Models
{
    public class SubscriptionXPackages
    {
       public int id { get; set; }
        public string dealer_name { get; set; }
        public string dealer_IdCode { get; set; }

        public int package_subscription { get; set; }

        public DateTime subscription_date { get; set; }

        public string email { get; set; }

        public DateTime dealer_registration_date { get; set; }

        public SubscriptionXPackages(string dealer_name, string dealer_IdCode, int package_subscription, DateTime subscription_date, string email, DateTime dealer_registration_date)
        {
            this.dealer_name = dealer_name;
            this.dealer_IdCode = dealer_IdCode;
            this.package_subscription = package_subscription;
            this.subscription_date = subscription_date;
            this.email = email;
            this.dealer_registration_date = dealer_registration_date;
        }
    }
}
