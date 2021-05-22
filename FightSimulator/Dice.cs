using System;

namespace FightSimulator
{
    class Dice
    {
        private readonly Random random;

        public int Sides { get; private set; }

        public Dice()
        {
            Sides = 10;
            random = new Random();
        }

        public Dice(int numberOfSides)
        {
            Sides = numberOfSides;
            random = new Random();
        }

        public int Throw() => random.Next(1, Sides + 1);

        public override string ToString() => $"DICE - Sides: {Sides}";
    }
}
