namespace FightSimulator.Classes
{
    class Wizard : Warrior
    {
        public int Mana { get; protected set; } = 0;
        public int UltimateAbility { get; protected set; }
        public int MaxMana { get; protected set; }

        public Wizard(string name, int health, int attack, int defense, Dice dice, int maxMana, int ultimaneAbility) : base(name, health, attack, defense, dice)
        {
            MaxMana = maxMana;
            UltimateAbility = ultimaneAbility;
        }

        public override void AttackEnemy(Warrior enemy)
        {
            if (Mana < MaxMana)
            {
                Mana += 10;
                if (Mana > MaxMana)
                    Mana = MaxMana;
                base.AttackEnemy(enemy);
            }
            else
            {
                int attackActual = UltimateAbility + Dice.Throw();
                Message = $"{Name} uses magic for {attackActual} hp";
                enemy.Defend(attackActual);
                Mana = 0;
            }
        }

        protected void AttackBaseEnemy(Warrior enemy) => base.AttackEnemy(enemy);

        public string GraphicMana() => GraphicPointer(Mana, MaxMana);
    }
}
