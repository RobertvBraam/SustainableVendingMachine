using System.Collections.Generic;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class Purse
    {
        public IEnumerable<CoinSlot> CoinSlots { get; set; }
    }
}