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
        // Keeps Tabs if Entity Dies
        private bool _dead;
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

        public bool Dead { get { return _dead; } set { _dead = value; } }

        /// <summary>
        /// Deconstructor For What is a Entity
        /// </summary>
        /// <param name="name"> Takes The Name for classificcation </param>
        /// <param name="health">Takes The hit points for survival detection </param>
        /// <param name="attack">Takes The attack Power for offense</param>
        /// <param name="defense">Takes The defense for some damage protection</param>
        /// <param name="goldEarn">Player Rewords</param>
        public Entity(string name, float health, float attack, float defense, int goldEarn, bool dead = false)
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

            _dead = dead;
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
        /// Calculates damage delt to defense then sets the entities health minus that damage
        /// </summary>
        /// <param name="damageTotal"></param>
        /// <returns>Returns total damage calcualted </returns>
        public float DamageCalcualtion(float damageTotal)
        {
            // Calculates the damage being done then reducing it by the entities defense 
            float damageTaken = damageTotal - _defense;

            // If the damage they take is lower then the entities defense . . .
            if (damageTaken < 0)
               // The Damage they took is equal to 0;
                damageTaken = 0;
            // The enetities takes the remainig damage  
            _hitPoints -= damageTaken;

            // Retuens whatthe damage was 
            return damageTaken;

        }

        /// <summary>
        /// When called the Entity Will Attack
        /// REducing there hit point based on there attack damage 
        /// and there defenders defense
        /// </summary>
        /// <param name="defender"></param>
        /// <returns>returns the damage calculation</returns>
        public float Attack(Entity defender)
        {
            
            return defender.DamageCalcualtion(AttackPower);
        }

        /// <summary>
        /// Called In Order to amplafy the entities 
        /// attack damage stat
        /// </summary>
        /// <param name="attackBoost">The potancy of the attack boost</param>
        public void AttackIncrease(int attackBoost)
        {
            _attack += attackBoost;    
        }

        /// <summary>
        ///  Called In Order to amplafy the entities 
        /// defesne power stat
        /// </summary>
        /// <param name="defenseBoost">The potancy of the defense boost</param>
        public void DefenseIncrease(int defenseBoost)
        {
            _defense += defenseBoost;
        }

        /// <summary>
        /// Called In Order to reduce the entities 
        /// attack damage stat
        /// </summary>
        /// <param name="attackReduced">The potancy of the attack reduction</param>
        public void AttackDecrease( int attackReduced)
        {
            _attack -= attackReduced;
        }

        /// <summary>
        /// Called In Order to reduce the entities 
        /// defense power stat
        /// </summary>
        /// <param name="defenseReduced">The potancy of the defense reduction</param>
        public void DefenseDecrease( int defenseReduced)
        {
            _defense -= defenseReduced;
        }

        /// <summary>
        /// Called In Order To write the Entity 
        /// name, health, attack power, defense and gold
        /// Stats from to a text file
        /// </summary>
        /// <param name="writer">Path saving to</param>
        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_hitPoints);
            writer.WriteLine(_attack);
            writer.WriteLine(_defense);
            writer.WriteLine(_goldEarn);
        }

        /// <summary>
        /// Called In Order To read the Entity 
        /// name, health, attack power, defense and gold
        /// Stats from a text file
        /// </summary>
        /// <param name="reader">Path saving to</param>
        /// <returns> if the file is corrupted returns false, true if stats loaded well</returns>
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
