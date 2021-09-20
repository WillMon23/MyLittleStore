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

        }

        private void Update()
        {

        }

        private void End()
        {

        } 

        private void InitializeItems()
        {

        }

        private int GetInput(string discription, params string[] arr)
        {
            return 0;
        }

        private void Save()
        {

        }

        private void Load()
        {

        }

        private void DisplayCurrentScene()
        {

        }

        private string[] GetShopMenuOptions()
        {
           return
        }

        private void DisplayShopMenu()
        {

        }
    }
}
