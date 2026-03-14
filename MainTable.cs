using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    class MainTable
    {
        public MainTable(int Id, string Name, string Type, double Price, double PurchasePrice, int Amount, double TotalPrice)
        {
            this.ID = Id;
            this.Name = Name;
            this.Type = Type;
            this.Price = Price;
            this.PurchasePrice = PurchasePrice;
            this.Amount = Amount;
            this.TotalPrice = TotalPrice;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public double PurchasePrice { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
        
    }
}