# Sound Blaster Z Profile Switcher
System Tray Applet and Command Line exe to switch between SBZ configurations quickly
![alt text](https://raw.githubusercontent.com/carlalldis/SbzProfileSwitcher/master/Tray.png)
# Why
I previously used SBZ Switcher (https://sourceforge.net/projects/sbzswitcher/) however while brilliant, it's use of AutoHotkey and it's requirement for elevation concerned me. I sought to use APIs as a more elegant solution and found someone had beat me to it (https://sourceforge.net/projects/soundblaster-z-switcher/). This exe simply switched between Speaker/Headphones mode but I required the ability to change the different options in the SBZ control panel, much like SBZ Switcher could already do. I asked CJTheTiger, the author of this app if I could see his source code; he kindly agreed to let me use and publish his code here.
# Usage
- Extract all files to "C:\Program Files (x86)\Creative\Sound Blaster Z-Series\Sound Blaster Z-Series Control Panel"
- Configure your speaker/headphone setups in SbzProfileSwitcher.json as per the example.
- Run SbzProfileSwitcherCli.exe to switch profiles at the command line
- Run SbzProfileSwitcherTray.exe to start the system tray applet. Right-click this to choose a profile.
# References
- Creative Soundblaster Z Switcher: https://sourceforge.net/projects/soundblaster-z-switcher/
- SetAudioLevel: https://eskerahn.dk/wordpress/?p=2089
- QuickSoundSwitch: https://github.com/promythyus/QuickSoundSwitch
- Newtonsoft.Json: https://github.com/JamesNK/Newtonsoft.Json
- System Tray Code: https://www.codeproject.com/Articles/290013/Formless-System-Tray-Application
# Licence
MIT License
