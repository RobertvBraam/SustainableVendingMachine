using System.Collections.Generic;
using SustainableVendingMachine.Domain.Enitities;

namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class PurchaseProductResult : Result
    {
        public Product ProductPurchased { get; }
        public List<Coin> CoinsReturned { get; }

        public PurchaseProductResult(string message, Product productPurchased, List<Coin> coinsReturned, bool isSuccessful = true) : base(message, isSuccessful)
        {
            ProductPurchased = productPurchased;
            CoinsReturned = coinsReturned;
        }
    }
}