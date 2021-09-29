 using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    /// <summary>
    /// The Decloration of what it means to be a player 
    /// </summary>
    class Player : Entity
    {
        // Gold the player will hold 
        private  int _gold;

        // Players Inventory, if they buy something it will be added here  
        private Item[] _inventory;    

        private float _defenseBoost;

        private float _attackBoost;

        //Gold set to a property only allowing othere classes to view the gold amount
        public int Gold { get { return _gold; } }
        
         public float AttackBoost { get { return _attackBoost; } set { _attackBoost = value; } }

        public float DefenseBoost { get { return _defenseBoost; } set { _defenseBoost = value; } }

        /// <summary>
        /// Creats a player costructer in order to set the players gold
        /// </summary>
        /// <param name="gold">Gold Set to the player</param>
        public Player(string name,float health, float attack, float defense, int gold ) : base( name, health, attack, defense, gold)
        {
            _gold = gold;

            _inventory = new Item[0];

            _attackBoost = HitPoint;

            _defenseBoost = Defense;

        }

   
        /// <summary>
        /// Function that places items they but into there inventory 
        /// </summary>
        /// <param name="item">Item that is being purchesed</param>
        public void Buy(Item item)
        {
            // if player has more gold or it equal to the item cost . . .
            if (_gold >= item.Cost)
            {
                //. . . creat a new int and set it to the curent size of the inventory plus one 
                int newInventorySize = _inventory.Length + 1; 
                //. . . creat a new class with the new size 
                Item[] placeHolder = new Item[newInventorySize];

                //. . . reduce the players gold buy the item cost 
                _gold -= item.Cost;

                //. . . for the many items that are in the players inventory . . .
                for (int i = 0; i < _inventory.Length; i++)
                    // . .. place it in the new array
                    placeHolder[i] = _inventory[i];
                //Once done add the new item bought

                placeHolder[_inventory.Length] = item;
                //Then replace the current array with the new array 
                _inventory = placeHolder;
            }
        }

        public void BonusItemsUse()
        {
            
            if(_inventory.Length != 0)
            {
                foreach (Item item in _inventory)
                {
                    if (item.Name == "Sword")
                        _attackBoost = +20;

                    else if (item.Name == "Shield")
                        _defenseBoost =+ 5;

                    else if (item.Name == "Health Potion")
                        HitPoint =+ 15;
                }

            }
        }

        /// <summary>
        /// Coolects all names listed in the array in order to be read 
        /// easier
        /// </summary>
        /// <returns>returns all the names currently listed in the array</returns>
        public string[] GetItemNames()
        {
            // Creats a temp array to hold the item names 
            string[] names = new string[_inventory.Length];
            //if the length of the array is not nothing . . .
            if(_inventory.Length != 0)
                //. . .for every item in the players inventory . . .
                for (int i = 0; i < _inventory.Length; i++)
                    // place that items name in the name array 
                    names[i] = _inventory[i].Name;
            //Just return the array when done
            return names;
        }

        /// <summary>
        /// Saves the values currently collected by the player
        /// </summary>
        /// <param name="save">Wehre to save all of the players information</param>
        public override void Save(StreamWriter save)
        {
            //Writes to file how much gold the player has
            save.WriteLine(_gold);
            //Writes to file how big the inventory of the player is 
            save.WriteLine(_inventory.Length);

            // foreach item in the players inventory. . .
            foreach(Item item in _inventory)
            {
                // . . . Save the items cost 
                save.WriteLine(item.Cost);
                //. . . Save The Item Name 
                save.WriteLine(item.Name);
            }
        }

        /// <summary>
        /// Loads the current stat of the player
        /// </summary>
        /// <param name="load">Wehre to load all of the players information</param>
        /// <returns>true if its been read correctly, false if failed to laod</returns>
        public override bool Load(StreamReader load)
        {
            //Reads the next line in the save file and sets the value to _gold if its a int 
            if(!int.TryParse(load.ReadLine(), out _gold))
                // Returms false if it's not true
                return false;

            //Reads the next line in the save file and sets the value to arraySize if its a int 
            if (!int.TryParse(load.ReadLine(), out int arrySize))
                // Returms false if it's not true
                return false;
            //Creats a new array for the new set of items in the players inventory 
            _inventory = new Item[arrySize];

            //for the size in the array . . . 
            for(int i = 0; i < _inventory.Length; i++)
            {
                //. . . Reads the next line in the save file and sets the value to to the next item in that index if its a int 
                if (!int.TryParse(load.ReadLine(), out _inventory[i].Cost))
                    //returns false if it's not true
                    return false;

                //For every item in that index add ot to the  inventory 
                _inventory[i].Name = load.ReadLine();
            }
            //return true if everything loaded well 
            return true;
        }
    }
}
