using SustainableVendingMachine.Domain.Enitities;
using SustainableVendingMachine.Domain.Enitities.Products;
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
            var amount = _vendingMachine.GetAmount();

            if (amount - product.Price <= 0)
            {
                return new PurchaseProductResult($"Not enough coins to buy product: {product.Name}", product);
            }

            var productAvailable = _vendingMachine.CheckProductAvailability(product);

            if (!productAvailable)
            {
                return new PurchaseProductResult($"Product not available: {product}", product);
            }

            var suffienctCoins = _vendingMachine.CheckSufficientCoinsToReturn(product.Price);

            if (suffienctCoins)
            {
                _vendingMachine.ExcecutePayment(product.Price);
                
                var coinsReturned = _vendingMachine.ReturnCoinsFromPayment();

                return new PurchaseProductResult($"Purchased product: {product.Name}", product, coinsReturned);
            }

            return new PurchaseProductResult($"Insufficient coins for product: {product.Name}", product);
        }
    }
}
