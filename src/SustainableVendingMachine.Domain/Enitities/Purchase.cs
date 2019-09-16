using System.Collections.Generic;
using System.Linq;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class Purchase
    {
        public List<CoinSlot> InsertedCoins { get; set; } = new List<CoinSlot>();
        public Product SelectedProduct { get; set; }

        public void InsertCoin(Coin coin)
        {
            var existingSlot = InsertedCoins.SingleOrDefault(slot => slot.Coin == coin);

            if (existingSlot is null)
            {
                InsertedCoins.Add(new CoinSlot(coin));
            }
            else
            {
                existingSlot.Amount++;
            }
        }

        public decimal GetAmount()
        {
            decimal result = 0;

            foreach (var insertedCoin in InsertedCoins)
            {
                var insertedCoinCoin = (int)insertedCoin.Coin;
                var amount = (insertedCoin.Amount * insertedCoinCoin) / 100m;
                result = amount + result;
            }

            return result;
        }
    }
}