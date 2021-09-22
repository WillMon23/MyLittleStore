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

        public Shop(params Item[] items)
        {
            _inventory = items;
            _gold = 0;
        }

        public bool Sell(Player player, int item)
        {
            bool purche = false;
            player.Buy(_inventory[item]);

            if (player.Gold <= _inventory[item].Cost)
                purche = true;
            return purche;
            

        }

        public string[] GetItemNames()
        {
            string[] names = new string[_inventory.Length];
            for (int i = 0; i <= _inventory.Length; i++)
                names[i] = _inventory[i].Name;
            return names;

        }
    }    
}
