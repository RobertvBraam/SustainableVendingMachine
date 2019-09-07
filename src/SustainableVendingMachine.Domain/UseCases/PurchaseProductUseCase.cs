using SustainableVendingMachine.Domain.Enitity;
using SustainableVendingMachine.Domain.UseCases.Result;
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
            return new CancelPurchaseResult();
        }

        public PurchaseProductResult PurchaseProduct()
        {
            return new PurchaseProductResult();
        }
    }
}
