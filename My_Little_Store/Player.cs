 using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    class Player
    {
        private  int _gold;

        private Item[] _inventory;


        public int Gold { get { return _gold; } }


        public Player(int gold)
        {
            _gold = gold;
            _inventory = new Item[0];
        }

        public void Buy(Item item)
        {
            if (_gold >= item.Cost)
            {
                int newInventorySize = _inventory.Length + 1; 
                Item[] placeHolder = new Item[newInventorySize];

                _gold -= item.Cost;

                for (int i = 0; i < _inventory.Length; i++)
                    placeHolder[i] = _inventory[i];
                placeHolder[_inventory.Length] = item;
                _inventory = placeHolder;

            }

        }

        public string[] GetItemNames()
        {
            
            string[] names = new string[_inventory.Length];
            if(_inventory.Length != 0)
                for (int i = 0; i < _inventory.Length; i++)
                    names[i] = _inventory[i].Name;
            return names;
        }

        public void Save(StreamWriter save)
        {
            save.WriteLine(_gold);
            save.WriteLine(_inventory.Length);

            foreach(Item item in _inventory)
            {
                save.WriteLine(item.Name);

                save.WriteLine(item.Cost);

            }
            save.Close();
        }

        public bool Load(StreamReader load)
        {
            
            int arrySize = 0;

            string loadGold = load.ReadLine();

            if(!int.TryParse(loadGold, out _gold))
                return false;

            if (!int.TryParse(load.ReadLine(), out arrySize))
                return false;

            _inventory = new Item[arrySize];

            for(int i = 0; i < _inventory.Length; i++)
            {
                _inventory[i].Name = load.ReadLine();

                if (!int.TryParse(load.ReadLine(), out _inventory[i].Cost))
                    return  false;

                
            }
            load.Close();

            return true;
        }
    }
}
