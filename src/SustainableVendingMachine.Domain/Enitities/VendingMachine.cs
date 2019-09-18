using System.Collections.Generic;
using System.Linq;
using SustainableVendingMachine.Domain.Enitities.Products;

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

        public bool CheckProductAvailability(Product productToCheck)
        {
            var productAvailable = _inventory.Exists(product => product.Name == productToCheck.Name);

            return productAvailable;
        }

        public bool CheckSufficientCoinsToReturn(decimal productPrice)
        {
            var coinsAvailable = DeepCopyOrderedPurse();
            var amountToReturn = GetAmount() - productPrice;

            if (amountToReturn > 0)
            {
                (List<Coin> coinsToReturn, decimal leftoverMoney) result = calculateCoinsReturned(coinsAvailable, amountToReturn);

                return result.leftoverMoney == 0.00m;
            }

            return true;
        }

        public void ExcecutePayment(decimal productPrice)
        {
            var coinsAvailable = DeepCopyOrderedPurse();
            var amountToReturn = GetAmount() - productPrice;

            (List<Coin> coinsToReturn, decimal leftoverMoney) result = calculateCoinsReturned(coinsAvailable, amountToReturn);

            _currentPurchase.Clear();

            foreach (var coin in result.coinsToReturn)
            {
                var coinSlot = _purse.Single(slot => slot.Coin == coin);

                if (coinSlot.Amount == 1)
                {
                    _purse.Remove(coinSlot);
                }
                else
                {
                    coinSlot.Amount--;
                }

                InsertCoin(coinSlot.Coin);
            }
        }

        private (List<Coin> coinsToReturn, decimal leftoverMoney) calculateCoinsReturned(List<CoinSlot> coinsAvailable, decimal amountToReturn)
        {
            var coinsToReturn = new List<Coin>();
            var coinSlot = coinsAvailable.FirstOrDefault();

            if (coinSlot == null)
            {
                return (coinsToReturn, amountToReturn);
            }

            while (0 < coinSlot.Amount)
            {
                if (amountToReturn - coinSlot.Value >= 0)
                {
                    coinSlot.Amount--;
                    amountToReturn -= coinSlot.Value;
                    coinsToReturn.Add(coinSlot.Coin);
                }
                else
                {
                    break;
                }
            }

            if (amountToReturn > 0)
            {
                coinsAvailable.RemoveAt(0);
                var result = calculateCoinsReturned(coinsAvailable.ToList(), amountToReturn);
                coinsToReturn.AddRange(result.coinsToReturn);
                amountToReturn = result.leftoverMoney;
            }

            return (coinsToReturn, amountToReturn);
        }

        private decimal ConvertCoinToEuros(Coin coin) => (int)coin / 100m;
        private List<CoinSlot> DeepCopyOrderedPurse() => _purse.ConvertAll(slot => new CoinSlot(slot.Coin, slot.Amount)).OrderByDescending(coin => coin.Value).ToList();
    }
}
