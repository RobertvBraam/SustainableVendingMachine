using System;
using System.Collections.Generic;
using System.Linq;
using SustainableVendingMachine.Domain.Helpers;

namespace SustainableVendingMachine.Domain.Entities
{
    public class VendingMachine
    {
        private readonly List<ProductSlot> _inventory;
        private readonly List<CoinSlot> _purse;
        private readonly List<CoinSlot> _currentPurchase = new List<CoinSlot>();

        public VendingMachine(List<ProductSlot> inventory, List<CoinSlot> purse)
        {
            Guard.AgainstNull(inventory, nameof(inventory));
            Guard.AgainstNull(purse, nameof(purse));

            _inventory = inventory;
            _purse = purse;
        }

        internal bool InsertCoin(Coin coin)
        {
            var maximumAmountOfCoins = GetAmount() + coin;

            if (maximumAmountOfCoins > 2.00m)
            {
                return false;
            }

            var existingSlot = _currentPurchase.SingleOrDefault(slot => slot.Coin.Equals(coin));

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

        internal decimal GetAmount()
        {
            decimal result = 0;

            foreach (var insertedCoin in _currentPurchase)
            {
                var amount = insertedCoin.Amount * insertedCoin.Value;
                result = amount + result;
            }

            return result;
        }

        internal List<CoinSlot> ReturnCoinsFromPayment()
        {
            var result = new List<CoinSlot>();

            result.AddRange(_currentPurchase);

            _currentPurchase.Clear();

            return result;
        }

        internal bool CheckProductAvailability(Product productToCheck)
        {
            var productAvailable = _inventory.SingleOrDefault(product => product.Name == productToCheck.Name);

            return productAvailable?.Amount != null && productAvailable.Amount > 0;
        }

        internal bool CheckSufficientCoinsToReturn(decimal productPrice)
        {
            var coinsAvailable = DeepCopyOrderedPurse();
            var amountToReturn = GetAmount() - productPrice;

            (List<CoinSlot> coinsToReturn, decimal leftoverMoney) result =
                calculateCoinsReturnedV2(coinsAvailable, amountToReturn);

            return result.leftoverMoney == 0.00m;
        }

        internal List<CoinSlot> ExcecutePayment(Product product)
        {
            var coinsAvailable = DeepCopyOrderedPurse();
            var amountToReturn = GetAmount() - product.Price;

            (List<CoinSlot> coinsToReturn, decimal leftoverMoney) result = calculateCoinsReturnedV2(coinsAvailable, amountToReturn);

            _currentPurchase.Clear();

            foreach (var coin in result.coinsToReturn)
            {
                var coinSlot = _purse.Single(slot => slot.Coin.Equals(coin.Coin));

                coinSlot.Amount -= coin.Amount;
            }

            RemoveProductFromInventory(product);

            return result.coinsToReturn;
        }

        private void RemoveProductFromInventory(Product productToRemove)
        {
            var productAvailable = _inventory.SingleOrDefault(product => product.Name == productToRemove.Name);

            if (productAvailable != null)
            {
                productAvailable.Amount--;
            }
        }

        /// <summary>
        /// The first version of calculating the coins returned, but this was less efficient and did not leverage the CoinSlot.
        /// </summary>
        /// <param name="coinsAvailable"></param>
        /// <param name="amountToReturn"></param>
        /// <returns></returns>
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

        /// <summary>
        /// The second version of calculating the coins returned, it calculates what amount of coins that need to be returned.
        /// </summary>
        /// <param name="coinsAvailable"></param>
        /// <param name="amountToReturn"></param>
        /// <returns></returns>
        private (List<CoinSlot> coinsToReturn, decimal leftoverMoney) calculateCoinsReturnedV2(List<CoinSlot> coinsAvailable, decimal amountToReturn)
        {
            var coinsToReturn = new List<CoinSlot>();
            
            foreach (var coinSlot in coinsAvailable)
            {
                if (coinSlot.Value > amountToReturn)
                {
                    continue;
                }

                var amountOfMoneyNotAvailable = amountToReturn % coinSlot.Value;
                var amountOfMoneyAvailable = amountToReturn - amountOfMoneyNotAvailable;
                var amountOfCoinsNeeded = amountOfMoneyAvailable / coinSlot.Value;

                if (coinSlot.Amount - (int)amountOfCoinsNeeded >= 0)
                {
                    coinsToReturn.Add(new CoinSlot(coinSlot.Coin, (int)amountOfCoinsNeeded));

                    amountToReturn = amountOfMoneyNotAvailable;
                }
                else
                {
                    var amountOfCoinMissing = (int) amountOfCoinsNeeded - coinSlot.Amount;
                    var amountOfMoneyMissing = amountOfCoinMissing * coinSlot.Value;

                    coinsToReturn.Add(new CoinSlot(coinSlot.Coin, coinSlot.Amount));

                    amountToReturn = amountOfMoneyNotAvailable + amountOfMoneyMissing;
                }
            }
            
            return (coinsToReturn, amountToReturn);
        }

        /// <summary>
        /// Creates a deep copy to make sure there is no reference to the objects withing the purse.
        /// </summary>
        /// <returns>Ordered list of CoinSlots</returns>
        private List<CoinSlot> DeepCopyOrderedPurse() => _purse.ConvertAll(slot => new CoinSlot(slot.Coin, slot.Amount)).OrderByDescending(coin => coin.Value).ToList();
    }
}
