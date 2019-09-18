using SustainableVendingMachine.Domain.Enitities.Products;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class ProductSlot
    {
        public ProductSlot(Product product, int amount = 1)
        {
            Name = product.Name;
            Price = product.Price;
            Amount = amount;
        }

        public string Name { get; }
        public int Amount { get; set; }
        public decimal Price { get; }
    }
}