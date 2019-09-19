using SustainableVendingMachine.Domain.Helpers;

namespace SustainableVendingMachine.Domain.Entities
{
    public class ProductSlot
    {
        /// <summary>
        /// Represents a set of similar type of product.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        public ProductSlot(Product product, int amount = 1)
        {
            Guard.AgainstNull(product, nameof(product));

            Name = product.Name;
            Price = product.Price;
            Amount = amount;
        }

        public string Name { get; }
        public int Amount { get; set; }
        public decimal Price { get; }
    }
}