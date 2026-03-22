using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    class TypeTable
    {
        public TypeTable(int Id, string Name)
        {
            this.ID = Id;
            this.Name = Name;
        }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}