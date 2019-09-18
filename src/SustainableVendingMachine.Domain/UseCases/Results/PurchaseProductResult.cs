using System.Collections.Generic;
using SustainableVendingMachine.Domain.Enitities;
using SustainableVendingMachine.Domain.Enitities.Products;

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
        
        public PurchaseProductResult(string message, Product productPurchased, bool isSuccessful = false) : base(message, isSuccessful)
        {
            ProductPurchased = productPurchased;
            CoinsReturned = new List<Coin>();
        }
    }
}