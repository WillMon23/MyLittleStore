using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace My_Little_Store
{
    class Entity
    {
        // Holds Eneity Name
        private string _name;
        // Holds Eneity Hit Points 
        private float _hitPoints;
        // Holds Eneity attack Power
        private float _attack;
        // Holds Eneity defense Power
        private float _defense;
        // Holds Eneity Gold Heald
        private int _goldEarn;
        // Holds Eneity Name 
        public string Name { get { return _name; } }
        // Takes and Dispalys  Eneity Hit Point value 
        public float HitPoint { get { return _hitPoints; } set { _hitPoints = value; } }
        // Dispalys  Eneity Attack Power
        public float AttackPower { get { return _attack; } }
        // Dispalys Eneity Defense Power 
        public float Defense { get { return _defense; } }
        // Dispalys  Eneity Gold Earns 
        public int GoldEarn { get { return _goldEarn; } }

        /// <summary>
        /// Deconstructor For What is a Entity
        /// </summary>
        /// <param name="name"> Takes The Name for classificcation </param>
        /// <param name="health">Takes The hit points for survival detection </param>
        /// <param name="attack">Takes The attack Power for offense</param>
        /// <param name="defense">Takes The defense for some damage protection</param>
        /// <param name="goldEarn">Player Rewords</param>
        public Entity(string name, float health, float attack, float defense, int goldEarn)
        {
            // Takes The Name for classificcation
            _name = name;
            // Takes The hit points for survival detection
            _hitPoints = health;
            // Takes The attack Power for offens 
            _attack = attack;
            // >Takes The defense for some damage protection
            _defense = defense;
            // Player Reword
            _goldEarn = goldEarn;
        }

        // Default Decontructor 
        public Entity()
        {
            _name = "Defult";
            _hitPoints = 0;
            _attack = 0;
            _defense = 0;


        }

        /// <summary>
        /// Calculates 
        /// </summary>
        /// <param name="damageTotal"></param>
        /// <returns></returns>
        public float DamageCalcualtion(float damageTotal)
        {
            // Calculates 
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

        public void Healing(int heal)
        {
            _hitPoints += heal;
        }

        public void AttackIncrease(int attackBoost)
        {
            _attack += attackBoost;    
        }

        public void DefenseIncrease(int defenseBoost)
        {
            _defense += defenseBoost;
        }

        public void AttackDecrease( int attackReduced)
        {
            _attack -= attackReduced;
        }

        public void DefenseDecrease( int defenseReduced)
        {
            _defense -= defenseReduced;
        }

        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_hitPoints);
            writer.WriteLine(_attack);
            writer.WriteLine(_defense);
            writer.WriteLine(_goldEarn);
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

            if (!int.TryParse(reader.ReadLine(), out _goldEarn))
                return false;

            return true;
        }


    }
    
}
