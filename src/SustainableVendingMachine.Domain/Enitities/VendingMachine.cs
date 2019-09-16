namespace SustainableVendingMachine.Domain.Enitities
{
    public class VendingMachine
    {
        public Inventory Inventory { get; set; } = new Inventory();
        public Purse Purse { get; set; } = new Purse();
        public Purchase CurrentPurchase { get; set; } = new Purchase();

        public decimal InsertCoin(Coin coin)
        {
            CurrentPurchase.InsertCoin(coin);

            return CurrentPurchase.GetAmount();
        }
    }
}
