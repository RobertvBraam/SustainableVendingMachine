using System.Collections.Generic;
using System.Linq;

namespace SustainableVendingMachine.Domain.Enitities
{
    public class Purchase
    {
        public List<CoinSlot> InsertedCoins { get; set; } = new List<CoinSlot>();
        public Product SelectedProduct { get; set; }
    }
}