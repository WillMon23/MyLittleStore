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

            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine((i + 1) + ".");
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

            return choice;
        }

        private void Save()
        {

        }

        private void Load()
        {

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
            int choice = GetInput("Welcomw to My Little Shop Simulator! What Would You Like To Do?", "Start Shopping", "Load Inventory");
            if (choice == 0)
                _currentScene = 1;
            if (choice == 1)
                Load();

        }


        private string[] GetShopMenuOptions()
        {
            int inventorySize = _shop.GetItemNames().Length;
            string[] result = new string[inventorySize + 2];

            for (int i = 0; i < inventorySize; i++)
                result[i] = _shop.GetItemNames()[i];

            result[inventorySize + 1] = "Save Game";
            result[inventorySize + 2] = "Quit Game";

            return result;
        }

        private void DisplayShopMenu()
        {

            int totalInventorySize = GetShopMenuOptions().Length - 2;
            Console.WriteLine(_player.Gold);
            Console.WriteLine("Inventory\n");

            for(int i = 0; i < _player.Inventory.Length;i++)
                Console.WriteLine(_player.Inventory[i]);

            int choice = GetInput("What would you like to purchase?", GetShopMenuOptions());

            if(choice < totalInventorySize)


        }

    }
}
