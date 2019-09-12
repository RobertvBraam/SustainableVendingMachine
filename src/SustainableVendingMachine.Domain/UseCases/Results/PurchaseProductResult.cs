namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class PurchaseProductResult : Result
    {
        public PurchaseProductResult(string message, bool isSuccessful = true) : base(message, isSuccessful)
        {
        }
    }
}