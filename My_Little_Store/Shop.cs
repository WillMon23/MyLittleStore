using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    /// <summary>
    /// Classifcations for an Item in the shop
    /// </summary>
    public struct Item
    {
        //How much it costs to but an item
        public int Cost;
        //The name assoicated with the cost 
        public string Name;
    }

    /// <summary>
    /// The Classification of what is  and what happens in thwe shop 
    /// </summary>
    class Shop
    {
        //How much gold the shop acumulates 
        private int _gold;
        //Stores inventory 
        private Item[] _inventory;


        /// <summary>
        /// Shops Constructers to creat a inventory of items 
        /// </summary>
        /// <param name="items">The Items that the shop will have</param>
        public Shop(params Item[] items)
        {
            //sets the inventory 
            _inventory = items;
            //Defults the stores gold count to zero
            _gold = 0;
        }

        /// <summary>
        /// The Proccess of selling an item in the store 
        /// </summary>
        /// <param name="player">to add item tio there inventory</param>
        /// <param name="item">Index of the item in the stores inventory</param>
        /// <returns>returns false if no sell was done true is transaction was succsessful</returns>
        public bool Sell(Player player, int item)
        {
            //Sets that there has not be a sell to false
            bool purches = false;
            //if players current gold is greater or equal to the items index cost. . . 
            if (player.Gold >= _inventory[item].Cost)
            {
                //. . .Percues has been done 
                purches = true;
                //. . .Store has gained gold in return 
                _gold += _inventory[item].Cost;
                //player took the item placing it in its inventory
                player.Buy(_inventory[item]);
            }
            // returns false if no sell was done true is transaction was succsessful
            return purches;
        }

        /// <summary>
        /// Meant to gather the names in the store inventory
        /// </summary>
        /// <returns>The names of all the items in the store</returns>
        public string[] GetItemNames()
        {
            //creats new array to collect the item names
            string[] names = new string[_inventory.Length];
            
            //for every item in that index. . .
            for (int i = 0; i < _inventory.Length; i++)
                //. . . put the name of that index in the name array
                names[i] = _inventory[i].Name + " - " + _inventory[i].Cost;
            // returns The names of all the items in the store
            return names;

        }
    }    
}
