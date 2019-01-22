using SbzProfileSwitcherTray.Properties;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using SbzProfileSwitcherCli;

namespace SbzProfileSwitcherTray
{
    class ProcessIcon : IDisposable
    {
        NotifyIcon ni;
        public SwitcherLib switcherLib;

        public ProcessIcon()
        {
            ni = new NotifyIcon();
            try
            {
                switcherLib = new SwitcherLib("SbzProfileSwitcher.json", true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to SBZ Control Panel: " + ex.Message);
            }
        }

        public void Display()
        {	
            ni.MouseClick += new MouseEventHandler(ni_MouseClick);
            ni.Icon = Resources.SystemTrayApp;
            ni.Text = "SBZ Profile Switcher";
            ni.Visible = true;

            ni.ContextMenuStrip = new ContextMenus().Create(switcherLib);
        }

        public void Dispose()
        {
            ni.Dispose();
        }

        void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Process.Start("SBZ.exe", null);
            }
        }
    }
}
