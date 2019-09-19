using System.Collections.Generic;
using SustainableVendingMachine.Domain.Entities;

namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class PurchaseProductResult : Result
    {
        public Product ProductPurchased { get; }
        public List<CoinSlot> CoinsReturned { get; }

        public PurchaseProductResult(string message, Product productPurchased, List<CoinSlot> coinsReturned, bool isSuccessful = true) : base(message, isSuccessful)
        {
            ProductPurchased = productPurchased;
            CoinsReturned = coinsReturned;
        }
        
        public PurchaseProductResult(string message, Product productPurchased, bool isSuccessful = false) : base(message, isSuccessful)
        {
            ProductPurchased = productPurchased;
            CoinsReturned = new List<CoinSlot>();
        }
    }
}