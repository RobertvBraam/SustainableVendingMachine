using SustainableVendingMachine.Domain.Helpers;

namespace SustainableVendingMachine.Domain.Entities
{
    public class CoinSlot
    {
        /// <summary>
        /// Represents a set of similar type of coin.
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="amount"></param>
        public CoinSlot(Coin coin, int amount = 1)
        {
            Guard.AgainstNull(coin, nameof(coin));

            Coin = coin;
            Amount = amount;
        }

        public Coin Coin { get; }
        public int Amount { get; set; }
        public decimal Value => Coin;
    }
}