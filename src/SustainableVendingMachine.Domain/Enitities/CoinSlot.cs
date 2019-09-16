namespace SustainableVendingMachine.Domain.Enitities
{
    public class CoinSlot
    {
        public CoinSlot(Coin coin)
        {
            Coin = coin;
            Amount = 1;
        }

        public Coin Coin { get; }
        public int Amount { get; set; }
    }
}