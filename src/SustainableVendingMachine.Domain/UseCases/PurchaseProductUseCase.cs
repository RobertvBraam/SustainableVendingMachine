using SustainableVendingMachine.Domain.Entities;
using SustainableVendingMachine.Domain.Helpers;
using SustainableVendingMachine.Domain.UseCases.Results;

namespace SustainableVendingMachine.Domain.UseCases
{
    public class PurchaseProductUseCase
    {
        private readonly VendingMachine _vendingMachine;

        public PurchaseProductUseCase(VendingMachine vendingMachine)
        {
            Guard.AgainstNull(vendingMachine, nameof(vendingMachine));

            _vendingMachine = vendingMachine;
        }

        public InsertCoinResult InsertCoin(Coin coin)
        {
            Guard.AgainstNull(coin, nameof(coin));

            var coinInsertionSuccessful = _vendingMachine.InsertCoin(coin);
            var currentAmount = _vendingMachine.GetAmount();

            return new InsertCoinResult($"Coin added: €{(decimal)coin}", currentAmount, coinInsertionSuccessful);
        }

        public CancelPurchaseResult CancelPurchase()
        {
            var coinsReturned = _vendingMachine.ReturnCoinsFromPayment();

            return new CancelPurchaseResult("Purchase has been canceled", coinsReturned);
        }

        public PurchaseProductResult PurchaseProduct(Product product)
        {
            Guard.AgainstNull(product, nameof(product));

            var amountOfMoney = _vendingMachine.GetAmount();

            if (amountOfMoney < product.Price)
            {
                return new PurchaseProductResult($"Not enough money to buy product: {product.Name} -> €{product.Price}", product, PurchaseFailedType.InsufficientCoins);
            }

            var productAvailable = _vendingMachine.CheckProductAvailability(product);

            if (!productAvailable)
            {
                return new PurchaseProductResult($"Product not available: {product.Name}", product, PurchaseFailedType.ProductOutOfStock);
            }

            var suffienctCoins = _vendingMachine.CheckSufficientCoinsToReturn(product.Price);

            if (suffienctCoins)
            {
                var coinsReturned = _vendingMachine.ExcecutePayment(product);

                return new PurchaseProductResult($"Purchased product: {product.Name}", product, coinsReturned);
            }

            return new PurchaseProductResult($"Insufficient in store to return for product: {product.Name}", product, PurchaseFailedType.InsufficientCoinsToReturn);
        }
    }
}
