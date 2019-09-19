using System;
using System.Collections.Generic;
using System.Linq;

namespace SustainableVendingMachine.Domain.Helpers
{
    internal static class Guard
    {
        public static void AgainstNull<T>(T value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name, "Value cannot be null");
            }
        }
    }
}
