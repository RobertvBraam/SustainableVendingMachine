using System.Collections.Generic;

namespace SustainableVendingMachine.Domain.Enitity
{
    public class Purchase
    {
        public IEnumerable<CoinSlot> InsertedCoins { get; set; }
        public Product SelectedProduct { get; set; }
    }
}