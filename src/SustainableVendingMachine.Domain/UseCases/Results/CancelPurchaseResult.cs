namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class CancelPurchaseResult : Result
    {
        public CancelPurchaseResult(string message, bool isSuccessful = true) : base(message, isSuccessful)
        {
        }
    }
}