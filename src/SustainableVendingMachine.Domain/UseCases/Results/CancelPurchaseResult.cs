using System.Collections.Generic;
using SustainableVendingMachine.Domain.Entities;

namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class CancelPurchaseResult : Result
    {
        public List<CoinSlot> CoinsReturned { get; }

        public CancelPurchaseResult(string message, List<CoinSlot> coinsReturned, bool isSuccessful = true) : base(message, isSuccessful)
        {
            CoinsReturned = coinsReturned;
        }
    }
}