 using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    class Player
    {
        private int _gold;

        private Item[] _inventory;


        public int Gold { get { return _gold; } }

        public Item[] Inventory { get { return _inventory; } set{ _inventory = value;} }

        public Player(int gold)
        {
            _gold = gold;
            _inventory = Inventory;
        }

        public void Buy(Item item)
        {
            int newInventorySize = _inventory.Length + 1;
            Item[] placeHolder = new Item[newInventorySize];

            if (_gold <= item.Cost)
            {
                Console.WriteLine("You purchased the " + item.Name + "!");
                _gold -= item.Cost;
                for (int i = 0; i < _inventory.Length; i++) 
                    placeHolder[i] = _inventory[i];
                placeHolder[newInventorySize] = item;
                _inventory = placeHolder;

            }
            else if (_gold > item.Cost)
                Console.WriteLine("You Don't Have Enough For That.");

        }

        public string[] GetItemNames()
        {
            string[] names = new string[_inventory.Length];
            if (_inventory.Length != 0)
                for (int i = 0; i <= _inventory.Length; i++)
                    names[i] = _inventory[i].Name;
            return names;
        }

        public void Save(StreamWriter save)
        {

        }

        public bool Load(StreamWriter load)
        {

        }
    }
}
