using SustainableVendingMachine.Domain.Enitities;
using SustainableVendingMachine.Domain.UseCases.Results;

namespace SustainableVendingMachine.Domain.UseCases
{
    public class PurchaseProductUseCase
    {
        public InsertCoinResult InsertCoin(Coin coin)
        {
            return new InsertCoinResult($"Coin has been added to the payment: {coin}");
        }

        public CancelPurchaseResult CancelPurchase()
        {
            return new CancelPurchaseResult("Purchase is canceled");
        }

        public PurchaseProductResult PurchaseProduct(Product product)
        {
            return new PurchaseProductResult($"Purchased product: {product}");
        }
    }
}
