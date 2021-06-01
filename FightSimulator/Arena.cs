using System;
using System.Threading;
using FightSimulator.Classes;

namespace FightSimulator
{
    class Arena
    {
        private readonly Warrior warrior1;
        private readonly Warrior warrior2;
        private readonly Dice dice;

        public Arena(Warrior warrior1, Warrior warrior2, Dice dice)
        {
            this.warrior1 = warrior1;
            this.warrior2 = warrior2;
            this.dice = dice;
        }

        public void Fight()
        {
            Warrior firstFighter = warrior1;
            Warrior secondFighter = warrior2;

            Console.WriteLine($"Fight between {warrior1} and {warrior2}.\n");

            if (dice.Throw() <= dice.Sides / 2)
                (firstFighter, secondFighter) = (secondFighter, firstFighter);

            Console.WriteLine($"{firstFighter} ambushes {secondFighter}!...\n");
            Thread.Sleep(1200);

            while (firstFighter.IsAlive() && secondFighter.IsAlive())
            {
                FightCycle(firstFighter, secondFighter);

                if (secondFighter.IsAlive())
                {
                    FightCycle(secondFighter, firstFighter);
                }
            }
        }

        private void FightCycle(Warrior w1, Warrior w2)
        {
            w1.AttackEnemy(w2);
            Draw();
            WriteMessage(w1.Message);
            WriteMessage(w2.Message);
        }

        private static void DrawFighter(Warrior f)
        {
            Console.WriteLine(f);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(f.GraphicHealth());
            Console.ResetColor();

            if (f is Wizard wizard)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine(wizard.GraphicMana());
                Console.ResetColor();

            }
            Console.WriteLine();

        }

        private void Draw()
        {
            Console.Clear();
            Console.WriteLine(@"   _____                              
  /  _  \_______  ____   ____ _____   
 /  /_\  \_  __ _/ __ \ /    \\__  \  
/    |    |  | \\  ___/|   |  \/ __ \_
\____|__  |__|   \___  |___|  (____  /
        \/           \/     \/     \/ ");
            Console.WriteLine();
            DrawFighter(warrior1);
            DrawFighter(warrior2);
        }

        private static void WriteMessage(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(750);
        }
    }
}
