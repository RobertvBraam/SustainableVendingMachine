using System.Collections.Generic;
using SustainableVendingMachine.Domain.Enitities;

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