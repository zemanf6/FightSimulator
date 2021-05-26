using System;
using System.Threading;

namespace FightSimulator
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();

            app.InitializeComponent();
            app.Run();
        }
    }
}
