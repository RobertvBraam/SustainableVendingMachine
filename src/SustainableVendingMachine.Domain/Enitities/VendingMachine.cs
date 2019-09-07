using System;
using System.IO;
using System.Text;

namespace SustainableVendingMachine.Domain.Enitity
{
    public class VendingMachine
    {
        public Inventory Inventory { get; set; }
        public Purse Purse { get; set; }
        public Purchase CurrentPurchase { get; set; }
    }
}
