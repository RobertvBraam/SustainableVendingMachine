using System.Collections.Generic;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class Inventory
    {
        public IEnumerable<ProductSlot> ProductSlots { get; set; }
    }
}