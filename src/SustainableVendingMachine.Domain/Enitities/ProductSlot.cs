namespace SustainableVendingMachine.Domain.Enitities
{
    public class ProductSlot
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}