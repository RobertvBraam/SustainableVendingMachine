namespace SustainableVendingMachine.Domain.Entities
{
    public class Product
    {
        public string Name { get; }
        public decimal Price { get; }

        public static readonly Product Tea = new Product("Tea", 1.30m);
        public static readonly Product Espresso = new Product("Espresso", 1.80m);
        public static readonly Product Juice = new Product("Juice", 1.80m);
        public static readonly Product ChickenSoup = new Product("ChickenSoup", 1.80m);

        private Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}