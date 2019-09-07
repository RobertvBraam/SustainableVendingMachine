using System.Collections.Generic;

namespace SustainableVendingMachine.Domain.Enitity
{
    public class Purse
    {
        public IEnumerable<CoinSlot> CoinSlots { get; set; }
    }
}