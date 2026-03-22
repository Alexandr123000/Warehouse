using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    internal class SoldProductsTable
    {
        public SoldProductsTable(int Id, string Name, string Type, double Price, int Amount, double TotalPrice)
        {
            this.ID = Id;
            this.Name = Name;
            this.Type = Type;
            this.Price = Price;
            this.Amount = Amount;
            this.TotalPrice = TotalPrice;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
    }
}
