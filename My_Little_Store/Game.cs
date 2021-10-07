using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    //Invelops Whats Scenes Will Be Tansitioning through
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

        // Tracks enemies Growth
        static float  _enemyHP, _enemyAtt, _enemyDef;
        
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
            // Enemy Health 
            _enemyHP = 20f;

            // Enemy Atttack Power 
            _enemyAtt = 30f;

            // Enemy Defemse Power
            _enemyDef = 10f;

            // Enemys Gold 
            _enemyGold = 15;

            // In Order To Keeep Count of the Number Of Enemies 
            _enemyCount = 1;

            // enemy Decloration
            _elfoo = new Entity("Elfoo", _enemyHP, _enemyAtt, _enemyDef, _enemyGold);

            // Player Decloration
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

            //Saves the players stats to that text file 
            _player.Save(writer);
            
            // Enemy Entity stats savd tp text File 
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

            // if enemy(elfoo) stats did not load 
            if (!_elfoo.Load(load))
                //returns false 
                loaded = false;

           

            //Closes the file when done using it 
            load.Close();
            // Returns true if everything loaded properlly 
            return loaded;
        }

        /// <summary>
        /// Checks and uses the potions aquired in the shop if purched 
        /// </summary>
        private void PotionUsed()
        {
            // IF player Needs Health But It's Not Zero. . .
            if (_player.NeedHealing() > 0)
            {
                // . . . Creats a verable to contain the values of both the poitons purcheded and the potions used 
                int _potionUsed = _player.NeedHealing() - _player.PotionCount;
                // . . .IF They HAve Potiones Equiped. . . 
                if (_potionUsed > 0)
                {   //. . .If player chooses to use a potion they will gain hit points but lose a potion. . . 
                    if (GetInput("You Have " + _potionUsed + " Health Potion, Would You Like To Use One?", "Yes", "No") == 0)
                    {
                        //. . . Adds Health 
                        _player.UsePotion(75);
                        //. . . Lets the player know they consumed there potion 
                        Console.WriteLine("You Used 1 Health Potion");
                    }
                    else
                        // If player did not decide to use the potion then they get a message 
                        Console.WriteLine("Health Potion Was Not Consumed");
                }
                else
                    // If They Ran Out of Potion after use they get a message 
                    Console.WriteLine("You Ran Out Of Potions");

                Console.ReadKey(true);
                Console.Clear();
            }
            else
            {
                // If fthey never had a potion to begine with they just get a message to notify the user 
                Console.WriteLine("You Don't Have Any HealthPotions");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        /// <summary>
        /// The Menu to be Displayed at the Start of the Game or The End Of The Game
        /// </summary>
        private void StartMenu()
        {
            // Starting Menu Menu
            int choice = GetInput("Start Menu", "Start New Game", "Load Game","Quit Game");
            // If choice is. . .
            switch (choice)
            {
                //. . . is 0, . . .
                case 0:
                    // Starts The Players VAlues Up 
                    InitializePlayerAndEnemy();
                    //. . . Chaange the Scene to Player Name Mwnu
                    _currentScene = Scen.PLAYERNAME;
                    break;
                //. . . is 1, . . .
                case 1:
                    // Loads Privouse Game if everything loaded smoothly 
                    if (Load())
                    {
                        // Lets the user know It loaded Correctly
                        Console.WriteLine("Load was successful");
                        Console.ReadKey(true);
                        Console.Clear();

                    }
                    //If Faied to load. . . 
                    else
                    {
                        // User Will be preseented with a faield  message 
                        Console.WriteLine("Failed to Load");
                        // scen stays in the Introductional scen 
                        _currentScene = Scen.INTRODUCTION;
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    break;
                //. . . is 2, . . .
                case 2:
                    // Ends The Game
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
                // case scene changed to Introduction
                case Scen.INTRODUCTION:
                    // Starting menu opporate 
                    StartMenu();
                    break;
                // case scene changed to Player Name Menu
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
                    // change scen to Load and Save Menu
                    _currentScene = Scen.LOADORSAVE;
                    break;
                //case choice is 2. . .
                case 2:
                    // Lets The User Know to that there heading to the Battle Menu
                    Console.WriteLine("Heading To Battle Menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    //Changes the scen to the battle menu
                    _currentScene = Scen.BATTLE;
                    break;
                // Case the users choice is 3. . . 
                case 3:
                    //Use Choose to end the game 
                    _gameOver = true;
                    break;
            }
        }

        /// <summary>
        /// Menu set to give the user the option to load, save or end the game 
        /// </summary>
        private void LoadOrSaveMenu()
        {
            // Let The User Know there in a load save menu and asks them what they would like to do next from Save, Load, Go Back to the Main Menu or End The Game 
            int choice = GetInput("Laod and Saving Menu, What Will You Do Next","Save" , "Load", "Back To Main Menu", "Quit Game");

            // If User Choice is. . .
            switch (choice)
            {
                // Case choice is 0. . .
                case 0:
                    // If Player is Not Dead (ALive). . .
                    if (!_died)
                    {
                        //. . .Data Gets Saved 
                        Save();
                        //. . .Tells the user they had saved successfully
                        Console.WriteLine("Save was succsessful");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    // Else . . . 
                    else
                    {
                        // Let The USer Know That This Is Not an Option For Them 
                        Console.WriteLine("Function Deactivated When Dead");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    break;
                // Case Choice is 1 . . .
                case 1:
                    // If Load Was Succsessful
                    if (Load())
                    {
                        // Gives The User a 'Welcome Back' Message  
                        Console.WriteLine("Welcome Back");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    //Else . .  .
                    else
                    {
                        // Lets the user Know Load had Failed 
                        Console.WriteLine("Load Attempt Failed ");
                        // Keeps It In The Same Scen 
                        _currentScene = Scen.INTRODUCTION;
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    break;
                //Case choice was 2 . . .
                case 2:
                    // Current Scen Will Be Changed to The MAin Menu
                    _currentScene = Scen.MAINMENU;
                    break;
                // Case Choice is 3 . . .
                case 3:
                    // Game HAs Ended 
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
            
            //sets the current size to be 'Save Game'.
            result[menuSize] = "Back TO Main Menu";
            //Sets the size plus one to be 'Quit Game'
            result[menuSize + 1] = "Quit Game";

            //return the total menu results
            return result;
        }

        /// <summary>
        /// Print Stats to Screen For Player Stats
        /// </summary>
        /// <param name="player">Users Players Stats For Display Perpous</param>
        private void PrintStats(Player player)
        {
            // IF THey Dont Have Anything Equiped. . . 
            if (!_equiped)
                // Stats Will Be Displayed Like Soo Without the Added Enhancements 
                Console.WriteLine(player.Name + "\n" + // Name 
                "Health: " + player.HitPoint + "\n" + // Hit points 
                "Attack: " + player.AttackPower + "\n" + // Attack Damage 
                "Defense: " + player.Defense + "\n" +  // Defense Power
                "Gold: " + player.Gold + "\n"); // Gold Held
            //Else . . .
            else
                // Console Will Display Stats With The Enhancements 
                Console.WriteLine(player.Name + "\n" + // Name 
                "Health: " + player.HitPoint + "\n" + // Hit Points 
                "Attack: " + player.AttackPower + " (" + player.AttackBoost + ")\n" + // Attack Damage and Attack bonus 
                "Defense: " + player.Defense + " (" + player.DefenseBoost + ")\n" + // Defense Power and Defense bonus 
                "Gold: " + player.Gold + "\n"); // Gold Hold 
        }

        /// <summary>
        /// Displays Entity Stats 
        /// </summary>
        /// <param name="entity">Base Stats Used To Classify a Entity </param>
        private void PrintStats(Entity entity)
        {
                // Console Will Dis[layes Entity Stats 
                Console.WriteLine(entity.Name + "\n" + // Name 
                    "Health: " + entity.HitPoint + "\n" + // Hit Points 
                    "Attack: " + entity.AttackPower + "\n" + // Attack Damage 
                    "Defense: " + entity.Defense + "\n" + // Defense Power 
                    "Gold: " + entity.GoldEarn + "\n"); // Gold Held 
           
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

        /// <summary>
        /// CAlculates and Handles What Happens In The Battle Scen
        /// </summary>
        private void Battle()
        {
            // Creats a Random Variable to Randomly Dicatates When the enemy will attack
            Random rng = new Random();

            // Prints the stats of the player
            PrintStats(_player);
            
            // Prints the stats of the enemy    
            PrintStats(_elfoo);

            // Gets the players Imput, User chooses wether they would like to "Attack","Heal", "Equip Bonuses", "Save", "Go Back To Main Menu", "Quit Game"
            int choice = GetInput("You've Come Accross " + _elfoo.Name + ", What Will You Do Next?","Attack","Heal", "Equip Bonuses", "Save", "Back To Main Menu", "Quit Game");

            
            //When the user makes there choice. . .
            switch (choice)
            {
                // Case choice is 0 . . .
                case 0:
                    // Console will print out the damage calculations made to the entity elfoo
                    Console.WriteLine("You Dealt " + _player.Attack(_elfoo) + " to " + _elfoo.Name);
                    Console.ReadKey(true);
                    Console.Clear();
                    
                    // Sets the chance that the enemy will attack back;
                    if (rng.Next(1, 4) == 1)
                    {
                        // Console will print out the damage calculations done to the play
                        Console.WriteLine("You took " + _elfoo.Attack(_player) + " Hit Points of Damage");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    break;
                // Case choice is 1 . . .
                case 1:
                    // Uses players potion
                    PotionUsed();
                    break;
                // Case choice is 2 . . . 
                case 2:
                    // Moddifries
                    if (!_equiped)
                    {
                        // IF Player Has modifires. . .
                        if (_player.BonusItemsUse())
                        {
                            // Let the player know thst they have been activated 
                            Console.WriteLine("Stats Have Been Modified");
                            // Set Equiped to true
                            _equiped = true;
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                        else
                        {
                            // Lets the user know there player has no modifires 
                            Console.WriteLine("You Don't Have Any Modifires");
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                        
                    }
                    else
                    {
                        // If player has modifires equiped 
                        if (_player.RemoveBonus())
                        {
                            // Let the player know the modifres were removed 
                            Console.WriteLine("Stat Modifiers Were Removed");
                            // Sets Equiped to false
                            _equiped = false;
                            Console.ReadKey(true);
                            Console.Clear();
                        }

                    }
                    break;
                // Case choice is 3 . . . 
                case 3:
                    // Saves Games current stat
                    Save();
                    //Lets the user know that the game was saved
                    Console.WriteLine("Save Was Succsessful");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                // Case choice is 4 . . . 
                case 4:
                    // Changes scene to the main menu
                    _currentScene = Scen.MAINMENU;
                    break;
                // Case choice is 5 . . . 
                case 5:
                    //End The Game
                    _gameOver = true;
                    break;

            }
            
        }

        /// <summary>
        /// At the end of the battle 
        /// Checks to she what are the conditions of the current Enities 
        /// Once Player or an Elfoo dies
        /// The Console will display the sesult 
        /// IT'll as the user if they would like to start again in the player dies
        /// </summary>
        private void BattleResults()
        {
            // if player hit points are equal or less than 0. . .
            if(_player.HitPoint <= 0f)
            {
                // Lets the user know they died 
                Console.WriteLine(_player.Name + " Your Died!");
                // Set Player to dead
                _died = true;
                // Current Scene changes to the introduction scene
                _currentScene = Scen.INTRODUCTION;
            }
            // If elfoo hit points are equal or less than 0 . . . 
            if(_elfoo.HitPoint <= 0)
            {
                // Lets the user know they have defeateed with there reward 
                Console.WriteLine(_elfoo.Name + " Has Met There End!!!" + 
                    "\nYou Won " + _player.GoldWon(_elfoo) + "g");
                Console.ReadKey();
                Console.Clear();

                // Gets users input in order to "Start A Next Fight", "Go Back To Main Menu" or "Quit Game . . ."
                switch ((GetInput("Would you like to do next?!", "Start A Next Fight", "Back To Main Menu","Quit Game")))
                {
                    // Case user input is 0 . . . 
                    case 0:
                        // Incraments the enemy count by 1
                        _enemyCount++;
                        // Creats a new instance of an elfo 
                        _elfoo = new Entity("Elfoo #" + _enemyCount, (_enemyHP *= 2), (_enemyAtt *= 2), _enemyDef, (_enemyGold *= 2));
                        break;
                    // Case user input is 1 . . . 
                    case 1:
                        // Incraments the enemy count by 1
                        _enemyCount++;
                        // Creats a new instance of an elfo 
                        _elfoo = new Entity("Elfoo #" + _enemyCount, (_enemyHP *= 2), (_enemyAtt *= 2), _enemyDef, (_enemyGold *= 2));
                        // Sets the new current scene to the main menu
                        _currentScene = Scen.MAINMENU;
                        Console.WriteLine("Expect a new fight when you arrive back to battle ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    // Case user input is 0 . . . 
                    case 2:
                        // End Game
                        _gameOver = true;
                        break;
                }
            }
        }

    }
}
