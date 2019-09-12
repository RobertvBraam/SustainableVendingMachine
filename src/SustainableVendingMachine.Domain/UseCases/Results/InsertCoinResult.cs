namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class InsertCoinResult : Result
    {
        public InsertCoinResult(string message, bool isSuccessful = true) : base(message, isSuccessful)
        {
        }
    }
}