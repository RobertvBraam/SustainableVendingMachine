using System.Collections.Generic;
using SustainableVendingMachine.Domain.Entities;

namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class CancelPurchaseResult : Result
    {
        public List<Coin> CoinsReturned { get; }

        public CancelPurchaseResult(string message, List<Coin> coinsReturned, bool isSuccessful = true) : base(message, isSuccessful)
        {
            CoinsReturned = coinsReturned;
        }
    }
}