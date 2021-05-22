namespace FightSimulator.Classes
{
    class Healer : Wizard
    {
        public Healer(string name, int health, int attack, int defense, Dice dice, int maxMana, int ultimateAbility)
                      : base(name, health, attack, defense, dice, maxMana, ultimateAbility)
        {
        }

        public override void AttackEnemy(Warrior enemy)
        {
            if (Mana < MaxMana)
            {
                Mana += 10;
                if (Mana > MaxMana)
                    Mana = MaxMana;
                AttackBaseEnemy(enemy);
            }
            else
            {
                int healActual = UltimateAbility + Dice.Throw();
                Message = $"{Name} heals himself for {healActual} hp";
                Health += UltimateAbility + Dice.Throw();

                if (Health > MaxHealth)
                    Health = MaxHealth;

                Mana = 0;
            }
        }
    }
}
