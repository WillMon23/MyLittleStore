using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    class Entity
    {
        private string _name;

        private float _hitPoints;

        private float _attack;
        
        private float _defense;

        private int _goldEarn;

        public string Name { get { return _name; } }

        public float HitPoint { get { return _hitPoints; } set { _hitPoints = value; } }

        public float AttackPower { get { return _attack; } }

        public float Defense { get { return _defense; } }

        public int GoldEarn { get { return _goldEarn; } }

        public Entity(string name, float health, float attack, float defense, int goldEarn)
        {
            _name = name;
            _hitPoints = health;
            _attack = attack;
            _defense = defense;
            _goldEarn = goldEarn;
        }

        public Entity()
        {
            _name = "Defult";
            _hitPoints = 0;
            _attack = 0;
            _defense = 0;


        }

        public float DamageCalcualtion(float damageTotal)
        {
            float damageTaken = damageTotal - _defense;

            if (damageTaken < 0)
                damageTaken = 0;

            _hitPoints -= damageTaken;

            return damageTaken;

        }

        public float Attack(Entity defender)
        {
            return defender.DamageCalcualtion(AttackPower);
        }


        public void AttackIncrease(int attackBoost)
        {
            _attack += attackBoost;    
        }

        public void DefenseIncrease(int defenseBoost)
        {
            _defense += defenseBoost;
        }

        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_hitPoints);
            writer.WriteLine(_attack);
            writer.WriteLine(_defense);

        }

        public virtual bool Load(StreamReader reader)
        {
            _name = reader.ReadLine();

            if (!float.TryParse(reader.ReadLine(), out _hitPoints))
                return false;

            if (!float.TryParse(reader.ReadLine(), out _attack))
                return false;

            if (!float.TryParse(reader.ReadLine(), out _defense))
                return false;


            return true;
        }


    }
    
}
