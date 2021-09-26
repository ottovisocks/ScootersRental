namespace ScooterRent.Models
{
    public class Scooter
    {
        private string _id;
        private decimal _pricePerMinute;
        private bool _isRented;

        public Scooter(string id, decimal pricePerMinute, bool isRented = false)
        {
            _id = id;
            _pricePerMinute = pricePerMinute;
            _isRented = isRented;
        }

        public string Id
        {
            get { return _id; }
        }

        public decimal PricePerMinute
        {
            get { return _pricePerMinute; }
        }

        public bool IsRented
        {
            get { return _isRented; }
            set { _isRented = value; }
        }
    }
}
