using System;

namespace SbzProfileSwitcherCli
{
    class Program
    {
        static SwitcherLib switcherLib = new SwitcherLib("SbzProfileSwitcher.json", false);
        static string[] profiles = switcherLib.GetProfileNames();

        public static void Main(string[] args)
        {
            if (args.Length != 1) { ExitError(); }
            try
            {
                int profile = int.Parse(args[0]);
                if (profile > (profiles.Length - 1)) { ExitError(); }
                switcherLib.SetProfile(profile);
            }
            catch
            {
                ExitError();
            }
        }

        static void ExitError()
        {
            Console.WriteLine("Usage: SbzProfileSwitcherCli.exe <int>");
            int i = 0;
            foreach (var profile in profiles)
            {
                Console.WriteLine(i + ": " + profile);
                i++;
            }
            Environment.Exit(1);
        }
    }
}
