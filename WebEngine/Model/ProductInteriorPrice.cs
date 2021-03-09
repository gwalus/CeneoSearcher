namespace WebEngine.Model
{

    public class Offers
    {
        public double lowPrice { get; set; }
    }

    public class ProductInteriorPrice
    {
        public Offers offers { get; set; }
    }
}
