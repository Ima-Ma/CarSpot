namespace DriveX_Showcase_Admin.Models
{
    public class Toyata
    {
        public int Id { get; set; }

        public string CarName { get; set; }

        public int CarPrice { get; set; }

        public string CarDescription { get; set; }

        public string FuelType { get; set; }

        public string Variant { get; set; }

        public string Mileage { get; set; }

        public string Colors { get; set; }

        public string Power { get; set; }

        public string EngineCapacity { get; set; }

        public string Image1 { get; set; }

        public string RatingExterior { get; set; }

        public string RatingComfort { get; set; }

        public string RatingPerformance { get; set; }

        public string RatingFuelEconomy { get; set; }

        public string Dealer_id { get; set; }
        public string PDF { get; set; }

        public string Status { get; set; }

        public Toyata(string carName, int carPrice, string carDescription, string fuelType, string variant, string mileage, string colors, string power, string engineCapacity, string image1, string ratingExterior, string ratingComfort, string ratingPerformance, string ratingFuelEconomy, string dealer_id, string pDF, string status)
        {
            CarName = carName;
            CarPrice = carPrice;
            CarDescription = carDescription;
            FuelType = fuelType;
            Variant = variant;
            Mileage = mileage;
            Colors = colors;
            Power = power;
            EngineCapacity = engineCapacity;
            Image1 = image1;
            RatingExterior = ratingExterior;
            RatingComfort = ratingComfort;
            RatingPerformance = ratingPerformance;
            RatingFuelEconomy = ratingFuelEconomy;
            Dealer_id = dealer_id;
            PDF = pDF;
            Status = status;
        }
    }
}
