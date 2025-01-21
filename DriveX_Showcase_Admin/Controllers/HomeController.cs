using System.Diagnostics;
using System.Net;
using System.Net.Mail;

using DriveX_Showcase_Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace DriveX_Showcase_Admin.Controllers
{
    public class HomeController : Controller
    {
        Connection connection = new Connection();
        // EMAIL WORK

        public void sendmail(string to, string subject, string body)
        {
            MailMessage msg = new MailMessage("mushtaqueimama@gmail.com", to, subject, body);
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("mushtaqueimama@gmail.com", "wgbjwwyfdsfgdeqb"),
                EnableSsl = true
            };
            client.Send(msg);
        }
        //LOGIN PAGE
        public IActionResult Index()
        {
            return View();
        }
        // MAIN INDEX
        public IActionResult Index1()
        {
            var carCounts = new Dictionary<string, int>
        {
            { "BMW", connection.BMW.Count() },
            { "Toyota", connection.Toyota.Count() },
            { "Suzuki", connection.Suzuki.Count() },
            { "Honda", connection.Honda.Count() }
        };
            int totalCarCount = carCounts.Values.Sum();

            // Pass the total car count to the view
            ViewBag.TotalCarCount = totalCarCount;

            // Pass the car counts to the view
            ViewBag.CarCounts = carCounts;
            return View();
        }
        // DEALER LOGIN
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string dealer_IdCode)
        {

            var data = connection.Dealers_Account.Where(x => x.email == email && x.dealer_IdCode == dealer_IdCode && x.status == "Active").FirstOrDefault();
            if (data != null)
            {
                HttpContext.Session.SetString("Brand", data.dealers);
                return RedirectToAction("Index1");
            }
            else
            {
                TempData["Message"] = "Invalid Email or IdCode or Maybe Deactive !";
                TempData["AlertType"] = "error";
                return RedirectToAction("Login");
            }
        }

        public IActionResult LogoutDealer()
        {

            HttpContext.Session.Remove("Brand");
            return RedirectToAction("Index");
        }
        // ADMIN LOGIN
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(string Email, int Password)
        {
            var data = connection.AdminLogin.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            if (data != null)
            {
                HttpContext.Session.SetString("AdminEmail", data.Name);
                TempData["SuccessMessage"] = "Login successfully!";
                return RedirectToAction("Index1");
            }
            else
            {

                TempData["Message"] = "Invalid Email or Password!";
                TempData["AlertType"] = "error"; 
                return RedirectToAction("AdminLogin");
            }
        }
        public IActionResult LogoutAdmin()
        {

            HttpContext.Session.Remove("AdminEmail");
            return RedirectToAction("Index");

        }
        // CARS PAGES
        public IActionResult BMW()
        {
            var data = connection.BMW.ToList();
            return View(data);
        }
        public IActionResult BMWReadmore(int id)
        {
            var data = connection.BMW.Where(x => x.Id == id).ToList();
            Boolean showdata = data.Count > 5;
         
            return View(data);
        }
        public IActionResult BMWDelete(int id)
        {
            var data = connection.BMW.Where(x => x.Id == id).FirstOrDefault();
            connection.BMW.Remove(data);
            connection.SaveChanges();
            return RedirectToAction("BMW");
        }
        public IActionResult Toyota()
        {
            var data = connection.Toyota.ToList();
            return View(data);
        }
        public IActionResult ToyotaReadmore(int id)
        {
            var data = connection.Toyota.Where(x => x.Id == id).ToList();
            return View(data);
        }
        public IActionResult ToyotaDelete(int id)
        {
            var data = connection.Toyota.Where(x => x.Id == id).FirstOrDefault();
            connection.Toyota.Remove(data);
            connection.SaveChanges();
            return RedirectToAction("Toyota");
        }
        public IActionResult Suzuki()
        {
            var data = connection.Suzuki.ToList();
            return View(data);
        }
        public IActionResult SuzukiReadmore(int id)
        {
            var data = connection.Suzuki.Where(x => x.Id == id).ToList();
            return View(data);
        }
        public IActionResult SuzukiDelete(int id)
        {
            var data = connection.Suzuki.Where(x => x.Id == id).FirstOrDefault();
            connection.Suzuki.Remove(data);
            connection.SaveChanges();
            return RedirectToAction("Suzuki");
        }
        public IActionResult Mercedes()
        {
            return View();
        }
        public IActionResult Honda()
        {
            var data = connection.Honda.ToList();
            return View(data);
        }
        public IActionResult HondaReadmore(int id)
        {
            var data = connection.Honda.Where(x => x.Id == id).ToList();
            return View(data);
        }
        public IActionResult HondaDelete(int id)
        {
            var data = connection.Honda.Where(x => x.Id == id).FirstOrDefault();
            connection.Honda.Remove(data);
            connection.SaveChanges();
            return RedirectToAction("Honda");
        }
        // DEALER HISTORY PAGE
        public IActionResult dealer_history()
        {
            var data = connection.Dealers_Account.Where(x => x.status == "Deactive").ToList();
            return View(data);
        }

        public IActionResult Add_Brand()
        {
            var data = connection.Brands.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult Add_Brand(string brand_name, IFormFile Brand_Logo)
        {
            var filename = Brand_Logo.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Brand_Logo", filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                Brand_Logo.CopyTo(stream);
            }
            Brand brands = new Brand(brand_name, filename);
            connection.Brands.Add(brands);
            connection.SaveChanges();

            TempData["SuccessMessage"] = "Brand added successfully!";
            return RedirectToAction("Add_Brand");
        }
        // ADD NEW DEALER
        public IActionResult New_Dealer()
        {
            var data = connection.Brands.ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult new_dealer(
     int selected_brand,
     string email,
     string address,
     DateTime registration_date,
     string dealers)
        {
            Random random = new Random();
            var randomCode = random.Next(100000, 999999);
            string newDealerCode = "DX" + randomCode;
            var existingDealer = connection.Dealers_Account
                .Where(x => x.dealers == dealers)
                .FirstOrDefault();

            string dealerIdCode;

            if (existingDealer != null)
            {
                dealerIdCode = existingDealer.dealer_IdCode;
            }
            else
            {
                dealerIdCode = newDealerCode;
            }
            Dealer_Account newDealer = new Dealer_Account(
                selected_brand: selected_brand,
                email: email,
                address: address,
                registration_date: registration_date,
                dealer_IdCode: dealerIdCode,
                dealers: dealers,
                status: "Deactive"
            );
            connection.Dealers_Account.Add(newDealer);
            connection.SaveChanges();
            TempData["SuccessMessage"] = "Dealer added successfully!";
            return RedirectToAction("new_dealer");
        }

        public IActionResult add_vehicle()
        {
            return View();
        }

        public IActionResult subs_pkg_detail()
        {
            var data = connection.Subscription_Packages.ToList();

            return View(data);
        }

        public IActionResult add_new_package()
        {
            return View();
        }
        [HttpPost]
        public IActionResult add_new_package(string package_name, string price, string duration, string idea_for, string feature_1, string feature_2, string feature_3)
        {
            Packages data = new Packages(package_name, price, duration, idea_for, feature_1, feature_2, feature_3);
            connection.Subscription_Packages.Add(data);
            connection.SaveChanges();
            TempData["SuccessMessage"] = "Package added successfully!";
            return RedirectToAction("subs_pkg_detail");
        }

        public IActionResult readmore(int id)
        {
            return View();
        }

        public IActionResult active_account(int id)
        {
            var data2 = connection.Subscription_Packages.ToList();
            ViewBag.data2 = data2;
            var data = connection.Dealers_Account.Where(x => x.id == id).ToList().FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult active_account(string dealer_name, string dealer_IdCode, int package_subscription, DateTime subscription_date, string email, DateTime dealer_registration_date)
        {
            var data1 = connection.Dealers_Account.Where(x => x.dealer_IdCode == dealer_IdCode).FirstOrDefault();
            if (data1 != null)
            {
                data1.status = "Active";
            }

            SubscriptionXPackages data = new SubscriptionXPackages(dealer_name, dealer_IdCode, package_subscription, subscription_date, email, dealer_registration_date);
            connection.Subscription_Details.Add(data);
            connection.SaveChanges();
            var data2 = connection.Subscription_Packages.Where(x => x.id == package_subscription).FirstOrDefault();
            sendmail(email,
      "Your Package Subscription Has Been Successfully Activated",
      "<div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #ddd; border-radius: 10px; overflow: hidden; box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);'>" +
          "<div style='background-color: #4B49AC ; padding: 20px; text-align: center;'>" +
              "<h1 style='color: #fff; margin: 10px 0; font-size: 24px;'>Car Spot Showcase</h1>" +
          "</div>" +
          "<div style='padding: 20px; color: #333;'>" +
              "<h2 style='color: #555; font-size: 20px;'>Your Package Subscription Has Been Successfully Activated</h2>" +
              "<p style='font-size: 16px;'>Dear <span style='color: #007BFF; font-weight: bold;'>" + dealer_name + "</span>,</p>" +
              "<p style='font-size: 16px;'>We are pleased to inform you that your subscription with <b>Car Spot Showcase</b> has been successfully activated.</p>" +
              "<p style='font-size: 16px;'>Here are the details of your subscription:</p>" +
              "<ul style='font-size: 16px; list-style-type: none; padding: 0;'>" +
                  "<li style='margin: 5px 0;'><b>Dealer ID Code:</b> " + dealer_IdCode + "</li>" +
                  "<li style='margin: 5px 0;'><b>Subscription Package:</b> " + data2.package_name + "</li>" +
                  "<li style='margin: 5px 0;'><b>Subscription Date:</b> " + subscription_date.ToString("MMMM dd, yyyy") + "</li>" +
                  "<li style='margin: 5px 0;'><b>Registration Date:</b> " + dealer_registration_date.ToString("MMMM dd, yyyy") + "</li>" +
              "</ul>" +
              "<p style='font-size: 16px;'>Your account is now active, and you can continue enjoying all the exclusive benefits associated with your subscription package.</p>" +
              "<p style='font-size: 16px;'>If you have any questions or need assistance, please feel free to contact our support team.</p>" +
          "</div>" +
          "<div style='background-color: #f7f7f7; padding: 20px; text-align: center;'>" +
              "<p style='margin-top: 20px; font-size: 14px; color: #555;'>Thank you for being a valued part of Car Spot Showcase!<br>" +
              "<b>Best regards,</b><br>Car Spot Showcase Team</p>" +
          "</div>" +
      "</div>"
  );


            TempData["SuccessMessage"] = "Account has been successfully activated!";
            return RedirectToAction("dealer_history");
        }


        public IActionResult all_dealers()
        {
            var data = connection.Dealers_Account.ToList();
            return View(data);
        }
        public IActionResult active_dealer()
        {
            var duration = connection.Subscription_Packages.ToList();
            ViewBag.duration_days = duration;
            var data = connection.Subscription_Details.ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult Deactivate(string dealer_IdCode)
        {
            var dealer = connection.Dealers_Account.FirstOrDefault(x => x.dealer_IdCode == dealer_IdCode);
            if (dealer != null)
            {
                dealer.status = "Deactive";
            }

            var subscription = connection.Subscription_Details.FirstOrDefault(x => x.dealer_IdCode == dealer_IdCode);
            if (subscription != null)
            {
                connection.Subscription_Details.Remove(subscription);
            }
            connection.SaveChanges();
            if (dealer != null && !string.IsNullOrEmpty(dealer.email))
            {
                sendmail(dealer.email, "Account Deactivated - Please Re-subscribe to Activate",
                    "<h1 style='color: #555;'>Account Deactivated - Please Re-subscribe to Activate</h1>" +
                    "<p>Dear <span style='color: #007BFF; font-weight: bold;'>" + dealer_IdCode + "</span>,</p>" +
                    "<p>We hope this message finds you well. We regret to inform you that your account with <b>DriveX Showcase</b> has been deactivated due to the expiration of your subscription.</p>" +
                    "<p>To restore your access and continue enjoying our services and benefits, we kindly request that you re-subscribe to one of our available packages. Renewing your subscription will reactivate your account and allow you to maintain your active status.</p>" +
                    "<p>If you have any questions or need assistance with the re-subscription process, please don't hesitate to contact our support team.</p>" +
                    "<p style='margin-top: 20px; font-style: italic; color: #555;'>Thank you for being a valued part of DriveX Showcase!<br>" +
                    "<b>Best regards,</b><br>DriveX Showcase Team</p>"
                );
            }
            else
            {
                TempData["ErrorMessage"] = "Dealer's email address is missing or invalid.";
            }

            TempData["SuccessMessage"] = "Account deactivated, email has been sent, and subscription deleted successfully!";
            return RedirectToAction("active_dealer");
        }




        [HttpPost]
        public IActionResult SendEmail(string dealer_IdCode, string Email)
        {
            var dealer = connection.Subscription_Details.FirstOrDefault(x => x.dealer_IdCode == dealer_IdCode);
            if (dealer != null && !string.IsNullOrEmpty(dealer.email))
            {
                sendmail(dealer.email, "Subscription Expiring Soon",
                    "<h1 style='color: #555;'>Subscription Expiring Soon</h1>" +
                    "<p>Dear <span style='color: #007BFF; font-weight: bold;'>" + dealer_IdCode + "</span>,</p>" +
                    "<p>We hope this message finds you well. This is a gentle reminder that your current subscription package with <b>DriveX Showcase</b> is nearing its expiration date.</p>" +
                    "<p>To ensure uninterrupted access to our services and benefits, we encourage you to renew your subscription at the earliest convenience. Renewing your subscription allows you to continue enjoying exclusive features and maintain your account's active status.</p>" +
                    "<p>If you have any questions or need assistance, please feel free to reach out to our support team.</p>" +
                    "<p style='margin-top: 20px; font-style: italic; color: #555;'>Thank you for being a valued part of DriveX Showcase!<br>" +
                    "<b>Best regards,</b><br>DriveX Showcase Team</p>"
                );
            }
            TempData["SuccessMessage"] = "Subscription Expiring Soon Email Sent!";

            return RedirectToAction("active_dealer");
        }
        public IActionResult automobiles()
        {
            return View();
        }
        [HttpPost]
        public IActionResult automobiles(string carName, int carPrice, string carDescription, string fuelType, string variant, string mileage, string colors, string power, string engineCapacity, IFormFile image1, string ratingExterior, string ratingComfort, string ratingPerformance, string ratingFuelEconomy, IFormFile pDF, string dealer_id, string BrandName)
        {

            var filename1 = image1.FileName;
            var pdfname = pDF.FileName;

            var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/car_images", filename1);
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploaded_pdf", pdfname);
            using (FileStream stream = new FileStream(path1, FileMode.Create))
            {
                image1.CopyTo(stream);
            }
            using (FileStream stream = new FileStream(path2, FileMode.Create))
            {
                pDF.CopyTo(stream);
            }
            if (BrandName == "BMW")
            {
                BMW bmw = new BMW(carName,carPrice,carDescription,fuelType,variant,mileage,colors,power,engineCapacity,filename1,ratingExterior,ratingComfort,ratingPerformance,ratingFuelEconomy,dealer_id,pdfname, "No Deal");
                connection.BMW.Add(bmw);
            }
            else if (BrandName == "Honda")
            {
                Honda honda = new Honda(carName, carPrice, carDescription, fuelType, variant, mileage, colors, power, engineCapacity, filename1, ratingExterior, ratingComfort, ratingPerformance, ratingFuelEconomy, dealer_id, pdfname , "No Deal");
                connection.Honda.Add(honda);
            }
            else if (BrandName == "Toyota")
            {
                Toyata toyota = new Toyata(carName, carPrice, carDescription, fuelType, variant, mileage, colors, power, engineCapacity, filename1, ratingExterior, ratingComfort, ratingPerformance, pdfname, dealer_id, ratingFuelEconomy, "No Deal");
                connection.Toyota.Add(toyota);
            }
            else if (BrandName == "Suzuki")
            {
                Suzuki suzuki = new Suzuki(carName, carPrice, carDescription, fuelType, variant, mileage, colors, power, engineCapacity, filename1, ratingExterior, ratingComfort, ratingPerformance, pdfname, ratingFuelEconomy, dealer_id, "No Deal");
                connection.Suzuki.Add(suzuki);
            }


            connection.SaveChanges();
            TempData["SuccessMessage"] = " Vehicle added Successfully!";
            return RedirectToAction("automobiles");
        }
        public IActionResult edit_delete_automobiles()
        {
            return View();
        }
        public IActionResult booking()
        {
            var data = connection.Booking_Form.ToList();
            return View(data);
        }
        public IActionResult booking_show()
        {
            var data = connection.Booking_Form.Where(x => x.dealer_name == HttpContext.Session.GetString("Brand")).ToList();
            return View(data);
        }
        public IActionResult launch_deals()
        {
            var data1 = connection.BMW.Where(x => x.Dealer_id == HttpContext.Session.GetString("Brand")).ToList();
            var data2 = connection.Toyota.Where(x => x.Dealer_id == HttpContext.Session.GetString("Brand")).ToList();
            var data3 = connection.Honda.Where(x => x.Dealer_id == HttpContext.Session.GetString("Brand")).ToList();
            var data4 = connection.Suzuki.Where(x => x.Dealer_id == HttpContext.Session.GetString("Brand")).ToList();

            ViewBag.bmw = data1;
            ViewBag.toyota = data2;
            ViewBag.honda = data3;
            ViewBag.suzuki = data4;
            return View();
        }
        public IActionResult dealercar_show()
        {

            var data1 = connection.BMW.Where(x => x.Dealer_id == HttpContext.Session.GetString("Brand")).ToList();
            var data2 = connection.Toyota.Where(x => x.Dealer_id == HttpContext.Session.GetString("Brand")).ToList();
            var data3 = connection.Honda.Where(x => x.Dealer_id == HttpContext.Session.GetString("Brand")).ToList();
            var data4 = connection.Suzuki.Where(x => x.Dealer_id == HttpContext.Session.GetString("Brand")).ToList();

            ViewBag.bmw = data1;
            ViewBag.toyota = data2;
            ViewBag.honda = data3;
            ViewBag.suzuki = data4;
            return View();
        }
        public IActionResult dealercar_Readmore(int id)
        {

            var data1 = connection.BMW.Where(x => x.Id == id).ToList();
            var data2 = connection.Toyota.Where(x => x.Id == id).ToList();
            var data3 = connection.Honda.Where(x => x.Id ==id).ToList();
            var data4 = connection.Suzuki.Where(x => x.Id ==id).ToList();

            ViewBag.bmw = data1;
            ViewBag.toyota = data2;
            ViewBag.honda = data3;
            ViewBag.suzuki = data4;
            return View();
        }
        public IActionResult User_Messages()
        {

            var data = connection.Contact.ToList();
            return View(data);
        }
        public IActionResult user_reply(int id)
        {

            var data = connection.Contact.Where(x=> x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult user_reply(string email, string reply, string name)
        {
            var data = connection.Contact.Where(x => x.Email == email && x.Name == name).FirstOrDefault();
            sendmail(email,
               "Reply To Your Query",
               "<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Dear "+ name + "</p>" +
               "<p style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>Thank you for reaching out to <strong style='color: #007BFF;'>CarSpot</strong>. We appreciate your interest and value your queries.</p>" +
               "<p style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>We have reviewed your request and would like to provide you with the following response:</p>" +
               "<p style='font-family: Arial, sans-serif; font-size: 14px; color: #333; font-style: italic;'>"+ reply +"</p>" +
               "<p style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>If you have any additional questions or need further assistance, please do not hesitate to contact us. Our team is here to help!</p>" +
               "<p style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>Best regards,</p>" +
               "<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333; font-weight: bold;'>CarSpot Customer Support Team</p>" +
               "<p style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>" +
               "</p>"
           );
            connection.Contact.Remove(data);
            connection.SaveChanges();
            TempData["SuccessMessage"] = "Reply  Sent And Deleted Successfully!";
            return RedirectToAction("User_Messages");
        }

        public IActionResult announcement()
        {
            var data = connection.Announcements.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult announcement(string announcement , IFormFile attachment)
        {
            if (attachment != null)
            {
                var filename = attachment.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/attachments", filename);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    attachment.CopyTo(stream);
                }
                Announcement data = new Announcement(announcement, filename);
                connection.Announcements.Add(data);
                connection.SaveChanges();
            }
            else
            {
               
                Announcement data = new Announcement(announcement, "No Attachment");
                connection.Announcements.Add(data);
                connection.SaveChanges();
            }
            TempData["SuccessMessage"] = "Announcement Post Successfully!";
            return RedirectToAction("announcement");
        }
        public IActionResult all_deals(string carname)
        {
            var data1 = connection.BMW.Where(x => x.CarName == carname).FirstOrDefault();
            var data2 = connection.Toyota.Where(x => x.CarName == carname).FirstOrDefault();
            var data3 = connection.Honda.Where(x => x.CarName == carname).FirstOrDefault();
            var data4 = connection.Suzuki.Where(x => x.CarName == carname).FirstOrDefault();

            ViewBag.bmw = data1;
            ViewBag.toyota = data2;
            ViewBag.honda = data3;
            ViewBag.suzuki = data4;
            return View();
        }
        [HttpPost]
        public IActionResult all_deals(string CarName, int Discount_Percent, string Dealer_Name, DateTime Till_Date , string Brand)
        {
            string emailContent = $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <h2 style='color: #007BFF;'>Exciting Deal on {CarName}!</h2>
                    <p>Dear Subscriber,</p>
                    <p>We are thrilled to announce a special offer brought to you by <strong>{Dealer_Name}</strong>, exclusively for our valued subscribers at <strong>CarSpot</strong>.</p>
                    <p>
                        <strong>Car Model:</strong> {CarName}<br>
                        <strong>Discount:</strong> {Discount_Percent}% Off<br>
                        <strong>Offer Valid Until:</strong> {Till_Date:dddd, MMMM d, yyyy}
                    </p>
                    <p>
                        Don't miss this chance to grab your dream car at an unbeatable price! Visit your nearest dealership today to learn more.
                    </p>
                    <p style='font-style: italic; color: #666;'>
                        This exclusive offer is available for a limited time only. Act fast!
                    </p>
                    <p>
                        Warm regards,<br>
                        <strong>The CarSpot Team</strong>
                    </p>
                </div>";
            if(Brand == "Honda")
            {
                var honda = connection.Honda.Where(x => x.CarName == CarName).FirstOrDefault();
                if(honda != null)
                {
                   honda.Status  = "Deal";
                }
            }
            else if (Brand == "BMW")
            {
                var bmw = connection.BMW.Where(x => x.CarName == CarName).FirstOrDefault();
                if (bmw != null)
                {
                    bmw.Status = "Deal";
                }
            }
            else if (Brand == "Toyota")
            {
                var toyota = connection.Toyota.Where(x => x.CarName == CarName).FirstOrDefault();
                if (toyota != null)
                {
                    toyota.Status = "Deal";
                }
            }
            else if (Brand == "Suzuki")
            {
                var suzuki = connection.Suzuki.Where(x => x.CarName == CarName).FirstOrDefault();
                if (suzuki != null)
                {
                    suzuki.Status = "Deal";
                }
            }
            var alldata = connection.All_Deals.Where(x => x.CarName == CarName).FirstOrDefault();
            if(alldata!= null)
            {
                TempData["SuccessMessage"] = "Already Deals Posted!";
                return RedirectToAction("launch_deals");
            }
            else
            {
                var all_subscriber = connection.Subscribers.ToList();
                foreach (var subscriber in all_subscriber)
                {
                sendmail(subscriber.email,"New Deal From "+ Dealer_Name, emailContent);
                }
                All_Deals data = new All_Deals(CarName, Dealer_Name, Discount_Percent, Till_Date);
                connection.All_Deals.Add(data);
                connection.SaveChanges();
                TempData["SuccessMessage"] = "Deal Post Successfully!";
                return RedirectToAction("launch_deals");
            }
           
        }
        public IActionResult Send_Mail(int id)
        {
            var data = connection.Booking_Form.Where(x => x.id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Send_Mail(string email, string reply , string CarName  )
        {
            sendmail(email, "Booking Respond",
                $"Dear Customer,\n\n" +
                $"We have received your inquiry regarding the vehicle \"{CarName}\".\n" +
                $"{reply}\n\n" +
                $"Thank you for choosing our services. If you have further questions, feel free to reach out to us.\n\n" +
                $"Best regards,\n" +
                $"CarSpot Team");

            var data = connection.Booking_Form.Where(x => x.car_name == CarName).FirstOrDefault();
            if(data != null)
            {
                data.status = "Responded";
            }
            connection.SaveChanges();
            TempData["SuccessMessage"] = "Response Sent Successfully!";
            return RedirectToAction("booking_show");
        }
        public IActionResult MarkAsConfirm(int id)
        {
           
            var data = connection.Booking_Form.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                data.status = "Confirmed";
            }
            sendmail(data.user_email,
                "Booking Confirmation - CarSpot",
                $"Dear {data.full_name},\n\n" +
                $"Thank you for booking the vehicle \"{data.car_name}\" with us. We have successfully received your booking request. Our team will review your inquiry and contact you shortly with further details.\n\n" +
                $"If you have any additional questions or need assistance, please don't hesitate to reach out to us at support@carspot.com.\n\n" +
                $"Thank you for choosing CarSpot. We look forward to serving you.\n\n" +
                $"Best regards,\n" +
                $"The CarSpot Team");
            connection.SaveChanges();
            TempData["SuccessMessage"] = "Response Sent Successfully!";
            return RedirectToAction("booking_show");
        }
        public IActionResult View_Announcement()
        {
            var data = connection.Announcements.ToList();
            return View(data);
        }
        public IActionResult myprofile()
        {
            var data = connection.Dealers_Account.Where(x=> x.dealers == HttpContext.Session.GetString("Brand")).FirstOrDefault();
            var data2 = connection.Subscription_Details.Where(x => x.dealer_name == HttpContext.Session.GetString("Brand")).FirstOrDefault();
            ViewBag.data2 = data2;
            return View(data);
        }
        public IActionResult viewdeales()
        {
            var data = connection.All_Deals.Where(x => x.Dealer_Name == HttpContext.Session.GetString("Brand")).ToList();
            var dealername = connection.Dealers_Account.Where(x => x.dealers == HttpContext.Session.GetString("Brand")).FirstOrDefault();
            ViewBag.dealername = dealername;
            return View(data);
        }
        public IActionResult deletedeals(int id, int Brand, string CarName)
        {
            if (Brand == 1)
            {
                var honda = connection.Honda.Where(x => x.CarName == CarName).FirstOrDefault();
                if (honda != null)
                {
                    honda.Status = "No Deal";
                    connection.SaveChanges(); // Ensure the change is saved
                }
            }
            else if (Brand == 3)
            {
                var bmw = connection.BMW.Where(x => x.CarName == CarName).FirstOrDefault();
                if (bmw != null)
                {
                    bmw.Status = "No Deal";
                    connection.SaveChanges(); // Ensure the change is saved
                }
            }
            else if (Brand == 4)
            {
                var toyota = connection.Toyota.Where(x => x.CarName == CarName).FirstOrDefault();
                if (toyota != null)
                {
                    toyota.Status = "No Deal";
                    connection.SaveChanges(); // Ensure the change is saved
                }
            }
            else if (Brand == 5)
            {
                var suzuki = connection.Suzuki.Where(x => x.CarName == CarName).FirstOrDefault();
                if (suzuki != null)
                {
                    suzuki.Status = "No Deal";
                    connection.SaveChanges(); // Ensure the change is saved
                }
            }

            // Ensure that the deal exists before removing it
            var data = connection.All_Deals.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                connection.All_Deals.Remove(data);
                connection.SaveChanges(); // Commit the removal of the deal
            }

            TempData["SuccessMessage"] = "Deal Deleted Successfully!";
            return RedirectToAction("viewdeales");
        }

        public IActionResult subscriber()
        {
            var data = connection.Subscribers.ToList();
            return View(data);
        }
        public IActionResult Delete_brand(int id)
        {
            var data=connection.Brands.Where(x=> x.id  == id).FirstOrDefault();
            connection.Brands.Remove(data);
            connection.SaveChanges();
            return RedirectToAction("Add_Brand");
        }
    }
}
