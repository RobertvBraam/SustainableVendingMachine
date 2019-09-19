namespace SustainableVendingMachine.Domain.Entities
{
    public class CoinSlot
    {
        public CoinSlot(Coin coin, int amount = 1)
        {
            Coin = coin;
            Amount = amount;
        }

        public Coin Coin { get; }
        public int Amount { get; set; }
        public decimal Value => Coin;
    }
}