using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    public enum Scen
    {
        
        INTRODUCTION,
        PLAYERNAME,
        MAINMENU,
        SHOP,
        BATTLE,
        LOADORSAVE,
    }
    
    /// <summary>
    /// Core oporations of the game
    /// </summary>
    class Game
    {

        //How the player items and gold kept tracked 
        Player _player;

        bool _died; 
        
        Entity _elfoo;

        int _enemyCount;

        //How the shop is kept tracked of  
        Shop _shop;

        //Keeps track of the game if its over or not 
        bool _gameOver;

        //Keeps track of the current scene being played on
        Scen _currentScene;

        bool _equiped;

        //Hold Players Name
        string _usersName = "Defult";

        float _enemyHP, _enemyAtt, _enemyDef;
        
        int _enemyGold;

        /// <summary>
        /// Runs the whole game when started  
        /// </summary>
        public void Run()
        {
            Start();

            while (!_gameOver)
                Update();

            End();

        }

        //Sets the Values foreverything at start
        private void Start()
        {
            //Game Over if it true 
            _gameOver = false;

            _equiped = false;

            _died = false;

            //Sets Current Scene to the first scene
            _currentScene = Scen.INTRODUCTION;

            //Initializes the Curent Items in the game
            InitializeItems();

            InitializePlayerAndEnemy();
        }

        private void GetPlayersName()
        { 
            Console.WriteLine("Lets Start With You're Name");
            Console.Write("> ");
            _usersName = Console.ReadLine();
            Console.Clear();

            if (GetInput("You Choose the Name " + _usersName + " Would You Like To Continue", "Yes", "No") == 0)
            {
                _player = new Player(_usersName, 1000f, 20f, 20f, 100);
                _currentScene = Scen.MAINMENU; 
            }
        }

        /// <summary>
        /// Initialize players and the enemies implamanted in the game 
        /// </summary>
        private void InitializePlayerAndEnemy()
        {

            //Sets players allowance

            _enemyHP = 20f;

            _enemyAtt = 30f;

            _enemyDef = 10f;

            _enemyGold = 15;

            _enemyCount = 1;

            _elfoo = new Entity("Elfoo", _enemyHP, _enemyAtt, _enemyDef, _enemyGold);

            _player = new Player(_usersName, 1000f, 20f, 20f, 100);


        }


        /// <summary>
        /// Updates Everything at frame
        /// </summary>
        private void Update()
        {
            //Dispalys Current Scene
            DisplayCurrentScene();
            
        }

        /// <summary>
        /// End Message For When the Game Ends
        /// </summary>
        private void End()
        {
            //End Message 
            Console.WriteLine("Thanks for shopping in My Little Store, Good Bye");
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Initializes the Curent Items in the game
        /// </summary>
        private void InitializeItems()
        {

            
            //Sets the Cost for a swrod
            Item sword = new Item { Cost = 25, Name = "Sword", Potence = 25 };

            //Sets the Cost for a Shield 
            Item shield = new Item { Cost = 10, Name = "Shield", Potence = 10 };

            // Sets the Cost for a Health Postion
            Item healthPotion = new Item { Cost = 15, Name = "Health Potion", Potence = 50 };

            //creats the shops inventory 
            _shop = new Shop(sword, shield, healthPotion);
        }

        /// <summary>
        /// Get players response and removes the possobility of input error
        /// </summary>
        /// <param name="discription">Writes to the player what they want them to do</param>
        /// <param name="arr">Options for the player</param>
        /// <returns>Once input recived returns it in a int foreasy response</returns>
        private int GetInput(string discription, params string[] arr)
        {
            //Since the value can be 0 were starting from -1
            int choice = -1;

            //So we can get the value from the user
            string input = "";

            //Displays our message to the user
            Console.WriteLine(discription);

            // While there is no valide input. . . 
            while (choice == -1)
            {
                //. . . for every string in the params 
                for (int i = 0; i < arr.Length; i++)

                    //. . .write to console that index + 1 and what the string in that index is 
                    Console.WriteLine((i + 1) + ". " + arr[i]);
                // Oncw done just type the great than sign to make it look clean 
                Console.Write("> ");

                // Reads the users input then sets it to our temp variable 
                input = Console.ReadLine();
                // if input is a int set it to choice. . . 
                if (int.TryParse(input, out choice))
                {
                    // reduce users choice by 1
                    choice--;

                    // if users choice is less then 0 or choice is greater or equal to the arrays size. . . 
                    if (choice < 0 || choice >= arr.Length)
                    {
                        //. . .Sets Choice to default value
                        choice = -1;
                        //. . .Writes out an ERROR message
                        Console.WriteLine("Invalde Input Try Again!");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                }
                //if any other input
                else
                {
                    //. . .Sets Choice to default value
                    choice = -1;
                    //. . .Writes out an ERROR message
                    Console.WriteLine("Invalde Input Try Again!");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            Console.Clear();
            return choice;
        }
        /// <summary>
        /// Save the progress of the player and the current scene 
        /// </summary>
        private void Save()
        {
            //Checks to see if there is an existising File named 'SaveData'. . . 
            if (!File.Exists("SaveData.txt"))
                //. . . If it does not we let them know there is a new save  
                Console.WriteLine("New Save Was Detected");
            // Other Wise. . .
            else
                // We welcome ther user back
                Console.WriteLine("Save was Saved Over");

            //Creats a new text file 
            StreamWriter writer = new StreamWriter("SaveData.txt");

            // Writes on the text file the current scene
            writer.WriteLine(_currentScene);

            //Saves the players writes stats to that text file 
            _player.Save(writer);


            

            _elfoo.Save(writer);


            //closes the file when done 
            writer.Close();
        }


        /// <summary>
        /// Load from the text file
        /// </summary>
        /// <returns>true if eveything loaded well</returns>
        private bool Load()
        {

            bool loaded = true;
            //Checks to see if that file exists. . .  
            if (!File.Exists("SaveData.txt"))
                //. . . if not returns falsem
                loaded = false;

            //Creats a streamerader so we can refur to that file
            StreamReader load = new StreamReader("SaveData.txt");

            if (!Scen.TryParse(load.ReadLine(), out _currentScene))
                loaded = false;

            //if player didn't load properly
            if (!_player.Load(load))
                // returns false
                loaded =  false;


            if (!_elfoo.Load(load))
                loaded = false;

           

            //Closes the file when done using it 
            load.Close();
            // Returns true if everything loaded properlly 
            return loaded;
        }


        private void PotionUsed()
        {
            if (_player.NeedHealing() != 0)
            {
                int _potionUsed = _player.NeedHealing() - _player.PotionCount;
                if (_potionUsed != 0)
                {
                    if (GetInput("You Have " + _potionUsed + " Health Potion, Would You Like To Use One?", "Yes", "No") == 0)
                    {
                        _player.UsePotion(75);
                        Console.WriteLine("You Used 1 Health Potion");
                    }
                    else
                        Console.WriteLine("Health Potion Was Not Consumed");
                }
                else
                    Console.WriteLine("You Ran Out Of Potions");

                Console.ReadKey(true);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("You Don't Have Any HealthPotions");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        private void StartMenu()
        {
            int choice = GetInput("Start Menu", "Start New Game", "Load Game","Quit Game");
            switch (choice)
            {
                case 0:
                    
                    _currentScene = Scen.PLAYERNAME;
                    break;
                case 1:
                    if (Load())
                    {
                        Console.WriteLine("Load was successful");
                        Console.ReadKey(true);
                        Console.Clear();

                    }
                    else
                    {
                        Console.WriteLine("Failed to Load");
                        _currentScene = Scen.INTRODUCTION;
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    break;

                case 2:
                    _gameOver = true;
                    break;
            }
        }

        /// <summary>
        /// Displays Current Scene
        /// </summary>
        private void DisplayCurrentScene()
        {
            // Loooks to see what scene were in. . .
            switch (_currentScene)
            {
                case Scen.INTRODUCTION:
                    StartMenu();
                    break;
                case Scen.PLAYERNAME:
                   // InitializePlayerAndEnemy();
                    GetPlayersName(); 
                    break;
                //. . .if the scene set to Introduction. . .
                case Scen.MAINMENU:
                    Console.WriteLine("Heading To Main Menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    //...Displays Opening Menu
                     MainGameMenu();
                    break;
                //. . .If the scene set to LoadOrSave. . . 
                case Scen.LOADORSAVE:
                    Console.WriteLine("Heading To Load and Save Menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    //. . . Dispalys a save load or quit menu
                    LoadOrSaveMenu();
                    break;
                    
                //. . .if the scene were in is Shop
                case Scen.SHOP:
                    //. . .Display Shop Menu 
                    DisplayShopMenu();
                    break;
                case Scen.BATTLE:
                    Battle();
                    BattleResults();
                    break;
            }
        }

        //Dispalys Opening Menu 
        private void MainGameMenu()
        {
            
            //Gathers uers input and turns it to a int value so that it can be interpreted
            int choice = GetInput("Hey "+ _player.Name + ", Welcome to Death Battle! Where You Endlessly fight Enemies Till Your Heart Content, What Would You Like To Do?","Shop", "Load Save Menu", "Start Battle","Quit Game");

            //Checks to see what the users input was. . . 
            switch (choice)
            {

                //. . .if chioce is 0 
                case 0:
                    Console.WriteLine("Heading To Shop Menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    //Scrolles to the next scene
                    _currentScene = Scen.SHOP;
                    break;
                //. . .if choice is 1 
                case 1:
                    _currentScene = Scen.LOADORSAVE;
                    break;

                case 2:
                    Console.WriteLine("Heading To Battle Menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene = Scen.BATTLE;
                    break;
                case 3:
                    _gameOver = true;
                    break;
            }
        }

        private void LoadOrSaveMenu()
        {
            int choice = GetInput("Laod and Saving Menu, What Will You Do Next","Save" , "Load", "Back To Main Menu", "Quit Game");

            switch (choice)
            {
                case 0:
                    if (!_died)
                    {
                        //. . .Data Gets Saved 
                        Save();
                        //. . .Tells the user they had saved successfully
                        Console.WriteLine("Save was succsessful");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Function Deactivated When Dead");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    break;
                case 1:
                    if (Load())
                    {
                        Console.WriteLine("Welcome Back");
                        Console.ReadKey(true);
                        Console.Clear();
                    }

                    else
                    {
                        Console.WriteLine("Load Attempt Failed ");
                        _currentScene = Scen.INTRODUCTION;
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    break;
                case 2:
                    _currentScene = Scen.MAINMENU;
                    break;
                case 3:
                    _gameOver = true;
                    break;

            }
        }

        /// <summary>
        /// Gets Shop Menu Options putd it in a array and just returns that array 
        /// </summary>
        /// <returns>retruns Array of Strings </returns>
        private string[] GetShopMenuOptions()
        {
            // sets to the size of the shop item names array 
            int menuSize = _shop.GetItemNames().Length;
            
            //Creats new array to put all the inputs to be used in the shop
            string[] result = new string[menuSize + 2];

            //For every index in the menu size. . .  
            for (int i = 0; i < menuSize; i++)
                    //. . . Just Plug it In
                    result[i] = _shop.GetItemNames()[i];
            
            //sets the current size to be 'Save Game'
            result[menuSize] = "Back TO Main Menu";
            //Sets the size plus one to be 'Quit Game'
            result[menuSize + 1] = "Quit Game";

            //return the total menu results
            return result;
        }

        private void PrintStats(Player entity)
        {
            if (!_equiped)
                Console.WriteLine(entity.Name + "\n" +
                "Health: " + entity.HitPoint + "\n" +
                "Attack: " + entity.AttackPower + "\n" +
                "Defense: " + entity.Defense + "\n" + 
                "Gold: " + entity.Gold + "\n");

            else
                Console.WriteLine(entity.Name + "\n" +
                "Health: " + entity.HitPoint + "\n" +
                "Attack: " + entity.AttackPower + " (" + _player.AttackBoost + ")\n" +
                "Defense: " + entity.Defense + " (" + _player.DefenseBoost + ")\n" +
                "Gold: " + entity.GoldEarn + "\n");
        }

        private void PrintStats(Entity entity)
        {
     
                Console.WriteLine(entity.Name + "\n" +
                    "Health: " + entity.HitPoint + "\n" +
                    "Attack: " + entity.AttackPower + "\n" +
                    "Defense: " + entity.Defense + "\n" +
                    "Gold: " + entity.GoldEarn + "\n");
           
        }

        /// <summary>
        /// Displays the Shops Menu
        /// </summary>
        private void DisplayShopMenu()
        {
            // Sets a int variable to be the total size of items it want to be desplayed minus 3
            int totalInventorySize = GetShopMenuOptions().Length - 3;
            //print the players current gold holding
            Console.WriteLine("Your gold: " + _player.Gold);
            //LEt the user know where there inventory will be displayed at 
            Console.WriteLine("Your Inventory:");

            //For every item the player has in there inventory. . . 
            for (int i = 0; i < _player.GetItemNames().Length; i++)
                //Dis play that items name to the screen 
                Console.WriteLine(_player.GetItemNames()[i]);

            //Get the players decision to what they would like to purchase
            int choice = GetInput("\n What would you like to purchase?", GetShopMenuOptions());

            //If the choice is less then the size between the options. . .
            if (choice <= totalInventorySize)
            {
                //If the Shops Sells the item. . . 
                if (_shop.Sell(_player, choice))
                {
                    //. . .Displays to the users that the shop just sold them that item
                    Console.WriteLine("You purchased the " + _shop.GetItemNames()[choice]);
                    Console.ReadKey();
                    Console.Clear();
                }
                //else they cant buy the item
                else
                {
                    //. . .Displays to them they can't purches said item
                    Console.WriteLine("You don't have enough for that.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            // if the choice happens to bt the size plus 1. . .
            else if (choice == (totalInventorySize + 1))
                _currentScene = Scen.MAINMENU;

            
            // if the choice happens to bt the size plus 2. . .
            else if (choice == (totalInventorySize + 2))
                //The Update Loop Ends and the Game is Over
                _gameOver = true;
        }

        private void Battle()
        {
            Random rng = new Random();

            PrintStats(_player);
            

            PrintStats(_elfoo);

            int choice = GetInput("You've Come Accross " + _elfoo.Name + ", What Will You Do Next?","Attack","Heal", "Equip Bonuses", "Save", "Back To Main Menu", "Quit Game");

            

            switch (choice)
            {
                case 0:
                    Console.WriteLine("You Dealt " + _player.Attack(_elfoo) + " " + _elfoo.Name);
                    Console.ReadKey(true);
                    Console.Clear();
    
                    if (rng.Next(1, 4) == 1)
                    {
                        Console.WriteLine("You took " + _elfoo.Attack(_player) + " Hit Points of Damage");
                        Console.ReadKey(true);
                        Console.Clear();
                    }break;

                case 1:
                    PotionUsed();
                    break;
                case 2:
                    if (!_equiped)
                    {
                        if (_player.BonusItemsUse())
                        {
                            Console.WriteLine("Stats Have Been Modified");
                            _equiped = true;
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("You Dont Have Any Modifires");
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                        
                    }
                    else
                    {
                        if (_player.RemoveBonus())
                        {
                            Console.WriteLine("Stat Modifiers Were Removed");
                            _equiped = false;
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                        
                    }
                    break;
                case 3:
                    Save();
                    Console.WriteLine("Save Was Succsessful");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case 4:
                    _currentScene = Scen.MAINMENU;
                    break;

                case 5:
                    _gameOver = true;
                    break;

            }
            
        }

        private void BattleResults()
        {
            if(_player.HitPoint <= 0f)
            {
                Console.WriteLine(_player.Name + " Your Died!");
                _died = true;
                _currentScene = Scen.LOADORSAVE;
            }
            if(_elfoo.HitPoint <= 0)
            {
                Console.WriteLine(_elfoo.Name + " Has Met There End!!!" + 
                    "\nYou Won " + _player.GoldWon(_elfoo) + "g");
                Console.ReadKey();
                Console.Clear();

                switch ((GetInput("Would you like to do next?!", "Start A New Fight", "Back To Main Menu","Quit Game")))
                {
                    case 0:
                        _enemyCount++;
                        _elfoo = new Entity("Elfoo #" + _enemyCount, (_enemyHP *= 2), (_enemyAtt *= 2), _enemyDef, (_enemyGold *= 2));
                        break;
                    case 1:
                        _currentScene = Scen.MAINMENU;
                        break;
                    case 2:
                        _gameOver = true;
                        break;
                }
            }
        }

    }
}
