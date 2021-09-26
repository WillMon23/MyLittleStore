using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    class Game
    {
        Player _player;
        Shop _shop;
        bool _gameOver;
        int _currentScene;


        public void Run()
        {
            Start();

            while (!_gameOver)
                Update();

            End();

        }
        private void Start()
        {
            _gameOver = false;
            _currentScene = 0;
            InitializeItems();

        }

        private void Update()
        {
            DisplayCurrentScene();
        }

        private void End()
        {
            Console.WriteLine("Thanks for shopping in My Little Store, Good Bye");
            Console.ReadKey(true);
            Console.Clear();
        }

        private void InitializeItems()
        {
            _player = new Player(100);
            

            Item sword = new Item { Cost = 500, Name = "Swrod" };

            Item shield = new Item { Cost = 10, Name = "Shield" };

            Item healthPostion = new Item { Cost = 15, Name = "Health Postion" };

            _shop = new Shop(sword, shield, healthPostion);
        }

        private int GetInput(string discription, params string[] arr)
        {
            int choice = -1;
            string input = "";

            Console.WriteLine(discription);
            while (choice == -1)
            {
                for (int i = 0; i < arr.Length; i++)
                    Console.WriteLine((i + 1) + ". " + arr[i]);
                Console.Write("> ");

                input = Console.ReadLine();
                if (int.TryParse(input, out choice))
                {
                    choice--;
                    if (choice < 0 || choice >= arr.Length)
                    {
                        choice = -1;
                        Console.WriteLine("Invalde Input Try Again!");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                }
                else
                {
                    choice = -1;
                    Console.WriteLine("Invalde Input Try Again!");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            Console.Clear();
            return choice;
        }

        private void Save()
        {
            StreamWriter writer = new StreamWriter("SaveData.txt");

            writer.WriteLine(_currentScene);

            _player.Save(writer);

            writer.Close();
        }

        private bool Load()
        {
            if (!File.Exists("SaveData.txt"))
                return false;

            StreamReader load = new StreamReader("SaveData.txt");

            if (!int.TryParse(load.ReadLine(), out _currentScene))
                return false;

            if (!_player.Load(load))
                return false;

            load.Close();
            return true;
        }

        private void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case 0:
                    DisplayOpeningMenu();
                    break;
                case 1:
                    DisplayShopMenu();
                    break;
            }
        }

        private void DisplayOpeningMenu()
        {
            int choice = GetInput("Welcome to My Little Shop Simulator! What Would You Like To Do?", "Start Shopping", "Load Inventory");
            if (choice == 0)
                _currentScene = 1;
            if (choice == 1)
                if (Load())
                {
                    Console.WriteLine("Load Was Succsessful");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Failed to Load");
                    Console.ReadKey();
                    Console.Clear();
                }    
        }


        private string[] GetShopMenuOptions()
        {
            int inventorySize = _shop.GetItemNames().Length;
            string[] result = new string[inventorySize + 2];

            for (int i = 0; i < inventorySize; i++)
                result[i] = _shop.GetItemNames()[i];

            result[inventorySize] = "Save Game";
            result[inventorySize + 1] = "Quit Game";

            return result;
        }

        private void DisplayShopMenu()
        {


            int totalInventorySize = GetShopMenuOptions().Length - 3;
            Console.WriteLine("Your gold: " + _player.Gold);
            Console.WriteLine("Your Inventory: \n");

            for (int i = 0; i < _player.GetItemNames().Length; i++)
                Console.WriteLine(_player.GetItemNames()[i] + "\n");

            int choice = GetInput("What would you like to purchase?", GetShopMenuOptions());


            if (choice <= totalInventorySize)
            {
                if (_shop.Sell(_player, choice))
                {
                    Console.WriteLine("You purchased the " + _shop.GetItemNames()[choice]);
                    Console.ReadKey();
                    Console.Clear();
                }

                else
                {
                    Console.WriteLine("You don't have enough for that.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            else if (choice == (totalInventorySize + 1))
            { 
                Save();
                Console.WriteLine("Save was succsessful");
                Console.ReadLine();
                Console.Clear();
            }

            else if (choice == (totalInventorySize + 2))
                _gameOver = true;


        }

    }
}
