namespace SustainableVendingMachine.Domain.Enitities
{
    public class ProductSlotUnavailable : ProductSlot
    {
        public ProductSlotUnavailable(Product product) : base(product)
        {
            Amount = 0;
        }
    }
}