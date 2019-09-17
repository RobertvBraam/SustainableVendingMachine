namespace SustainableVendingMachine.Domain.Enitities
{
    public class ProductSlot
    {
        public ProductSlot(Product product, int amount = 1)
        {
            Product = product;
            Amount = amount;
        }

        public Product Product { get; }
        public int Amount { get; set; }
        public decimal Price => (int) Product / 100m;
    }
}