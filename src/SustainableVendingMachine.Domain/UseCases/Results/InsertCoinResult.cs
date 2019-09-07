namespace SustainableVendingMachine.Domain.UseCases.Result
{
    public class InsertCoinResult
    {
        private readonly bool _isSuccessful;

        public InsertCoinResult(string message, bool isSuccessful = true)
        {
            Message = message;
            _isSuccessful = isSuccessful;
        }

        public bool HasFailed => !_isSuccessful;
        public string Message { get; }
    }
}