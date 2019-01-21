// Decompiled with JetBrains decompiler
// Type: Util
// Assembly: SBZ, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null
// MVID: F576CC9B-1B1F-4112-BD5C-14BC70FE47C8
// Assembly location: C:\Program Files (x86)\Creative\Sound Blaster Z-Series\Sound Blaster Z-Series Control Panel\SBZ.exe

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

internal class Util
{
    private static bool bShowDebugText = false;
    public static string CmdLineSpkMode = "/spkmode";
    public static string CmdLineEpID = "/epid";
    public static string CmdLineSpeaker = "/spk";
    private const string strDateTimeFormat = "M/d/yyyy h:mm:ss tt";

    public static string GetAppExeNameWithoutExt()
    {
        return Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName).Replace(".vshost", "");
    }

    public static string GetSaveGuidFullName(string ADestFolder, string ADestExt)
    {
        string str1 = Guid.NewGuid().ToString().ToLower().Replace("-", "");
        string path;
        string str2;
        for (path = Path.Combine(ADestFolder, str1 + ADestExt); File.Exists(path); path = Path.Combine(ADestFolder, str2 + ADestExt))
            str2 = Guid.NewGuid().ToString().ToLower().Replace("-", "");
        return path;
    }

    public static string GetSaveGuidFullName(string ADestFolder, string ADestExt, string[] AExceptList)
    {
        string path;
        do
        {
            string str;
            bool flag;
            do
            {
                str = Guid.NewGuid().ToString().ToLower().Replace("-", "");
                flag = false;
                foreach (string aexcept in AExceptList)
                {
                    if (str + ADestExt == aexcept)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            while (flag);
            path = Path.Combine(ADestFolder, str + ADestExt);
        }
        while (File.Exists(path));
        return path;
    }

    public static int ConvertStringToInt(string AStrValue)
    {
        try
        {
            return Convert.ToInt32(AStrValue);
        }
        catch
        {
            return 0;
        }
    }

    public static float ConvertStringToFloat(string AValue)
    {
        try
        {
            return Convert.ToSingle(AValue, (IFormatProvider)CultureInfo.InvariantCulture);
        }
        catch
        {
            return 0.0f;
        }
    }

    public static string ConvertToString(ushort[] us)
    {
        char[] chArray = new char[us.Length];
        Array.Copy((Array)us, (Array)chArray, us.Length);
        string str = new string(chArray);
        int length = str.IndexOf("\0");
        if (length >= 0)
            return str.Substring(0, length);
        return str;
    }

    public static string ConvertToString(sbyte[] sb)
    {
        return Encoding.ASCII.GetString(Array.ConvertAll<sbyte, byte>(sb, (Converter<sbyte, byte>)(a => (byte)a)));
    }

    public static string ConvertToUnicodeString(byte[] bytearray)
    {
        string str = Encoding.Unicode.GetString(bytearray);
        int length = str.IndexOf("\0");
        if (length > 0)
            return str.Substring(0, length);
        return str;
    }

    public static string ConvertToStringX(byte[] bytearray)
    {
        string str = "";
        for (int index = 0; index < bytearray.Length; ++index)
            str += bytearray[index].ToString("X2");
        return str;
    }

    public static DateTime ConvertStringToUSDateTime(string ADateTimeStr)
    {
        return DateTime.ParseExact(ADateTimeStr, "M/d/yyyy h:mm:ss tt", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"));
    }

    public static string ConvertUSDateTimeToString(DateTime ADateTime)
    {
        return ADateTime.ToString("M/d/yyyy h:mm:ss tt", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"));
    }

    [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnString, int nSize, string lpFilename);

    [DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    private static extern int WritePrivateProfileStringW(string lpAppName, string lpKeyName, string lpString, string lpFilename);

    public static string GetIniFileString(string iniFile, string category, string key, string defaultValue)
    {
        string lpReturnString = new string(' ', 1024);
        Util.GetPrivateProfileString(category, key, defaultValue, lpReturnString, 1024, iniFile);
        return lpReturnString.Split(new char[1])[0];
    }

    public static int WriteIniFileString(string iniFile, string category, string key, string value)
    {
        return Util.WritePrivateProfileStringW(category, key, value, iniFile);
    }

    public static string[] ParseCmdlineStr(string cmdlineStr)
    {
        if (cmdlineStr == null)
            return new string[0];
        char[] charArray = cmdlineStr.ToCharArray();
        bool flag = false;
        for (int index = 0; index < charArray.Length; ++index)
        {
            if ((int)charArray[index] == 34)
                flag = !flag;
            if (!flag && (int)charArray[index] == 32)
                charArray[index] = '\n';
        }
        return new string(charArray).Replace("\"", "").Split('\n');
    }

    public static void BreakdownCmdline(string AString, out string AKey, out string AValue)
    {
        string lower = AString.ToLower();
        if (lower.StartsWith(Util.CmdLineSpkMode + "="))
        {
            AKey = Util.CmdLineSpkMode;
            AValue = AString.Substring(Util.CmdLineSpkMode.Length + 1);
        }
        else if (lower.StartsWith(Util.CmdLineEpID + "="))
        {
            AKey = Util.CmdLineEpID;
            AValue = AString.Substring(Util.CmdLineEpID.Length + 1);
        }
        else if (lower.StartsWith(Util.CmdLineSpeaker + "="))
        {
            AKey = Util.CmdLineSpeaker;
            AValue = AString.Substring(Util.CmdLineSpeaker.Length + 1);
        }
        else
        {
            AKey = AString;
            AValue = AString;
        }
    }

    public static bool IsTextTrimming(TextBlock ATextBlock)
    {
        ATextBlock.UpdateLayout();
        Typeface typeface = new Typeface(ATextBlock.FontFamily, ATextBlock.FontStyle, ATextBlock.FontWeight, ATextBlock.FontStretch);
        return new FormattedText(ATextBlock.Text, Thread.CurrentThread.CurrentCulture, ATextBlock.FlowDirection, typeface, ATextBlock.FontSize, ATextBlock.Foreground) { Trimming = TextTrimming.None, MaxTextWidth = ATextBlock.ActualWidth }.Height > ATextBlock.ActualHeight;
    }

    public static void SetShowDebugText(bool AEnable)
    {
        Util.bShowDebugText = AEnable;
    }

    public static void PrintDebug(string AMsg)
    {
        if (!Util.bShowDebugText)
            return;
        Trace.WriteLine("=CT= " + AMsg);
    }

    public static void WriteLogFile(string AMsg)
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "SoundBlaster.log");
        try
        {
            StreamWriter streamWriter = File.Exists(path) ? File.AppendText(path) : new StreamWriter(path);
            streamWriter.WriteLine(DateTime.Now.ToString() + " " + AMsg);
            streamWriter.Close();
        }
        catch (Exception ex)
        {
        }
    }

    public static int LaunchProcess(string AFilename, string AParam)
    {
        int num = -1;
        if (File.Exists(AFilename))
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = AFilename;
                process.StartInfo.Arguments = AParam;
                process.Start();
                process.WaitForExit();
                num = process.ExitCode;
                process.Close();
            }
            catch
            {
            }
        }
        return num;
    }

    public static string GetSpecialFolder(Util.SpecialFolderCSIDL folder)
    {
        StringBuilder pszPath = new StringBuilder(260);
        Util.SHGetFolderPath(IntPtr.Zero, (int)folder, IntPtr.Zero, 0U, pszPath);
        return pszPath.ToString();
    }

    [DllImport("shell32.dll")]
    private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, [Out] StringBuilder pszPath);

    public static string GetHKLMRegistryAppPathsKey(string AAppPathsKey)
    {
        return Util.GetHKLMRegistryKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + AAppPathsKey, "");
    }

    public static string GetHKLMRegistryKey(string APath, string AKey)
    {
        string str1 = "";
        try
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(APath);
            if (registryKey != null)
            {
                string str2 = (string)registryKey.GetValue(AKey);
                if (str2 != null)
                    str1 = str2;
                registryKey.Close();
            }
        }
        catch (Exception ex)
        {
        }
        return str1;
    }

    [DllImport("user32.dll")]
    internal static extern bool ClientToScreen(IntPtr hWnd, ref Util.POINT lpPoint);

    [DllImport("user32.dll")]
    internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    public enum SpecialFolderCSIDL
    {
        CSIDL_DESKTOP = 0,
        CSIDL_INTERNET = 1,
        CSIDL_PROGRAMS = 2,
        CSIDL_CONTROLS = 3,
        CSIDL_PRINTERS = 4,
        CSIDL_PERSONAL = 5,
        CSIDL_FAVORITES = 6,
        CSIDL_STARTUP = 7,
        CSIDL_RECENT = 8,
        CSIDL_SENDTO = 9,
        CSIDL_BITBUCKET = 10,
        CSIDL_STARTMENU = 11,
        CSIDL_DESKTOPDIRECTORY = 16,
        CSIDL_DRIVES = 17,
        CSIDL_NETWORK = 18,
        CSIDL_NETHOOD = 19,
        CSIDL_FONTS = 20,
        CSIDL_TEMPLATES = 21,
        CSIDL_COMMON_STARTMENU = 22,
        CSIDL_COMMON_PROGRAMS = 23,
        CSIDL_COMMON_STARTUP = 24,
        CSIDL_COMMON_DESKTOPDIRECTORY = 25,
        CSIDL_APPDATA = 26,
        CSIDL_PRINTHOOD = 27,
        CSIDL_LOCAL_APPDATA = 28,
        CSIDL_ALTSTARTUP = 29,
        CSIDL_COMMON_ALTSTARTUP = 30,
        CSIDL_COMMON_FAVORITES = 31,
        CSIDL_INTERNET_CACHE = 32,
        CSIDL_COOKIES = 33,
        CSIDL_HISTORY = 34,
        CSIDL_COMMON_APPDATA = 35,
        CSIDL_WINDOWS = 36,
        CSIDL_SYSTEM = 37,
        CSIDL_PROGRAM_FILES = 38,
        CSIDL_MYPICTURES = 39,
        CSIDL_PROFILE = 40,
        CSIDL_SYSTEMX86 = 41,
        CSIDL_PROGRAM_FILESX86 = 42,
        CSIDL_PROGRAM_FILES_COMMON = 43,
        CSIDL_PROGRAM_FILES_COMMONX86 = 44,
        CSIDL_COMMON_TEMPLATES = 45,
        CSIDL_COMMON_DOCUMENTS = 46,
        CSIDL_COMMON_ADMINTOOLS = 47,
        CSIDL_ADMINTOOLS = 48,
        CSIDL_CONNECTIONS = 49,
        CSIDL_CDBURN_AREA = 59,
    }

    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public POINT(Point pt)
        {
            this.X = Convert.ToInt32(pt.X);
            this.Y = Convert.ToInt32(pt.Y);
        }
    }
}
