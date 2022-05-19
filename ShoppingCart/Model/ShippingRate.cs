namespace ShoppingCart.Model
{
    public class ShippingRate
    {
        public int id { get; set; }
        public string country { get; set; }
        public int rate { get; set; }

        public ShippingRate(string country, int rate)
        {
            this.country = country;
            this.rate = rate;
        }

        public override string ToString()
        {
            return $"{nameof(id)}:{id},{nameof(country)}:{country},{nameof(rate)}:{rate}";
        }
    }
}