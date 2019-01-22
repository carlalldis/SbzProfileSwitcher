using System;
using System.Windows.Forms;

namespace SbzProfileSwitcherTray
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                using (ProcessIcon pi = new ProcessIcon())
                {
                    pi.Display();
                    Application.Run();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
    }
}
