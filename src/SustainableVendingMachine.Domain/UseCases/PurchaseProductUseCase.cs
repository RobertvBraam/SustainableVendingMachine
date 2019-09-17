using SustainableVendingMachine.Domain.Enitities;
using SustainableVendingMachine.Domain.UseCases.Results;

namespace SustainableVendingMachine.Domain.UseCases
{
    public class PurchaseProductUseCase
    {
        private readonly VendingMachine _vendingMachine;

        public PurchaseProductUseCase(VendingMachine vendingMachine)
        {
            _vendingMachine = vendingMachine;
        }

        public InsertCoinResult InsertCoin(Coin coin)
        {
            var coinInsertionSuccessful = _vendingMachine.InsertCoin(coin);
            var currentAmount = _vendingMachine.GetAmount();

            return new InsertCoinResult($"Coin processed in the payment: {coin}", currentAmount, coinInsertionSuccessful);
        }

        public CancelPurchaseResult CancelPurchase()
        {
            var coinsReturned = _vendingMachine.ReturnCoinsFromPayment();

            return new CancelPurchaseResult("Purchase has been canceled", coinsReturned);
        }

        public PurchaseProductResult PurchaseProduct(Product product)
        {
            var availableProduct = _vendingMachine.CheckProductAvailability(product);

            if (availableProduct.Amount == 0)
            {
                return new PurchaseProductResult($"Product not available: {product}", product);
            }

            var suffienctCoins = _vendingMachine.CheckSufficientCoinsToReturn(availableProduct.Price);

            if (suffienctCoins)
            {
                return new PurchaseProductResult($"Purchased product: {product}", product, true);
            }

            return new PurchaseProductResult($"Insufficient coins for product: {product}", product);
        }
    }
}
