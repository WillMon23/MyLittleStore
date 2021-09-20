using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{  
    public struct Item
    {
        public int Cost;
        public string Name;
    }
    class Shop
    {
        private int _gold;

        private Item[] _inventory;

        public Item[] Inventory;

        public Shop(Item[] items)
        {

        }

        public bool Sell(Player player, int gold)
        {
            return false;
        }

        public string[] GetItemNames()
        {

        }
    }    
}
