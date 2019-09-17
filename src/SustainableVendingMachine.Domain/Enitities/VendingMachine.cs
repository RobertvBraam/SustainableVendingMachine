using System.Collections.Generic;
using System.Linq;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class VendingMachine
    {
        private readonly List<ProductSlot> _inventory;
        private readonly List<CoinSlot> _purse;
        private readonly List<CoinSlot> _currentPurchase = new List<CoinSlot>();

        public VendingMachine(List<ProductSlot> inventory, List<CoinSlot> purse)
        {
            _inventory = inventory;
            _purse = purse;
        }
        
        public bool InsertCoin(Coin coin)
        {
            var maximumAmountOfCoins = GetAmount() + ConvertCoinToEuros(coin);

            if (maximumAmountOfCoins > 2.00m)
            {
                return false;
            }

            var existingSlot = _currentPurchase.SingleOrDefault(slot => slot.Coin == coin);

            if (existingSlot is null)
            {
                _currentPurchase.Add(new CoinSlot(coin));
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

            foreach (var insertedCoin in _currentPurchase)
            {
                var amount = insertedCoin.Amount * insertedCoin.Value;
                result = amount + result;
            }

            return result;
        }

        public List<Coin> ReturnCoinsFromPayment()
        {
            var result = new List<Coin>();

            foreach (var insertedCoin in _currentPurchase)
            {
                for (int i = 0; i < insertedCoin.Amount; i++)
                {
                    result.Add(insertedCoin.Coin);
                }
            }

            return result;
        }

        private decimal ConvertCoinToEuros(Coin coin) => (int)coin / 100m;
    }
}
