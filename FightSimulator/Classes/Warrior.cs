using System;

namespace FightSimulator.Classes
{
    class Warrior
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        public Dice Dice { get; protected set; }

        public string Message { get; protected set; }

        public Warrior(string name, int health, int attack, int defense, Dice dice)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            Attack = attack;
            Defense = defense;
            Dice = dice;
        }

        public override string ToString() => $"{Name}";

        public bool IsAlive() => Health > 0;

        protected string GraphicPointer(int actual, int maximal)
        {
            string s = "";
            const int totalIndicators = 20;
            double count = Math.Round((double)actual / maximal * totalIndicators);

            if ((count == 0) && IsAlive())
                count = 1;

            for (int i = 0; i < count; i++)
                s += "█";

            return s.PadRight(totalIndicators);
        }

        public string GraphicHealth() => GraphicPointer(Health, MaxHealth);

        public void Defend(int attackActual)
        {
            int injury = attackActual - (Defense + Dice.Throw());

            Message = $"{Name} lost {injury} HP";
            if (injury > 0)
            {
                Health -= injury;

                if (Health <= 0)
                {
                    Health = 0;
                    Message += " and died.";
                }
            }
            else
            {
                Message = $"{Name} blocked the attack";
            }
        }

        public virtual void AttackEnemy(Warrior enemy)
        {
            int attackActual = Attack + Dice.Throw();
            Message = $"{Name} attacks for {attackActual} hp";
            enemy.Defend(attackActual);
        }
    }
}
