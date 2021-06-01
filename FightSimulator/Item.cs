namespace FightSimulator
{
    class Item
    {
        public string Name { get; private set; }

        public int HealthBonus { get; private set; }
        public int AttackBonus { get; private set; }
        public int DefenseBonus { get; private set; }
        public int UltimateBonus { get; private set; }

        public Item(string name, int healthBonus, int attackBonus, int defenseBonus, int ultimateBonus)
        {
            Name = name;

            HealthBonus = healthBonus;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
            UltimateBonus = ultimateBonus;
        }

        public override string ToString() => Name;
    }
}
