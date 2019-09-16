using System.Collections.Generic;
using System.Linq;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class VendingMachine
    {
        public List<ProductSlot> Inventory { get; set; } = new List<ProductSlot>();
        public Purse Purse { get; set; } = new Purse();
        public List<CoinSlot> CurrentPurchase { get; set; } = new List<CoinSlot>();

        public bool InsertCoin(Coin coin)
        {
            var maximumAmountOfCoins = GetAmount() + ConvertCoinToEuros(coin);

            if (maximumAmountOfCoins > 2.00m)
            {
                return false;
            }

            var existingSlot = CurrentPurchase.SingleOrDefault(slot => slot.Coin == coin);

            if (existingSlot is null)
            {
                CurrentPurchase.Add(new CoinSlot(coin));
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

            foreach (var insertedCoin in CurrentPurchase)
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

            foreach (var insertedCoin in CurrentPurchase)
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
