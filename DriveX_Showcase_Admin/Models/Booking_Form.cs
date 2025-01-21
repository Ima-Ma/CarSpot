namespace DriveX_Showcase_Admin.Models
{
    public class Booking_Form
    {
        public int id { get; set; }
        public string car_name { get; set; }
        public string dealer_name { get; set; }
        public string full_name { get; set; }
        public string user_email { get; set; }
        public string phone_number { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public Booking_Form(string car_name, string dealer_name, string full_name, string user_email, string phone_number, string message , string status)
        {
            this.car_name = car_name;
            this.dealer_name = dealer_name;
            this.full_name = full_name;
            this.user_email = user_email;
            this.phone_number = phone_number;
            this.message = message;
            this.status = status;
        }
    }
}
