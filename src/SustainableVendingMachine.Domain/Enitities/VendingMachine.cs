namespace SustainableVendingMachine.Domain.Enitities
{
    public class VendingMachine
    {
        public Inventory Inventory { get; set; }
        public Purse Purse { get; set; }
        public Purchase CurrentPurchase { get; set; }
    }
}
