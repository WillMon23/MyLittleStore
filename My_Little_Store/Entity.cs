using System;
using System.Collections.Generic;
using System.Text;

namespace My_Little_Store
{
    class Entity
    {
        private string _name;

        private float _hitPoints;

        private float _attack;
        
        private float _attackBoost;

        private float _defense;

        private float _defenseBoost;

        public string Name { get { return _name; } }

        public float HitPoint { get { return _hitPoints; } }

        public float Attack { get { return _attack; } }

        public float AttackBoost { get { return _attackBoost; } }
        
        public float Defense { get { return _defense; } }

        public float DefenseBoost { get { return _defenseBoost; } }


        public Entity(string name,  float health, float attack, float defense)
        {
            _name = name;
            _hitPoints = health;
            _attack = attack;
            _defense = defense;
        }


        public Entity()
        {
            _name = "Defult";
            _hitPoints = 0;
            _attack = 0;
            _attackBoost = 0;
            _defense = 0;
            _defenseBoost = 0;

        }
    }
    
}
