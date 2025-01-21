using System.Diagnostics;
using DriveX_Showcase_Admin.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace DriveX_UserPanel.Controllers
{
    public class UserController : Controller
    {
        DriveX_Showcase_Admin.Models.Connection connection = new DriveX_Showcase_Admin.Models.Connection();
        public IActionResult Index()
        {
            var dealer_data = connection.Dealers_Account.ToList();
            ViewBag.dealerdata = dealer_data;
            var dealer_Active = connection.Dealers_Account
                .Where(x => x.status == "Active")
                .Select(x => x.dealers) 
                .ToList();
            var bmwcars = connection.BMW
                .Where(x => dealer_Active.Contains(x.Dealer_id))
                .ToList();

            ViewBag.BMW_Data = bmwcars;
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;

            var alldeals = connection.All_Deals.ToList();
            ViewBag.alldeals = alldeals;
            var datas = connection.Brands.ToList();
            return View(datas);
        }
        public IActionResult AllVehicles()
        {
            var dealer_Active = connection.Dealers_Account
                .Where(x => x.status == "Active")
                .Select(x => x.dealers)
                .ToList();
            var dealer_data = connection.Dealers_Account.ToList();
            ViewBag.dealerdata = dealer_data;
            var bmwcars = connection.BMW.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.BMW_Data = bmwcars;
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;

            var alldeals = connection.All_Deals.ToList();
            ViewBag.alldeals = alldeals;
            var datas = connection.Brands.ToList();
            return View(datas);
        }
        public IActionResult OurBrands()
        {
            var dealer_Active = connection.Dealers_Account
                .Where(x => x.status == "Active")
                .Select(x => x.dealers)
                .ToList();
            var dealer_data = connection.Dealers_Account.ToList();
            ViewBag.dealerdata = dealer_data;
            var bmwcars = connection.BMW.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.BMW_Data = bmwcars;
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var data = connection.Brands.ToList();
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;
            var data2 = connection.Brands.ToList();
            return View(data2);
        }
        public IActionResult FindDealers()
        {
            var dealer_Active = connection.Dealers_Account
                .Where(x => x.status == "Active")
                .Select(x => x.dealers)
                .ToList();
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var bmwcars = connection.BMW.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.BMW_Data = bmwcars;
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;
            var brand = connection.Brands.ToList();
            ViewBag.brand = brand;
            var booking = connection.Booking_Form.ToList();
            ViewBag.Booking = booking;
            var data3 = connection.Dealers_Account.ToList();
            return View(data3);
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(string Name , string Email , string Message)
        {
            Contact contact = new Contact(Name, Email, Message);
            connection.Contact.Add(contact);
            connection.SaveChanges();
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        public IActionResult HondaDiscover(int id)
        {
            var dealer_data = connection.Dealers_Account.ToList();
            ViewBag.dealerdata = dealer_data;
            var dealer_Active = connection.Dealers_Account
            .Where(x => x.status == "Active")
            .Select(x => x.dealers)
            .ToList();
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var bmwcars = connection.BMW.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.BMW_Data = bmwcars;
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;

            var comments = connection.Comment.ToList();
            ViewBag.comments = comments;
            var data = connection.Honda.Where(x => x.Id == id).FirstOrDefault();
            var deals = connection.All_Deals.ToList();
            ViewBag.deals = deals;
            return View(data);
        }
        [HttpPost("HondaDiscover/AddCommentHonda")]
        public IActionResult AddCommentHonda(int Carname, string Comment, string UserName)
        {
            Comments data = new Comments(Carname, "Honda", Comment, UserName);
            connection.Comment.Add(data);
            connection.SaveChanges();
            TempData["SuccessComment"] = " Thanks For Feedback!";
            return RedirectToAction("AllVehicles");
        }


        [HttpPost("HondaDiscover/AddBookingHonda")]
        public IActionResult AddBookingHonda(string car_name, string dealer_name, string full_name, string user_email, string phone_number, string message)
        {
            Booking_Form data = new Booking_Form(car_name, dealer_name, full_name, user_email, phone_number, message, "Pending");
            connection.Booking_Form.Add(data);
            connection.SaveChanges();
            TempData["SuccessBooking"] = " Booking Request Has Been Sent!";
            return RedirectToAction("AllVehicles");
        }
        public IActionResult BMWDiscover(int id)
        {
            var dealer_data = connection.Dealers_Account.ToList();
            ViewBag.dealerdata = dealer_data;

            var dealer_Active = connection.Dealers_Account
             .Where(x => x.status == "Active")
             .Select(x => x.dealers)
             .ToList();
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var bmwcars = connection.BMW.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.BMW_Data = bmwcars;
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;
            var comments = connection.Comment.ToList();
            ViewBag.comments = comments;
            var deals = connection.All_Deals.ToList();
            ViewBag.deals = deals;
            var data = connection.BMW.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost("BMWDiscover/BMWAddComment")]
        public IActionResult BMWAddComment(int Carname, string Comment, string UserName)
        {
            Comments data = new Comments(Carname, "BMW", Comment, UserName);
            connection.Comment.Add(data);
            connection.SaveChanges();
            TempData["SuccessComment"] = " Thanks For Feedback!";
            return RedirectToAction("AllVehicles");
        }
        [HttpPost("BMWDiscover/BMWAddBooking")]
        public IActionResult BMWAddBooking(string car_name, string dealer_name, string full_name, string user_email, string phone_number, string message)
        {
            Booking_Form data = new Booking_Form(car_name, dealer_name, full_name, user_email, phone_number, message, "Pending");
            connection.Booking_Form.Add(data);
            connection.SaveChanges();
            TempData["SuccessBooking"] = " Booking Request Has Been Sent!";
            return RedirectToAction("AllVehicles");
        }
        public IActionResult SuzukiDiscover(int id)
        {
            var dealer_data = connection.Dealers_Account.ToList();
            ViewBag.dealerdata = dealer_data;

            var dealer_Active = connection.Dealers_Account
            .Where(x => x.status == "Active")
            .Select(x => x.dealers)
            .ToList();
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var bmwcars = connection.BMW.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.BMW_Data = bmwcars;
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;

            var comments = connection.Comment.ToList();
            ViewBag.comments = comments;
            var deals = connection.All_Deals.ToList();
            ViewBag.deals = deals;
            var data = connection.Suzuki.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost("SuzukiDiscover/AddCommentSuzuki")]
        public IActionResult AddCommentSuzuki(int Carname, string Comment, string UserName)
        {
            Comments data = new Comments(Carname, "Suzuki", Comment, UserName);
            connection.Comment.Add(data);
            connection.SaveChanges();
            TempData["SuccessComment"] = " Thanks For Feedback!";
            return RedirectToAction("AllVehicles");
        }


        [HttpPost("SuzukiDiscover/AddBookingSuzuki")]
        public IActionResult AddBookingSuzuki(string car_name, string dealer_name, string full_name, string user_email, string phone_number, string message)
        {
            Booking_Form data = new Booking_Form(car_name, dealer_name, full_name, user_email, phone_number, message, "Pending");
            connection.Booking_Form.Add(data);
            connection.SaveChanges();
            TempData["SuccessBooking"] = " Booking Request Has Been Sent!";
            return RedirectToAction("AllVehicles");
        }

        public IActionResult ToyotaDiscover(int id)
        {
            var dealer_data = connection.Dealers_Account.ToList();
            ViewBag.dealerdata = dealer_data;

            var dealer_Active = connection.Dealers_Account
            .Where(x => x.status == "Active")
            .Select(x => x.dealers)
            .ToList();
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var bmwcars = connection.BMW.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.BMW_Data = bmwcars;
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;

            var comments = connection.Comment.ToList();
            ViewBag.comments = comments;
            var deals = connection.All_Deals.ToList();
            ViewBag.deals = deals;
            var data = connection.Toyota.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost("ToyotaDiscover/AddCommentToyota")]
        public IActionResult AddCommentToyota(int Carname, string Comment, string UserName)
        {
            Comments data = new Comments(Carname, "Toyota", Comment, UserName);
            connection.Comment.Add(data);
            connection.SaveChanges();
            TempData["SuccessComment"] = " Thanks For Feedback!";
            return RedirectToAction("AllVehicles");
        }


        [HttpPost("ToyotaDiscover/AddBookingToyota")]
        public IActionResult AddBookingToyota(string car_name, string dealer_name, string full_name, string user_email, string phone_number, string message)
        {
            Booking_Form data = new Booking_Form(car_name, dealer_name, full_name, user_email, phone_number, message, "Pending");
            connection.Booking_Form.Add(data);
            connection.SaveChanges();
            TempData["SuccessBooking"] = " Booking Request Has Been Sent!";
            return RedirectToAction("AllVehicles");
        }
        public IActionResult Discover_Brand(string name)
        {
            

            if (name == "BMW")
            {
                return RedirectToAction("BMWCars");
            }
            else if (name == "Toyota")
            {
                return RedirectToAction("ToyotaCars");
            }
            else if (name == "Honda")
            {
                return RedirectToAction("HondaCars");
            }
            else if (name == "Suzuki")
            {
                return RedirectToAction("SuzukiCars");
            }
            else if (name == "Mercedes_Benz")
            {
                return RedirectToAction("ErrorPage");
            }

            return View();
        }
        public IActionResult DiscoverDealer(int id)
        {
            var data = connection.Dealers_Account.Where(x => x.id == id).FirstOrDefault();

            var alldatahonda = connection.Honda.Where(x => x.Dealer_id == data.dealers).ToList();
            var alldatabmw = connection.BMW.Where(x => x.Dealer_id == data.dealers).ToList();
            var alldatatoyota = connection.Toyota.Where(x => x.Dealer_id == data.dealers).ToList(); // Fix: Toyota model was fetching BMW data
            var alldatasuzuki = connection.Suzuki.Where(x => x.Dealer_id == data.dealers).ToList(); // Fix: Suzuki model was fetching BMW data

            ViewBag.alldatahonda = alldatahonda;
            ViewBag.alldatabmw = alldatabmw;
            ViewBag.alldatatoyota = alldatatoyota;
            ViewBag.alldatasuzuki = alldatasuzuki;
            return View(data);
        }


        public IActionResult SuzukiCars()
        {
            var data = connection.Suzuki.ToList();
            return View(data);
        }
        public IActionResult BMWCars()
        {
            var data = connection.BMW.ToList();
            return View(data);
        }
        public IActionResult ToyotaCars()
        {
            var data = connection.Toyota.ToList();
            return View(data);
        }
        public IActionResult HondaCars()
        {
            var data = connection.Honda.ToList();
            return View(data);
        }
        public IActionResult Errorpage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Subscribers(string email)
        {
            Subscribers data = new Subscribers(email);
            var similar = connection.Subscribers.Where(x => x.email == email).FirstOrDefault();
            if(similar != null)
            {
                TempData["SuccessMessage"] = "You Have Already Subscribe";

            }
            else
            {
                connection.Subscribers.Add(data);
                connection.SaveChanges();
               
            }
    
            return RedirectToAction("Index");
        }
        public IActionResult Comparision()
        {
            var dealer_data = connection.Dealers_Account.ToList();
            ViewBag.dealerdata = dealer_data;
            var dealer_Active = connection.Dealers_Account
            .Where(x => x.status == "Active")
            .Select(x => x.dealers)
            .ToList();
            var hondacar = connection.Honda.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Honda_Data = hondacar;
            var bmwcars = connection.BMW.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.BMW_Data = bmwcars;
            var toyotacars = connection.Toyota.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => dealer_Active.Contains(x.Dealer_id)).ToList();
            ViewBag.Suzuki_Data = suzukicar;
            return View();
        }

        public IActionResult DealsPage()
        {
            var hondacar = connection.Honda.Where(x => x.Status == "Deal").ToList();
            ViewBag.Honda_Data = hondacar;
            var bmwcars = connection.BMW.Where(x => x.Status == "Deal").ToList();
            ViewBag.BMW_Data = bmwcars;
            var toyotacars = connection.Toyota.Where(x => x.Status == "Deal").ToList();
            ViewBag.Toyota_Data = toyotacars;
            var suzukicar = connection.Suzuki.Where(x => x.Status == "Deal").ToList();
            ViewBag.Suzuki_Data = suzukicar;
            var deals = connection.All_Deals.ToList();
            return View(deals);
        }
    }
}
