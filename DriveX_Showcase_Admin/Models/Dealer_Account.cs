using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveX_Showcase_Admin.Models
{
    public class Dealer_Account
    {
        public int id { get; set; }

        [ForeignKey("selected_brand")]
        public int selected_brand { get; set; }

        public string email { get; set; }
        public string address { get; set; }
        public DateTime registration_date { get; set; }
        [MinLength(5)]
        public string dealer_IdCode { get; set; }

        public string dealers { get; set; }

        public string status { get; set; }

        public Dealer_Account(int selected_brand, string email, string address, DateTime registration_date, string dealer_IdCode, string dealers, string status)
        {
            this.selected_brand = selected_brand;
            this.email = email;
            this.address = address;
            this.registration_date = registration_date;
            this.dealer_IdCode = dealer_IdCode;
            this.dealers = dealers;
            this.status = status;
        }
    }
}
