namespace ShoppingCart.Model
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string shippedFrom { get; set; }
        public double weight { get; set; }

        public Product(string name, double price, string shippedFrom, double weight)
        {
            this.name = name;
            this.price = price;
            this.shippedFrom = shippedFrom;
            this.weight = weight;
        }

        public override string ToString()
        {
            return $"{nameof(id)}:{id},{nameof(name)}:{name},{nameof(price)}:{price},{nameof(shippedFrom)}:{shippedFrom},{nameof(weight)}:{weight}";
        }
    }
}