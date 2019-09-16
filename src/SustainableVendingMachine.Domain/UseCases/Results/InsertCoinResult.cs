namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class InsertCoinResult : Result
    {
        public decimal CurrentAmount { get; }

        public InsertCoinResult(string message, decimal currentAmount, bool isSuccessful = true) : base(message, isSuccessful)
        {
            CurrentAmount = currentAmount;
        }
    }
}