using System.Collections.Generic;
using System.Linq;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class VendingMachine
    {
        public Inventory Inventory { get; set; } = new Inventory();
        public Purse Purse { get; set; } = new Purse();
        public Purchase CurrentPurchase { get; set; } = new Purchase();

        public bool InsertCoin(Coin coin)
        {
            var maximumAmountOfCoins = GetAmount() + ConvertCoinToEuros(coin);

            if (maximumAmountOfCoins > 2.00m)
            {
                return false;
            }

            var existingSlot = CurrentPurchase.InsertedCoins.SingleOrDefault(slot => slot.Coin == coin);

            if (existingSlot is null)
            {
                CurrentPurchase.InsertedCoins.Add(new CoinSlot(coin));
            }
            else
            {
                existingSlot.Amount++;
            }

            return true;
        }

        public decimal GetAmount()
        {
            decimal result = 0;

            foreach (var insertedCoin in CurrentPurchase.InsertedCoins)
            {
                var amount = insertedCoin.Amount * ConvertCoinToEuros(insertedCoin.Coin);
                result = amount + result;
            }

            return result;
        }

        private decimal ConvertCoinToEuros(Coin coin) => (int) coin / 100m;

        public List<Coin> ReturnCoinsFromPayment()
        {
            var result = new List<Coin>();

            foreach (var insertedCoin in CurrentPurchase.InsertedCoins)
            {
                for (int i = 0; i < insertedCoin.Amount; i++)
                {
                    result.Add(insertedCoin.Coin);
                }
            }

            return result;
        }
    }
}
