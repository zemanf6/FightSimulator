using System;
using System.Diagnostics;
using System.Linq;

namespace FightSimulator
{
    static class Utilities
    {
        public static void Launch_URL(string url)
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
