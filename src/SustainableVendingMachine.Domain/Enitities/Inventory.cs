using System.Collections.Generic;

namespace SustainableVendingMachine.Domain.Enitity
{
    public class Inventory
    {
        public IEnumerable<ProductSlot> ProductSlots { get; set; }
    }
}