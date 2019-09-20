using System.Collections.Generic;
using SustainableVendingMachine.Domain.Entities;

namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class PurchaseProductResult : Result
    {
        public Product ProductPurchased { get; }
        public List<CoinSlot> CoinsReturned { get; }
        public PurchaseFailedType PurchaseFailedReason { get; }

        public PurchaseProductResult(string message, Product productPurchased, List<CoinSlot> coinsReturned) : base(message, true)
        {
            ProductPurchased = productPurchased;
            CoinsReturned = coinsReturned;
            PurchaseFailedReason = PurchaseFailedType.Successful;
        }
        
        public PurchaseProductResult(string message, Product productPurchased, PurchaseFailedType purchaseFailedReason) : base(message, false)
        {
            ProductPurchased = productPurchased;
            PurchaseFailedReason = purchaseFailedReason;
            CoinsReturned = new List<CoinSlot>();
        }
    }

    public enum PurchaseFailedType
    {
        Successful,
        ProductOutOfStock,
        InsufficientCoinsToReturn,
        InsufficientCoins
    }
}