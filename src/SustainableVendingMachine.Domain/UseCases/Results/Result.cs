using System;
using System.Collections.Generic;
using System.Text;

namespace SustainableVendingMachine.Domain.UseCases.Results
{
    public class Result
    {
        private readonly bool _isSuccessful;

        public Result(string message, bool isSuccessful = true)
        {
            Message = message;
            _isSuccessful = isSuccessful;
        }

        public bool HasFailed => !_isSuccessful;
        public string Message { get; }
    }
}
