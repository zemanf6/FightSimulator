using System.Diagnostics;

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
