
using Microsoft.EntityFrameworkCore;

namespace DriveX_Showcase_Admin.Models
{
    public class Connection : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-F48Q0VS ; User Id=sa ; Database=DriveX ; Password=aptech123 ; TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer("Server=DESKTOP-AUP15IR\\SQLEXPRESS01;Database=DriveX; Trusted_Connection=True; TrustServerCertificate=True;");
        }

        public DbSet<Admin> AdminLogin { get; set; }
        public DbSet<Dealer_Account> Dealers_Account { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Packages> Subscription_Packages { get; set; }
        public DbSet<SubscriptionXPackages> Subscription_Details { get; set; }
        public DbSet<BMW> BMW { get; set; }
        public DbSet<Toyata> Toyota { get; set; }
        public DbSet<Honda> Honda { get; set; }
        public DbSet<Suzuki> Suzuki { get; set; }
        public DbSet<Mercedes_Benz> Mercedes_Benz { get; set; }
        public DbSet<Booking_Form> Booking_Form { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<All_Deals> All_Deals { get; set; }
        public DbSet<Subscribers> Subscribers { get; set; }

        public DbSet<Comments> Comment { get; set; }

    }
}
