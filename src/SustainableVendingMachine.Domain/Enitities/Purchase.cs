using System.Collections.Generic;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class Purchase
    {
        public IEnumerable<CoinSlot> InsertedCoins { get; set; }
        public Product SelectedProduct { get; set; }
    }
}