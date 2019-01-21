using System;
using System.Diagnostics;
using System.Windows.Forms;
using SbzProfileSwitcherTray.Properties;
using SbzProfileSwitcherCli;
using Microsoft.Win32;

namespace SbzProfileSwitcherTray
{
    class ContextMenus
    {
        SwitcherLib switcherLib;

        public ContextMenuStrip Create(SwitcherLib switcherLib)
        {
            this.switcherLib = switcherLib;

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripMenuItem optionsMenu;
            ToolStripSeparator sep;

            // Add each profile
            int profileIdx = 0;
            foreach (var profile in switcherLib.GetProfileNames())
            {
                int thisProfileIdx = profileIdx;
                item = new ToolStripMenuItem();
                item.Text = profile;
                item.Click += delegate (object sender, EventArgs e)
                {
                    Profile_Click(sender, e, thisProfileIdx);
                };
                profileIdx++;
                menu.Items.Add(item);
            }

            // Separator
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Options Menu
            optionsMenu = new ToolStripMenuItem();
            optionsMenu.Text = "Options";
            menu.Items.Add(optionsMenu);

            // Config
            item = new ToolStripMenuItem();
            item.Text = "Edit Config";
            item.Click += new EventHandler(Edit_Config_Click);
            optionsMenu.DropDownItems.Add(item);

            item = new ToolStripMenuItem();
            item.Text = "Reload Config";
            item.Click += new EventHandler(Reload_Config_Click);
            optionsMenu.DropDownItems.Add(item);

            item = new ToolStripMenuItem();
            item.Text = "Show Device Names";
            item.Click += new EventHandler(Device_Names_Click);
            optionsMenu.DropDownItems.Add(item);

            item = new ToolStripMenuItem();
            item.Text = "Enable Windows Startup";
            item.Click += delegate (object sender, EventArgs e)
            {
                Windows_Startup_Click(sender, e, true);
            };
            optionsMenu.DropDownItems.Add(item);

            item = new ToolStripMenuItem();
            item.Text = "Disable Windows Startup";
            item.Click += delegate (object sender, EventArgs e)
            {
                Windows_Startup_Click(sender, e, false);
            };
            optionsMenu.DropDownItems.Add(item);

            // Separator
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit.
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new EventHandler(Exit_Click);
            menu.Items.Add(item);

            return menu;
        }

        void Profile_Click(object sender, EventArgs e, int profileIdx)
        {
            switcherLib.SetProfile(profileIdx);
        }

        void Windows_Startup_Click(object sender, EventArgs e, bool enable)
        {
            var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
            if (enable)
            {
                key.SetValue("SbzProfileSwitcher", Application.ExecutablePath.ToString());
            }
            else
            {
                key.DeleteValue("SbzProfileSwitcher");
            }
            
        }

        void Edit_Config_Click(object sender, EventArgs e)
        {
            Process.Start("notepad", switcherLib.ConfigPath);
        }

        void Reload_Config_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        void Device_Names_Click(object sender, EventArgs e)
        {
            string[] deviceNames = SwitcherLib.GetAudioDeviceNames();
            NotepadHelper.ShowMessage(string.Join("\r\n",deviceNames));
        }

        void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}