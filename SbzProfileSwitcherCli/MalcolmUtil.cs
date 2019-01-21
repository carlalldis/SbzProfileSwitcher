// Decompiled with JetBrains decompiler
// Type: Malcolm.MalcolmUtil
// Assembly: SBZ, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null
// MVID: F576CC9B-1B1F-4112-BD5C-14BC70FE47C8
// Assembly location: C:\Program Files (x86)\Creative\Sound Blaster Z-Series\Sound Blaster Z-Series Control Panel\SBZ.exe

using CTSoundCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Malcolm
{
    internal class MalcolmUtil
    {
        public static char[] InvalidNameChars = new char[2] { '<', '>' };
        public static string MalThemeBlue = "Blue";
        public static string MalThemeRed = "Red";
        public static string MalThemeGamma = "Gamma";
        public static string MalThemeEpsilon = "Epsln";
        private static bool bInternalTest = false;
        private static bool bCustomizedPagesTest = false;
        private static bool bThemeTest = false;
        private static string JackSetupFilename = "CTJckCfg.exe";
        public const uint SPEAKERTYPE_BITMASK_EXTERNAL = 0;
        public const uint SPEAKERTYPE_BITMASK_INTERNAL = 1073741824;
        public const uint SPEAKERTYPE_BITMASK_HEADPHONES = 2147483648;
        public const uint SPEAKERTYPE_EXTERNAL = 0;
        public const uint SPEAKERTYPE_INTERNAL = 1073741824;
        public const uint SPEAKERTYPE_HEADPHONES = 2147483648;
        public const uint SPEAKERCONFIG_SPEAKERS = 33554432;
        public const uint OVERRIDE_SPKTEST_DEFAULT_BEHAVIOR = 67108864;
        public const uint SPEAKERCONFIG_DIRECT_192KHZ = 134217728;
        public const uint SPEAKER_FRONT_LEFT = 1;
        public const uint SPEAKER_FRONT_RIGHT = 2;
        public const uint SPEAKER_FRONT_CENTER = 4;
        public const uint SPEAKER_LOW_FREQUENCY = 8;
        public const uint SPEAKER_BACK_LEFT = 16;
        public const uint SPEAKER_BACK_RIGHT = 32;
        public const uint SPEAKER_FRONT_LEFT_OF_CENTER = 64;
        public const uint SPEAKER_FRONT_RIGHT_OF_CENTER = 128;
        public const uint SPEAKER_BACK_CENTER = 256;
        public const uint SPEAKER_SIDE_LEFT = 512;
        public const uint SPEAKER_SIDE_RIGHT = 1024;
        public const uint SPEAKER_TOP_CENTER = 2048;
        public const uint SPEAKER_TOP_FRONT_LEFT = 4096;
        public const uint SPEAKER_TOP_FRONT_CENTER = 8192;
        public const uint SPEAKER_TOP_FRONT_RIGHT = 16384;
        public const uint SPEAKER_TOP_BACK_LEFT = 32768;
        public const uint SPEAKER_TOP_BACK_CENTER = 65536;
        public const uint SPEAKER_TOP_BACK_RIGHT = 131072;
        public const uint KSAUDIO_SPEAKER_MONO = 4;
        public const uint KSAUDIO_SPEAKER_STEREO = 3;
        public const uint KSAUDIO_SPEAKER_QUAD = 51;
        public const uint KSAUDIO_SPEAKER_SURROUND = 263;
        public const uint KSAUDIO_SPEAKER_5POINT1 = 63;
        public const uint KSAUDIO_SPEAKER_7POINT1 = 255;
        public const uint KSAUDIO_SPEAKER_5POINT1_SURROUND = 1551;
        public const uint KSAUDIO_SPEAKER_7POINT1_SURROUND = 1599;
        public const uint KSAUDIO_SPEAKER_5POINT1_BACK = 63;
        public const uint KSAUDIO_SPEAKER_7POINT1_WIDE = 255;
        public const uint SPEAKERCONFIG_HEADPHONES_7POINT1_VIRTUAL_SURROUND = 2214606399;
        public const uint SPEAKERCONFIG_HEADPHONES_5POINT1 = 2147741759;
        public const uint SPEAKERCONFIG_HEADPHONES_STEREO = 2147495939;
        public const uint SPEAKERCONFIG_EXTERNAL_STEREO = 12291;
        public const uint SPEAKERCONFIG_EXTERNAL_STEREO_DIRECT_192KHZ = 134230019;
        public const uint SPEAKERCONFIG_EXTERNAL_QUAD = 208947;
        public const uint SPEAKERCONFIG_EXTERNAL_SURROUND = 1077511;
        public const uint SPEAKERCONFIG_EXTERNAL_4POINT1_SURROUND = 6305283;
        public const uint SPEAKERCONFIG_EXTERNAL_5POINT1 = 258111;
        public const uint SPEAKERCONFIG_EXTERNAL_5POINT1_SURROUND = 6354447;
        public const uint SPEAKERCONFIG_EXTERNAL_6POINT1 = 1306943;
        public const uint SPEAKERCONFIG_EXTERNAL_6POINT1_SURROUND = 7403279;
        public const uint SPEAKERCONFIG_EXTERNAL_7POINT1 = 1044735;
        public const uint SPEAKERCONFIG_EXTERNAL_7POINT1_SURROUND = 6551103;
        public const uint SPEAKERCONFIG_INTERNAL_STEREO = 1073754115;
        public const uint SPEAKERCONFIG_EXTERNAL_PSEUDO_7POINT1 = 17036863;
        public const uint SC_MalcolmDeviceConfig_MC_5POINT1 = 4128768;
        public const uint SC_MalcolmDeviceConfig_MC_QUAD = 3342336;
        public const uint SC_MalcolmDeviceConfig_MC_5POINT1_LineIn = 4128769;
        public const uint SC_MalcolmDeviceConfig_MC_5POINT1_MicIn = 4128770;
        public const uint SC_MalcolmDeviceConfig_SC_LineIn = 196609;
        public const uint SC_MalcolmDeviceConfig_SC_MicIn = 196610;
        public const uint SC_MalcolmDeviceConfig_SC_LineIn2 = 196611;
        public const uint SC_MalcolmDeviceConfig_SC_MicIn2 = 196612;
        public const uint SC_MalcolmDeviceConfig_HP_LineIn = 2147483649;
        public const uint SC_MalcolmDeviceConfig_HP_MicIn = 2147483650;
        public const uint SC_MalcolmDeviceConfig_HP_LineIn2 = 2147483651;
        public const uint SC_MalcolmDeviceConfig_HP_MicIn2 = 2147483652;
        public const uint SC_MalcolmDeviceConfig_MC_Pseudo7POINT1 = 104796160;
        public const uint SC_MalcolmDeviceConfig_Dell_MC_5POINT1 = 4161536;
        public const uint SC_MalcolmDeviceConfig_Dell_SC_LineIn2 = 229379;
        public const uint SC_MalcolmDeviceConfig_Dell_SC_MicIn2 = 229380;
        public const uint SC_MalcolmDeviceConfig_Dell_HP_LineIn2 = 2147516419;
        public const uint SC_MalcolmDeviceConfig_Dell_HP_MicIn2 = 2147516420;
        public const uint SC_MalcolmDeviceConfig_Dell_AHS_LineIn2 = 2147524611;
        public const uint SC_MalcolmDeviceConfig_Dell_AHS_MicIn2 = 2147524612;
        public const uint SC_MalcolmDeviceConfig_Dell_NHS_LineIn2 = 2147532803;
        public const uint SC_MalcolmDeviceConfig_Dell_NHS_MicIn2 = 2147532804;
        public const uint SC_MalcolmDeviceConfig_Dell_HP_HP = 2147516416;
        public const uint SC_MalcolmDeviceConfig_Dell_NHS_HP = 2147532800;
        public const uint PARAMATTRIB_READONLY = 1;
        public const uint PARAMATTRIB_VOLATILE = 2;
        private static MalcolmControl MalCtrl;

        public static string GetResourceString(string ResName)
        {
            string str = "";
            try
            {
                str = (string)Application.Current.FindResource((object)ResName);
            }
            catch (Exception ex)
            {
            }
            return str;
        }

        public static double GetResourceDouble(string ResName)
        {
            double num = -1.0;
            try
            {
                num = (double)Application.Current.FindResource((object)ResName);
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public static Style GetResourceStyle(string ResName)
        {
            Style style = (Style)null;
            try
            {
                style = (Style)Application.Current.FindResource((object)ResName);
            }
            catch (Exception ex)
            {
            }
            return style;
        }

        public static ImageSource GetResourceImageBrush(string ResName)
        {
            ImageSource imageSource = (ImageSource)null;
            try
            {
                imageSource = (ImageSource)Application.Current.FindResource((object)ResName);
            }
            catch (Exception ex)
            {
            }
            return imageSource;
        }

        public static SolidColorBrush GetResourceSolidColorBrush(string ResName)
        {
            SolidColorBrush solidColorBrush = Brushes.White;
            try
            {
                solidColorBrush = (SolidColorBrush)Application.Current.FindResource((object)ResName);
            }
            catch (Exception ex)
            {
            }
            return solidColorBrush;
        }

        public static SolidColorBrush GetStandardPresetTextColor()
        {
            return MalcolmUtil.GetResourceSolidColorBrush("StandardPresetTextBrush");
        }

        public static SolidColorBrush GetCustomPresetTextColor()
        {
            return MalcolmUtil.GetResourceSolidColorBrush("CustomPresetTextBrush");
        }

        public static SolidColorBrush GetComboboxTextColor()
        {
            return MalcolmUtil.GetResourceSolidColorBrush("ComboboxTextBrush");
        }

        public static SolidColorBrush GetAlertTextColor()
        {
            return Brushes.Red;
        }

        public static SolidColorBrush GetMainPageFontColor()
        {
            return MalcolmUtil.GetResourceSolidColorBrush("MainFontBrush");
        }

        public static SolidColorBrush GetMainPageHighlightFontColor()
        {
            return MalcolmUtil.GetResourceSolidColorBrush("MainHighlightFontBrush");
        }

        public static SolidColorBrush GetMainPageDisabledFontColor()
        {
            return MalcolmUtil.GetResourceSolidColorBrush("MainFontDisableBrush");
        }

        public static string GetMenuTextRunAtStartup()
        {
            return string.Format(MalcolmUtil.GetResourceString("resStrRunAtStartup"), (object)App.AppTitle);
        }

        public static string GetCustomPresentName()
        {
            return "<" + MalcolmUtil.GetResourceString("resStrCustom") + ">";
        }

        public static string GetEqualizerFactoryPresetName(int APresetID)
        {
            switch (APresetID)
            {
                case 1:
                    return MalcolmUtil.GetResourceString("resStrPresetFlat");
                case 2:
                    return MalcolmUtil.GetResourceString("resStrPresetAcoustic");
                case 3:
                    return MalcolmUtil.GetResourceString("resStrPresetClassical");
                case 4:
                    return MalcolmUtil.GetResourceString("resStrPresetCountry");
                case 5:
                    return MalcolmUtil.GetResourceString("resStrPresetDance");
                case 6:
                    return MalcolmUtil.GetResourceString("resStrPresetJazz");
                case 7:
                    return MalcolmUtil.GetResourceString("resStrPresetNewAge");
                case 8:
                    return MalcolmUtil.GetResourceString("resStrPresetPop");
                case 9:
                    return MalcolmUtil.GetResourceString("resStrPresetRock");
                case 10:
                    return MalcolmUtil.GetResourceString("resStrPresetVocal");
                default:
                    return "";
            }
        }

        public static string GetVoiceFXFactoryPresetName(int APresetID)
        {
            switch (APresetID)
            {
                case 1:
                    return MalcolmUtil.GetResourceString("resStrPresetNeutral");
                case 2:
                    return MalcolmUtil.GetResourceString("resStrPresetFemale2Male");
                case 3:
                    return MalcolmUtil.GetResourceString("resStrPresetMale2Female");
                case 4:
                    return MalcolmUtil.GetResourceString("resStrPresetScrappyKid");
                case 5:
                    return MalcolmUtil.GetResourceString("resStrPresetElderly");
                case 6:
                    return MalcolmUtil.GetResourceString("resStrPresetOrc");
                case 7:
                    return MalcolmUtil.GetResourceString("resStrPresetElf");
                case 8:
                    return MalcolmUtil.GetResourceString("resStrPresetDwarf");
                case 9:
                    return MalcolmUtil.GetResourceString("resStrPresetAlienBrute");
                case 10:
                    return MalcolmUtil.GetResourceString("resStrPresetAlienInfiltrator");
                case 11:
                    return MalcolmUtil.GetResourceString("resStrPresetRobot");
                case 12:
                    return MalcolmUtil.GetResourceString("resStrPresetMarine");
                case 13:
                    return MalcolmUtil.GetResourceString("resStrPresetEmo");
                case 14:
                    return MalcolmUtil.GetResourceString("resStrPresetUnstable");
                case 15:
                    return MalcolmUtil.GetResourceString("resStrPresetFromUpNorth");
                case 16:
                    return MalcolmUtil.GetResourceString("resStrPresetDeepVoice");
                case 17:
                    return MalcolmUtil.GetResourceString("resStrPresetMunchkin");
                case 18:
                    return MalcolmUtil.GetResourceString("resStrPresetDemon");
                case 100:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMDeepVoice");
                case 101:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMDepressed");
                case 102:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMDistressed");
                case 103:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMDucky");
                case 104:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMEmotional");
                case 105:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMExcited");
                case 106:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMFemaletomale");
                case 107:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMKid");
                case 108:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMMaletofemale");
                case 109:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMMunchkin");
                case 110:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMNasal");
                case 111:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMOldVoice");
                case 112:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMRobot");
                case 113:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMStrangeaccent1");
                case 114:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMStrangeaccent2");
                case 115:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMStrangeaccent3");
                case 116:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMStrangeaccent4");
                case 117:
                    return MalcolmUtil.GetResourceString("resStrPresetOEMStrangeaccent5");
                default:
                    return "";
            }
        }

        public static string GetMicReverbFactoryPresetName(int APresetID)
        {
            switch (APresetID)
            {
                case 1:
                    return MalcolmUtil.GetResourceString("resStrPresetAuditorium");
                case 2:
                    return MalcolmUtil.GetResourceString("resStrPresetBallPark");
                case 3:
                    return MalcolmUtil.GetResourceString("resStrPresetBathroom");
                case 4:
                    return MalcolmUtil.GetResourceString("resStrPresetCathedral");
                case 5:
                    return MalcolmUtil.GetResourceString("resStrPresetChapel");
                case 6:
                    return MalcolmUtil.GetResourceString("resStrPresetChurch");
                case 7:
                    return MalcolmUtil.GetResourceString("resStrPresetConcertHall");
                case 8:
                    return MalcolmUtil.GetResourceString("resStrPresetDefault");
                case 9:
                    return MalcolmUtil.GetResourceString("resStrPresetGarage");
                case 10:
                    return MalcolmUtil.GetResourceString("resStrPresetHauntedCavern");
                case 11:
                    return MalcolmUtil.GetResourceString("resStrPresetIndoorArena");
                case 12:
                    return MalcolmUtil.GetResourceString("resStrPresetJazzClub");
                case 13:
                    return MalcolmUtil.GetResourceString("resStrPresetLivingRoom");
                case 14:
                    return MalcolmUtil.GetResourceString("resStrPresetOperaHouse");
                case 15:
                    return MalcolmUtil.GetResourceString("resStrPresetPsychotic");
                case 16:
                    return MalcolmUtil.GetResourceString("resStrPresetRecordingStudio");
                case 17:
                    return MalcolmUtil.GetResourceString("resStrPresetSmallRoom");
                case 18:
                    return MalcolmUtil.GetResourceString("resStrPresetTheater");
                case 19:
                    return MalcolmUtil.GetResourceString("resStrPresetUnderwater");
                default:
                    return "";
            }
        }

        public static string GetMicEQFactoryPresetName(int APresetID)
        {
            switch (APresetID)
            {
                case 1:
                    return MalcolmUtil.GetResourceString("resStrPresetFlat");
                case 2:
                    return MalcolmUtil.GetResourceString("resStrPreset1");
                case 3:
                    return MalcolmUtil.GetResourceString("resStrPreset2");
                case 4:
                    return MalcolmUtil.GetResourceString("resStrPreset3");
                case 5:
                    return MalcolmUtil.GetResourceString("resStrPreset4");
                case 6:
                    return MalcolmUtil.GetResourceString("resStrPreset5");
                case 7:
                    return MalcolmUtil.GetResourceString("resStrPreset6");
                case 8:
                    return MalcolmUtil.GetResourceString("resStrPreset7");
                case 9:
                    return MalcolmUtil.GetResourceString("resStrPreset8");
                case 10:
                    return MalcolmUtil.GetResourceString("resStrPreset9");
                case 11:
                    return MalcolmUtil.GetResourceString("resStrPreset10");
                case 12:
                    return MalcolmUtil.GetResourceString("resStrPresetDM1");
                default:
                    return "";
            }
        }

        public static string GetSpeakerEQPresetName(uint APresetID)
        {
            switch (APresetID)
            {
                case 0:
                    return MalcolmUtil.GetResourceString("resStrNone");
                case 1:
                    return MalcolmUtil.GetResourceString("resStrStereoHeadphones");
                default:
                    return "";
            }
        }

        public static string GetMultiplexInputsName(uint AInputID)
        {
            switch (AInputID)
            {
                case 0:
                    return MalcolmUtil.GetResourceString("resStrRearMic");
                case 1:
                    return MalcolmUtil.GetResourceString("resStrFrontAuxiliary");
                case 2:
                    return MalcolmUtil.GetResourceString("resStrFrontMic");
                case 3:
                    return MalcolmUtil.GetResourceString("resStrRearLineIn");
                case 4:
                    return MalcolmUtil.GetResourceString("resStrMic");
                case 5:
                    return MalcolmUtil.GetResourceString("resStrLineIn");
                default:
                    return "";
            }
        }

        private static bool GetMalcolmIniFlag(string category, string key, string defaultvalue, ref string value)
        {
            string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Malcolm.ini");
            if (!File.Exists(str))
                return false;
            string iniFileString = Util.GetIniFileString(str, category, key, defaultvalue);
            value = !(iniFileString != "") ? defaultvalue : iniFileString;
            return true;
        }

        public static bool IsInternalTest()
        {
            return MalcolmUtil.bInternalTest;
        }

        public static void SetInternalTest()
        {
            string defaultvalue = "1";
            if (MalcolmUtil.GetMalcolmIniFlag("Settings", "InternalTest", defaultvalue, ref defaultvalue))
                MalcolmUtil.bInternalTest = defaultvalue == "1";
            if (!MalcolmUtil.bInternalTest)
                return;
            MalcolmUtil.SetCustomizedPagesTest();
        }

        public static bool IsCustomizedPagesTest()
        {
            return MalcolmUtil.bCustomizedPagesTest;
        }

        public static void SetCustomizedPagesTest()
        {
            MalcolmUtil.bCustomizedPagesTest = MalcolmUtil.GetTestFlag("CustomizedPagesTest");
        }

        public static bool HasTestPage(string APageKey)
        {
            string defaultvalue = "1";
            if (MalcolmUtil.GetMalcolmIniFlag("Pages", APageKey, defaultvalue, ref defaultvalue))
                return defaultvalue == "1";
            return false;
        }

        public static bool IsThemeTest()
        {
            return MalcolmUtil.bThemeTest;
        }

        public static void SetThemeTest()
        {
            MalcolmUtil.bThemeTest = MalcolmUtil.GetTestFlag("ThemeTest");
        }

        public static void CheckShowDebugText()
        {
            Util.SetShowDebugText(MalcolmUtil.GetTestFlag("ShowDebugText"));
        }

        private static bool GetTestFlag(string ATestFlag)
        {
            string defaultvalue = "0";
            if (MalcolmUtil.GetMalcolmIniFlag("Settings", ATestFlag, defaultvalue, ref defaultvalue))
                return defaultvalue == "1";
            return false;
        }

        public static bool AddMalcolmMORBCard()
        {
            return MalcolmUtil.GetTestFlag("MalcolmCardTest");
        }

        public static bool SupportUIGeneration2()
        {
            return MalcolmUtil.GetTestFlag("UIGeneration2");
        }

        public static string GetRegistrySettingsPath(string AHardwareID)
        {
            return "Settings\\" + AHardwareID;
        }

        public static string GetCurrentUserRegistryKey(string sPath, string sKey, string sDefaultValue)
        {
            string name = "SOFTWARE\\Creative Tech\\" + App.AppID;
            if (sPath != "")
                name = name + "\\" + sPath;
            string str1 = "";
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name);
                if (registryKey != null)
                {
                    string str2 = (string)registryKey.GetValue(sKey);
                    str1 = str2 == null ? sDefaultValue : str2;
                    registryKey.Close();
                }
                else
                    str1 = sDefaultValue;
            }
            catch (Exception ex)
            {
            }
            return str1;
        }

        public static void SetCurrentUserRegistryKey(string sPath, string sKey, string sValue)
        {
            string subkey = "SOFTWARE\\Creative Tech\\" + App.AppID;
            if (sPath != "")
                subkey = subkey + "\\" + sPath;
            try
            {
                RegistryKey subKey = Registry.CurrentUser.CreateSubKey(subkey);
                if (subKey == null)
                    return;
                subKey.SetValue(sKey, (object)sValue);
                subKey.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static string GetHKLMAppRegistryKey(string sKey)
        {
            return Util.GetHKLMRegistryKey("SOFTWARE\\Creative Tech\\" + App.AppID, sKey);
        }

        public static void ProcessStart(string AURL)
        {
            try
            {
                Process.Start(AURL);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, App.AppTitle);
            }
        }

        public static void ProcessStart(string AURL, string Arguments)
        {
            try
            {
                Process.Start(AURL, Arguments);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, App.AppTitle);
            }
        }

        public static bool GetPresetSaveName(double ALeft, double ATop, int AMaxLength, char[] AInvalidChars, ref string ANewName)
        {
            bool flag = false;
            try
            {
                SavePresetDialog savePresetDialog = new SavePresetDialog();
                savePresetDialog.ShowInTaskbar = false;
                savePresetDialog.Owner = Application.Current.MainWindow;
                savePresetDialog.Left = ALeft;
                savePresetDialog.Top = ATop;
                flag = savePresetDialog.ShowAsSaveDialog(AMaxLength, AInvalidChars, ref ANewName);
            }
            catch
            {
            }
            return flag;
        }

        public static void SetMalcolmControl(MalcolmControl ACtrl)
        {
            MalcolmUtil.MalCtrl = ACtrl;
        }

        public static void InitializeSlider(MalcolmControlID AID, CTSlider ASlider, Label AReadOnlyLabel)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            double ADefValue;
            double AMinValue;
            double AMaxValue;
            double AStepSize;
            uint AParamAttribute;
            if (MalcolmUtil.MalCtrl.GetParamInfoRange(AID, out ADefValue, out AMinValue, out AMaxValue, out AStepSize, out AParamAttribute))
            {
                if ((int)AParamAttribute == 1)
                {
                    ASlider.Visibility = Visibility.Collapsed;
                    if (AReadOnlyLabel == null)
                        return;
                    AReadOnlyLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    ASlider.Visibility = Visibility.Visible;
                    ASlider.MinValue = AMinValue;
                    ASlider.MaxValue = AMaxValue;
                    ASlider.DefaultValue = ADefValue;
                    ASlider.StepSize = AStepSize;
                    if (AReadOnlyLabel != null)
                        AReadOnlyLabel.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                ASlider.Visibility = Visibility.Collapsed;
                if (AReadOnlyLabel != null)
                    AReadOnlyLabel.Visibility = Visibility.Collapsed;
            }
        }

        public static void InitializeSlider(MalcolmControlID AID, CTSlider ASlider)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            double ADefValue;
            double AMinValue;
            double AMaxValue;
            double AStepSize;
            uint AParamAttribute;
            if (MalcolmUtil.MalCtrl.GetParamInfoRange(AID, out ADefValue, out AMinValue, out AMaxValue, out AStepSize, out AParamAttribute))
            {
                ASlider.Visibility = Visibility.Visible;
                ASlider.MinValue = AMinValue;
                ASlider.MaxValue = AMaxValue;
                ASlider.DefaultValue = ADefValue;
                ASlider.StepSize = AStepSize;
            }
            else
                ASlider.Visibility = Visibility.Collapsed;
        }

        public static void InitializeSlider(MalcolmControlID AID, CTVerticalSlider ASlider)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            double ADefValue;
            double AMinValue;
            double AMaxValue;
            double AStepSize;
            uint AParamAttribute;
            if (MalcolmUtil.MalCtrl.GetParamInfoRange(AID, out ADefValue, out AMinValue, out AMaxValue, out AStepSize, out AParamAttribute))
            {
                ASlider.Visibility = Visibility.Visible;
                ASlider.MinValue = AMinValue;
                ASlider.MaxValue = AMaxValue;
                ASlider.DefaultValue = ADefValue;
            }
            else
                ASlider.Visibility = Visibility.Collapsed;
        }

        public static void InitializeSlider(MalcolmControlID AID, Slider ASlider)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            double ADefValue;
            double AMinValue;
            double AMaxValue;
            double AStepSize;
            uint AParamAttribute;
            if (MalcolmUtil.MalCtrl.GetParamInfoRange(AID, out ADefValue, out AMinValue, out AMaxValue, out AStepSize, out AParamAttribute))
            {
                ASlider.Visibility = Visibility.Visible;
                ASlider.Minimum = AMinValue;
                ASlider.Maximum = AMaxValue;
                if (AMaxValue != 1.0)
                    return;
                ASlider.LargeChange = 0.1;
                ASlider.SmallChange = 0.01;
                ASlider.TickFrequency = ASlider.SmallChange;
            }
            else
                ASlider.Visibility = Visibility.Collapsed;
        }

        public static bool CheckControlAttribute(MalcolmControlID AID, ref uint AParamAttribute)
        {
            if (MalcolmUtil.MalCtrl == null)
                return false;
            return MalcolmUtil.MalCtrl.ValidateParamInfo(AID, ref AParamAttribute);
        }

        public static void ValidateDisplayOnlyControl(_etFeature AFeatureID, UIElement AControl)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            if (MalcolmUtil.MalCtrl.ValidateDisplayOnlyFeature(AFeatureID))
                AControl.Visibility = Visibility.Visible;
            else
                AControl.Visibility = Visibility.Collapsed;
        }

        public static void ValidateControl(MalcolmControlID AID, UIElement AControl)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            if (MalcolmUtil.MalCtrl.ValidateParamInfo(AID))
                AControl.Visibility = Visibility.Visible;
            else
                AControl.Visibility = Visibility.Collapsed;
        }

        public static bool ValidateFeature(MalcolmControlID AID)
        {
            if (MalcolmUtil.MalCtrl == null)
                return false;
            return MalcolmUtil.MalCtrl.ValidateParamInfo(AID);
        }

        public static bool ValidateFeature(_etFeature AFeatureID)
        {
            if (MalcolmUtil.MalCtrl == null)
                return false;
            return MalcolmUtil.MalCtrl.ValidateFeature(AFeatureID);
        }

        public static bool ValidateMiniControl(MalcolmControlID AID, UIElement AControl)
        {
            if (MalcolmUtil.MalCtrl == null)
                return false;
            if (MalcolmUtil.MalCtrl.ValidateParamInfo(AID))
            {
                AControl.Visibility = Visibility.Visible;
                return true;
            }
            AControl.Visibility = Visibility.Collapsed;
            return false;
        }

        public static void InitializeCheckbox(MalcolmControlID AID, CheckBox ACheckBox)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            if (MalcolmUtil.MalCtrl.ValidateParamInfo(AID))
                ACheckBox.Visibility = Visibility.Visible;
            else
                ACheckBox.Visibility = Visibility.Collapsed;
        }

        public static void UpdateCheckboxValue(MalcolmControlID AID, CheckBox ACheckBox)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            bool AEnable;
            if (MalcolmUtil.MalCtrl.GetParamBool(AID, out AEnable))
            {
                ACheckBox.IsEnabled = true;
                ACheckBox.IsChecked = new bool?(AEnable);
            }
            else
            {
                ACheckBox.IsEnabled = MalcolmUtil.IsInternalTest();
                ACheckBox.IsChecked = new bool?(false);
            }
        }

        public static void UpdateCheckboxValue(MalcolmControlID AID, CTGraphicCheckBox ACheckBox)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            bool AEnable;
            if (MalcolmUtil.MalCtrl.GetParamBool(AID, out AEnable))
            {
                ACheckBox.IsEnabled = true;
                ACheckBox.IsChecked = new bool?(AEnable);
            }
            else
            {
                ACheckBox.IsEnabled = MalcolmUtil.IsInternalTest();
                ACheckBox.IsChecked = new bool?(false);
            }
        }

        public static void UpdateToggleButtonValue(MalcolmControlID AID, ToggleButton AToggleButton)
        {
            if (MalcolmUtil.MalCtrl == null)
                return;
            bool AEnable;
            if (MalcolmUtil.MalCtrl.GetParamBool(AID, out AEnable))
            {
                AToggleButton.IsEnabled = true;
                AToggleButton.IsChecked = new bool?(AEnable);
            }
            else
            {
                AToggleButton.IsEnabled = MalcolmUtil.IsInternalTest();
                AToggleButton.IsChecked = new bool?(false);
            }
        }

        public static bool UpdateFeatureValue(MalcolmControlID AID, Button featureButton)
        {
            if (MalcolmUtil.MalCtrl == null)
                return false;
            bool AEnable;
            if (MalcolmUtil.MalCtrl.GetParamBool(AID, out AEnable))
            {
                featureButton.IsEnabled = true;
                return AEnable;
            }
            featureButton.IsEnabled = MalcolmUtil.IsInternalTest();
            return false;
        }

        public static bool CheckIfSoundcoreProprietryInputMonitoringSupported(string AEndpointID, ref MalcolmControlID ADirMonitorInputSourceID)
        {
            return false;
            //bool flag = false;
            //if (MalcolmUtil.MalCtrl != null && MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Direct_MonitorControl))
            //{
            //    List<_stDirectMonitorInputSourceInfo> ADirectMonitorList = new List<_stDirectMonitorInputSourceInfo>();
            //    if (MalcolmUtil.MalCtrl.GetDirectMonitorInputSourceList(MalcolmControlID.MixerEnumDirectMonitorSource, ADirectMonitorList))
            //    {
            //        for (int index = 0; index < ADirectMonitorList.Count; ++index)
            //        {
            //            if (Util.ConvertToString(ADirectMonitorList[index].deviceId) == AEndpointID)
            //            {
            //                ADirMonitorInputSourceID = MalcolmUtil.ConvertToMalcolmControlID(ADirectMonitorList[index].source);
            //                flag = true;
            //                break;
            //            }
            //        }
            //    }
            //}
            //return flag;
        }

        public static MalcolmControlID ConvertToMalcolmControlID(_etDirectMonitorInputSource ADirectMonitorInputSource)
        {
            switch (ADirectMonitorInputSource)
            {
                case _etDirectMonitorInputSource.eDirMonitorInputSource_LineIn1:
                    return MalcolmControlID.MixerDirectMonitorLineIn1Enable;
                case _etDirectMonitorInputSource.eDirMonitorInputSource_Mic1:
                    return MalcolmControlID.MixerDirectMonitorMic1Enable;
                case _etDirectMonitorInputSource.eDirMonitorInputSource_FPMic1:
                    return MalcolmControlID.MixerDirectMonitorFPMic1Enable;
                case _etDirectMonitorInputSource.eDirMonitorInputSource_FPAux1:
                    return MalcolmControlID.MixerDirectMonitorFPAux1Enable;
                case _etDirectMonitorInputSource.eDirMonitorInputSource_LineIn2:
                    return MalcolmControlID.MixerDirectMonitorLineIn2Enable;
                case _etDirectMonitorInputSource.eDirMonitorInputSource_Mic2:
                    return MalcolmControlID.MixerDirectMonitorMic2Enable;
                default:
                    return MalcolmControlID.Invalid;
            }
        }

        public static bool GetEncoderEndpointID(string AAdapterName, ref string ASpdifOutEndpointID, ref string AWhatUHearEndpointID)
        {
            return false;
            //ASpdifOutEndpointID = "";
            //AWhatUHearEndpointID = "";
            //if (!MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.MixerEnumAssociatedDeviceData))
            //    return MalLgcyLib.GetEncoderEndpointID(AAdapterName, ref ASpdifOutEndpointID, ref AWhatUHearEndpointID);
            //List<_stAssociatedDeviceInfo> AAssociatedDeviceList = new List<_stAssociatedDeviceInfo>();
            //if (!MalcolmUtil.MalCtrl.GetAssociatedDeviceList(MalcolmControlID.MixerEnumAssociatedDeviceData, AAssociatedDeviceList))
            //    return false;
            //List<string> AEndpointIDList = new List<string>();
            //for (int index = 0; index < AAssociatedDeviceList.Count; ++index)
            //    AEndpointIDList.Add(Util.ConvertToString(AAssociatedDeviceList[index].deviceId));
            //return MalLgcyLib.GetEncoderEndpointID(AEndpointIDList, ref ASpdifOutEndpointID, ref AWhatUHearEndpointID);
        }

        public static bool GetMalcolmEndpointList(string AAdapterName, List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList, MalcolmLegacy.CTAUDENDPOINTSTATE AAudEndpointState)
        {
            return false;
            //if (!MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.MixerEnumAssociatedDeviceData))
            //    return MalLgcyLib.GetEndpointList(AAdapterName, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_All, MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_MASK_All, AAudEndpointState, MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_All, MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices, AAudEndpointList);
            //List<_stAssociatedDeviceInfo> AAssociatedDeviceList = new List<_stAssociatedDeviceInfo>();
            //if (!MalcolmUtil.MalCtrl.GetAssociatedDeviceList(MalcolmControlID.MixerEnumAssociatedDeviceData, AAssociatedDeviceList))
            //    return false;
            //List<string> AEndpointIDList = new List<string>();
            //for (int index = 0; index < AAssociatedDeviceList.Count; ++index)
            //    AEndpointIDList.Add(Util.ConvertToString(AAssociatedDeviceList[index].deviceId));
            //return MalLgcyLib.GetEndpointList(AEndpointIDList, AAudEndpointState, AAudEndpointList);
        }

        public static bool CheckIfAssociatedDevice(string AudEndpointID)
        {
            return false;
            //if (MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.MixerEnumAssociatedDeviceData))
            //{
            //    List<_stAssociatedDeviceInfo> AAssociatedDeviceList = new List<_stAssociatedDeviceInfo>();
            //    if (MalcolmUtil.MalCtrl.GetAssociatedDeviceList(MalcolmControlID.MixerEnumAssociatedDeviceData, AAssociatedDeviceList))
            //    {
            //        for (int index = 0; index < AAssociatedDeviceList.Count; ++index)
            //        {
            //            if (AudEndpointID == Util.ConvertToString(AAssociatedDeviceList[index].deviceId))
            //                return true;
            //        }
            //    }
            //}
            //return false;
        }

        private static bool HasInternalTestPage(MalcolmFeaturesID AID)
        {
            switch (AID)
            {
                case MalcolmFeaturesID.THXTruStudioPro:
                    return MalcolmUtil.HasTestPage("THXTruStudioPro");
                case MalcolmFeaturesID.MicLineIn:
                    return MalcolmUtil.HasTestPage("MicLineIn");
                case MalcolmFeaturesID.ScoutMode:
                    return MalcolmUtil.HasTestPage("ScoutMode");
                case MalcolmFeaturesID.SpeakerHeadphone:
                    return MalcolmUtil.HasTestPage("SpeakerHeadphone");
                case MalcolmFeaturesID.Cinematic:
                    return MalcolmUtil.HasTestPage("Cinematic");
                case MalcolmFeaturesID.Mixer:
                    return MalcolmUtil.HasTestPage("Mixer");
                case MalcolmFeaturesID.Equalizer:
                    return MalcolmUtil.HasTestPage("Equalizer");
                case MalcolmFeaturesID.Karaoke:
                    return MalcolmUtil.HasTestPage("Karaoke");
                case MalcolmFeaturesID.JackSetup:
                    return MalcolmUtil.HasTestPage("JackSetup");
                case MalcolmFeaturesID.EAXReverb:
                    return MalcolmUtil.HasTestPage("EAXReverb");
                case MalcolmFeaturesID.AdvanceFeatures:
                    return MalcolmUtil.HasTestPage("AdvanceFeatures");
                case MalcolmFeaturesID.SBUpgradePage:
                    return MalcolmUtil.HasTestPage("SBUpgradePage");
                case MalcolmFeaturesID.SBGamingPage:
                    return MalcolmUtil.HasTestPage("SBGamingPage");
                case MalcolmFeaturesID.HWSpkProfile:
                    return MalcolmUtil.HasTestPage("");
                case MalcolmFeaturesID.HWMicProfile:
                    return MalcolmUtil.HasTestPage("");
                case MalcolmFeaturesID.SBXEqualizer:
                    return true;
                case MalcolmFeaturesID.Profile:
                    return MalcolmUtil.HasTestPage("Profile");
                default:
                    return true;
            }
        }

        public static bool HasFeaturesPage(MalcolmFeaturesID AID)
        {
            if (!MalcolmUtil.MalCtrl.GetDeviceControlPermitted())
                return false;
            if (ProductInfo.GetMalcolmProductID() == MalcolmProductID.Apollo8)
            {
                switch (AID)
                {
                    case MalcolmFeaturesID.THXTruStudioPro:
                    case MalcolmFeaturesID.SpeakerHeadphone:
                    case MalcolmFeaturesID.SBUpgradePage:
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                if (MalcolmUtil.IsCustomizedPagesTest())
                    return MalcolmUtil.HasInternalTestPage(AID);
                switch (AID)
                {
                    case MalcolmFeaturesID.THXTruStudioPro:
                        return MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_CMSS3D) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_Crystalizer) || (MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_XBass) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_DialogPlus)) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_SmartVolume);
                    case MalcolmFeaturesID.MicLineIn:
                        return (MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_AEC) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_MicArray) || (MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_MicEQ) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_MicSmartVolume)) || (MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_NoiseReduction) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_VoiceFX) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_DualMicEndFiring))) && (!MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_Mic2EQ) && !MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_Mic2SmartVolume) && !MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_Mic2VoiceFX));
                    case MalcolmFeaturesID.ScoutMode:
                        return MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.ScoutModeOnOff);
                    case MalcolmFeaturesID.SpeakerHeadphone:
                        return MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_BassManagement) || MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.SpkEnumSpkConfig);
                    case MalcolmFeaturesID.Cinematic:
                        return MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Encoder_DDLive) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Decoder_DolbyDigital) || EncoderManager.DolbyDigitalLiveSupported() || EncoderManager.DTSConnectSupported();
                    case MalcolmFeaturesID.Mixer:
                        return ProductInfo.SupportMixerPage() && MalcolmUtil.MalCtrl.GetCurrentModeID() != _etContext.eContext_Invalid;
                    case MalcolmFeaturesID.Equalizer:
                        return MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_GraphicEQ);
                    case MalcolmFeaturesID.Karaoke:
                        return MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_Mic2EQ) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_Mic2SmartVolume) || (MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_VIP_Mic2VoiceFX) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_PitchShift)) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_MicReverb);
                    case MalcolmFeaturesID.JackSetup:
                        return MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.JackDeviceConfig) && ProductInfo.GetMalcolmProductID() != MalcolmProductID.Saturn;
                    case MalcolmFeaturesID.EAXReverb:
                        return MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_EAX3) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Effects_EAXEnvironment);
                    case MalcolmFeaturesID.AdvanceFeatures:
                        return MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.AdvBitMatch) || MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.AdvSpdifOutSource) || (MalcolmUtil.MalCtrl.HasMalcolmControlID(MalcolmControlID.AdvNumHWProfileTHX) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_USBPowerOverdrive)) || (MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_BluetoothAutoConnect) || MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_LEDControl) || MalcolmUtil.MalCtrl.HasMultiplexOutputs(MalcolmControlID.SpkEnumMultiplexOutputs, etMultiplexOutputs.eMultiplexOutput_MicIn)) || EncoderManager.DefaultEncoderSupported();
                    case MalcolmFeaturesID.SBGamingPage:
                        return ProductInfo.SupportSBGamingPage();
                    case MalcolmFeaturesID.HWSpkProfile:
                        SoundCoreObject playbackSoundcoreObject = MalcolmUtil.MalCtrl.GetCurrentPlaybackSoundcoreObject();
                        return playbackSoundcoreObject != null && playbackSoundcoreObject.HasFeatureID(_etFeature.eFeature_EffectsProfile);
                    case MalcolmFeaturesID.HWMicProfile:
                        SoundCoreObject recordingSoundcoreObject = MalcolmUtil.MalCtrl.GetCurrentRecordingSoundcoreObject();
                        return recordingSoundcoreObject != null && recordingSoundcoreObject.HasFeatureID(_etFeature.eFeature_EffectsProfile);
                    case MalcolmFeaturesID.SBXEqualizer:
                        return MalcolmUtil.HasFeaturesPage(MalcolmFeaturesID.THXTruStudioPro) || MalcolmUtil.HasFeaturesPage(MalcolmFeaturesID.Equalizer);
                    default:
                        return false;
                }
            }
        }

        public static bool HasBatterySupport()
        {
            return MalcolmUtil.MalCtrl.GetDeviceControlPermitted() && MalcolmUtil.MalCtrl.HasFeatureID(_etFeature.eFeature_Battery);
        }

        public static string GetTheme(string AHardwareID)
        {
            string str = ProductInfo.GetDefaultTheme();
            if (str == MalcolmUtil.MalThemeGamma || !(AHardwareID != "") || (AHardwareID.IndexOf("VEN_1102") < 0 || AHardwareID.IndexOf("DEV_0011") < 0) || AHardwareID.IndexOf("SUBSYS_11020014") < 0 && AHardwareID.IndexOf("SUBSYS_11020015") < 0)
                return str;
            str = MalcolmUtil.MalThemeRed;
            return str;
        }

        public static int PresetComboBoxComparisonFunction(ComboBoxItem item1, ComboBoxItem item2)
        {
            if (item1.Foreground == item2.Foreground)
                return MalcolmUtil.CompareAlphanumericTo(item1.Content as string, item2.Content as string);
            return item1.Foreground == MalcolmUtil.GetStandardPresetTextColor() ? -1 : 1;
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string x, string y);

        public static int CompareAlphanumericTo(string x, string y)
        {
            return MalcolmUtil.StrCmpLogicalW(x, y);
        }

        public static bool InitializeVoiceFXPresetComboBox(ComboBox APresetComboBox, MalcolmControlID AEnumPresetID)
        {
            APresetComboBox.Text = "";
            APresetComboBox.SelectedIndex = -1;
            APresetComboBox.Items.Clear();
            List<ComboBoxItem> AComboBoxItemList = new List<ComboBoxItem>();
            List<stEnumPresetData> APresetList = new List<stEnumPresetData>();
            if (MalcolmUtil.MalCtrl.GetPresetList(AEnumPresetID, APresetList))
            {
                for (int index = 0; index < APresetList.Count; ++index)
                {
                    if (APresetList[index].presetInfo.presetType == etPresetType.ePresetType_Standard)
                        MalcolmUtil.AddStandardVoiceFXComboBoxItem(AComboBoxItemList, APresetList[index].presetInfo.presetId);
                    else
                        MalcolmUtil.AddCustomComboBoxItem(AComboBoxItemList, APresetList[index].presetInfo.presetId, Util.ConvertToUnicodeString(APresetList[index].presetInfo.presetName));
                }
            }
            else if (MalcolmUtil.IsInternalTest())
                MalcolmUtil.GetVoiceFXPresetDummy(AComboBoxItemList);
            if (AComboBoxItemList.Count > 0)
            {
                AComboBoxItemList.Sort(new Comparison<ComboBoxItem>(MalcolmUtil.PresetComboBoxComparisonFunction));
                for (int index = 0; index < AComboBoxItemList.Count; ++index)
                    APresetComboBox.Items.Add((object)AComboBoxItemList[index]);
            }
            return AComboBoxItemList.Count > 0;
        }

        private static void AddStandardVoiceFXComboBoxItem(List<ComboBoxItem> AComboBoxItemList, int APresetID)
        {
            string factoryPresetName = MalcolmUtil.GetVoiceFXFactoryPresetName(APresetID);
            if (!(factoryPresetName != ""))
                return;
            ComboBoxItem comboBoxItem = new ComboBoxItem();
            comboBoxItem.Foreground = (Brush)MalcolmUtil.GetStandardPresetTextColor();
            comboBoxItem.Content = (object)factoryPresetName;
            comboBoxItem.Tag = (object)APresetID;
            AComboBoxItemList.Add(comboBoxItem);
        }

        private static void AddCustomComboBoxItem(List<ComboBoxItem> AComboBoxItemList, int APresentID, string AContent)
        {
            ComboBoxItem comboBoxItem = new ComboBoxItem();
            comboBoxItem.Foreground = (Brush)MalcolmUtil.GetCustomPresetTextColor();
            comboBoxItem.Content = (object)AContent;
            comboBoxItem.Tag = (object)APresentID;
            AComboBoxItemList.Add(comboBoxItem);
        }

        public static void GetCurrentVoiceFXPreset(ComboBox APresetComboBox, MalcolmControlID AActivePresetID)
        {
            uint AIndex;
            if (!MalcolmUtil.MalCtrl.GetActivePreset(AActivePresetID, out AIndex))
                return;
            if ((int)AIndex == 0)
            {
                APresetComboBox.Text = MalcolmUtil.GetCustomPresentName();
            }
            else
            {
                for (int index = 0; index < APresetComboBox.Items.Count; ++index)
                {
                    if ((long)(int)(APresetComboBox.Items[index] as ComboBoxItem).Tag == (long)AIndex)
                    {
                        APresetComboBox.SelectedIndex = index;
                        break;
                    }
                }
            }
        }

        private static void GetVoiceFXPresetDummy(List<ComboBoxItem> AComboBoxItemList)
        {
            for (int APresetID = 1; APresetID <= 117; ++APresetID)
                MalcolmUtil.AddStandardVoiceFXComboBoxItem(AComboBoxItemList, APresetID);
            MalcolmUtil.AddCustomComboBoxItem(AComboBoxItemList, 101, "Custom 1");
        }

        public static bool InitializeMicEQPresetComboBox(ComboBox APresetComboBox, MalcolmControlID AEnumPresetID)
        {
            APresetComboBox.Text = "";
            APresetComboBox.SelectedIndex = -1;
            APresetComboBox.Items.Clear();
            List<ComboBoxItem> AComboBoxItemList = new List<ComboBoxItem>();
            List<stEnumPresetData> APresetList = new List<stEnumPresetData>();
            if (MalcolmUtil.MalCtrl.GetPresetList(AEnumPresetID, APresetList))
            {
                for (int index = 0; index < APresetList.Count; ++index)
                {
                    if (APresetList[index].presetInfo.presetType == etPresetType.ePresetType_Standard)
                        MalcolmUtil.AddStandardMicEQComboBoxItem(AComboBoxItemList, APresetList[index].presetInfo.presetId);
                    else
                        MalcolmUtil.AddCustomComboBoxItem(AComboBoxItemList, APresetList[index].presetInfo.presetId, Util.ConvertToUnicodeString(APresetList[index].presetInfo.presetName));
                }
            }
            else if (MalcolmUtil.IsInternalTest())
                MalcolmUtil.GetMicEQPresetDummy(AComboBoxItemList);
            if (AComboBoxItemList.Count > 0)
            {
                AComboBoxItemList.Sort(new Comparison<ComboBoxItem>(MalcolmUtil.PresetComboBoxComparisonFunction));
                for (int index = 0; index < AComboBoxItemList.Count; ++index)
                    APresetComboBox.Items.Add((object)AComboBoxItemList[index]);
            }
            return AComboBoxItemList.Count > 0;
        }

        public static void GetCurrentMicEQPreset(ComboBox APresetComboBox, MalcolmControlID AActivePresetID)
        {
            uint AIndex;
            if (!MalcolmUtil.MalCtrl.GetActivePreset(AActivePresetID, out AIndex))
                return;
            if ((int)AIndex == 0)
            {
                APresetComboBox.Text = MalcolmUtil.GetCustomPresentName();
            }
            else
            {
                for (int index = 0; index < APresetComboBox.Items.Count; ++index)
                {
                    if ((long)(int)(APresetComboBox.Items[index] as ComboBoxItem).Tag == (long)AIndex)
                    {
                        APresetComboBox.SelectedIndex = index;
                        break;
                    }
                }
            }
        }

        private static void AddStandardMicEQComboBoxItem(List<ComboBoxItem> AComboBoxItemList, int APresetID)
        {
            string factoryPresetName = MalcolmUtil.GetMicEQFactoryPresetName(APresetID);
            if (!(factoryPresetName != ""))
                return;
            ComboBoxItem comboBoxItem = new ComboBoxItem();
            comboBoxItem.Foreground = (Brush)MalcolmUtil.GetStandardPresetTextColor();
            comboBoxItem.Content = (object)factoryPresetName;
            comboBoxItem.Tag = (object)APresetID;
            AComboBoxItemList.Add(comboBoxItem);
        }

        private static void GetMicEQPresetDummy(List<ComboBoxItem> AComboBoxItemList)
        {
            for (int APresetID = 1; APresetID <= 12; ++APresetID)
                MalcolmUtil.AddStandardMicEQComboBoxItem(AComboBoxItemList, APresetID);
            MalcolmUtil.AddCustomComboBoxItem(AComboBoxItemList, 101, "Custom 1");
        }

        public static bool InitializeMicReverbPresetComboBox(ComboBox APresetComboBox, MalcolmControlID AEnumPresetID)
        {
            APresetComboBox.Text = "";
            APresetComboBox.SelectedIndex = -1;
            APresetComboBox.Items.Clear();
            List<ComboBoxItem> AComboBoxItemList = new List<ComboBoxItem>();
            List<stEnumPresetData> APresetList = new List<stEnumPresetData>();
            if (MalcolmUtil.MalCtrl.GetPresetList(AEnumPresetID, APresetList))
            {
                for (int index = 0; index < APresetList.Count; ++index)
                {
                    if (APresetList[index].presetInfo.presetType == etPresetType.ePresetType_Standard)
                        MalcolmUtil.AddStandardMicReverbComboBoxItem(AComboBoxItemList, APresetList[index].presetInfo.presetId);
                    else
                        MalcolmUtil.AddCustomComboBoxItem(AComboBoxItemList, APresetList[index].presetInfo.presetId, Util.ConvertToUnicodeString(APresetList[index].presetInfo.presetName));
                }
            }
            else if (MalcolmUtil.IsInternalTest())
                MalcolmUtil.GetMicReverbPresetDummy(AComboBoxItemList);
            if (AComboBoxItemList.Count > 0)
            {
                AComboBoxItemList.Sort(new Comparison<ComboBoxItem>(MalcolmUtil.PresetComboBoxComparisonFunction));
                for (int index = 0; index < AComboBoxItemList.Count; ++index)
                    APresetComboBox.Items.Add((object)AComboBoxItemList[index]);
            }
            return AComboBoxItemList.Count > 0;
        }

        public static void GetCurrentMicReverbPreset(ComboBox APresetComboBox, MalcolmControlID AActivePresetID)
        {
            uint AIndex;
            if (!MalcolmUtil.MalCtrl.GetActivePreset(AActivePresetID, out AIndex))
                return;
            if ((int)AIndex == 0)
            {
                APresetComboBox.Text = MalcolmUtil.GetCustomPresentName();
            }
            else
            {
                for (int index = 0; index < APresetComboBox.Items.Count; ++index)
                {
                    if ((long)(int)(APresetComboBox.Items[index] as ComboBoxItem).Tag == (long)AIndex)
                    {
                        APresetComboBox.SelectedIndex = index;
                        break;
                    }
                }
            }
        }

        private static void AddStandardMicReverbComboBoxItem(List<ComboBoxItem> AComboBoxItemList, int APresetID)
        {
            string factoryPresetName = MalcolmUtil.GetMicReverbFactoryPresetName(APresetID);
            if (!(factoryPresetName != ""))
                return;
            ComboBoxItem comboBoxItem = new ComboBoxItem();
            comboBoxItem.Foreground = (Brush)MalcolmUtil.GetStandardPresetTextColor();
            comboBoxItem.Content = (object)factoryPresetName;
            comboBoxItem.Tag = (object)APresetID;
            AComboBoxItemList.Add(comboBoxItem);
        }

        private static void GetMicReverbPresetDummy(List<ComboBoxItem> AComboBoxItemList)
        {
            for (int APresetID = 1; APresetID <= 19; ++APresetID)
                MalcolmUtil.AddStandardMicReverbComboBoxItem(AComboBoxItemList, APresetID);
            MalcolmUtil.AddCustomComboBoxItem(AComboBoxItemList, 101, "Custom 1");
        }

        public static void AddComboBoxItem(ComboBox AComboBox, string AContent, object ATag)
        {
            ComboBoxItem comboBoxItem = new ComboBoxItem();
            comboBoxItem.Content = (object)AContent;
            comboBoxItem.Tag = ATag;
            AComboBox.Items.Add((object)comboBoxItem);
        }

        public static MalcolmUtil.SpeakerConfig GetSpeakerConfig(uint ASpkConfig)
        {
            if ((int)ASpkConfig == 17036863)
                return MalcolmUtil.SpeakerConfig.SpkConfig71EX;
            if (((int)ASpkConfig & 1073741824) == 1073741824)
                return MalcolmUtil.SpeakerConfig.SpkConfigBuildInSpk;
            if (((int)ASpkConfig & int.MinValue) == int.MinValue)
                return MalcolmUtil.SpeakerConfig.SpkConfigHp;
            uint num = ASpkConfig & 4095U;
            if (num <= 63U)
            {
                if ((int)num == 3)
                    return MalcolmUtil.SpeakerConfig.SpkConfigStereo;
                if ((int)num != 51)
                {
                    if ((int)num == 63)
                        goto label_17;
                    else
                        goto label_19;
                }
            }
            else
            {
                if (num <= 263U)
                {
                    if ((int)num != (int)byte.MaxValue)
                    {
                        if ((int)num == 263)
                            goto label_16;
                        else
                            goto label_19;
                    }
                }
                else if ((int)num != 1551)
                {
                    if ((int)num != 1599)
                        goto label_19;
                }
                else
                    goto label_17;
                return MalcolmUtil.SpeakerConfig.SpkConfig71;
            }
            label_16:
            return MalcolmUtil.SpeakerConfig.SpkConfigQuad;
            label_17:
            return MalcolmUtil.SpeakerConfig.SpkConfig51;
            label_19:
            return MalcolmUtil.SpeakerConfig.Unknown;
        }

        public static string GetSpeakerConfigStr(uint ASpkConfig)
        {
            if (((int)ASpkConfig & 1073741824) == 1073741824)
                return MalcolmUtil.GetResourceString("resStrBuildInSpeakers");
            if (((int)ASpkConfig & int.MinValue) == int.MinValue)
            {
                if (ProductInfo.IsHeadsetProduct() && (int)ASpkConfig == -2080360897)
                    return MalcolmUtil.GetResourceString("resStr71VirtualSurround");
                return MalcolmUtil.GetResourceString("resStrHeadphones");
            }
            if (((int)ASpkConfig & 33554432) == 33554432)
                return MalcolmUtil.GetResourceString("resStrSpeakers");
            if ((int)ASpkConfig == 17036863)
                return MalcolmUtil.GetResourceString("resStr71EXSurround");
            if ((int)ASpkConfig == 134230019)
                return MalcolmUtil.GetResourceString("resStrStereoDirect192kHz");
            uint num = ASpkConfig & 4095U;
            if (num <= 63U)
            {
                if ((int)num == 3)
                    return MalcolmUtil.GetResourceString("resStrStereo");
                if ((int)num == 51)
                    return MalcolmUtil.GetResourceString("resStrQuadraphonic");
                if ((int)num != 63)
                    goto label_26;
            }
            else
            {
                if (num <= 263U)
                {
                    if ((int)num != (int)byte.MaxValue)
                    {
                        if ((int)num == 263)
                            return MalcolmUtil.GetResourceString("resStrSpkConfigSurround");
                        goto label_26;
                    }
                }
                else if ((int)num != 1551)
                {
                    if ((int)num != 1599)
                        goto label_26;
                }
                else
                    goto label_24;
                return MalcolmUtil.GetResourceString("resStr71Surround");
            }
            label_24:
            return MalcolmUtil.GetResourceString("resStr51Surround");
            label_26:
            return "";
        }

        public static string GetDeviceConfigStr(uint ADeviceConfig)
        {
            uint num = ADeviceConfig;
            if (num <= 4161536U)
            {
                if (num <= 229380U)
                {
                    switch (num)
                    {
                        case 196609:
                            return MalcolmUtil.GetResourceString("resStrStereoAndLineIn");
                        case 196610:
                            return MalcolmUtil.GetResourceString("resStrStereoAndMic");
                        case 196611:
                            return MalcolmUtil.GetResourceString("resStrStereoAndLineIn");
                        case 196612:
                            return MalcolmUtil.GetResourceString("resStrStereoAndMic");
                        case 229379:
                            return MalcolmUtil.GetResourceString("resStrStereoAndLineIn");
                        case 229380:
                            return MalcolmUtil.GetResourceString("resStrStereoAndMicIn");
                    }
                }
                else
                {
                    switch (num)
                    {
                        case 3342336:
                            return MalcolmUtil.GetResourceString("resStrQuadraphonic");
                        case 4128768:
                            return MalcolmUtil.GetResourceString("resStr51Surround");
                        case 4128769:
                            return MalcolmUtil.GetResourceString("resStr51SurroundLineIn");
                        case 4128770:
                            return MalcolmUtil.GetResourceString("resStr51SurroundMic");
                        case 4161536:
                            return MalcolmUtil.GetResourceString("resStr51Surround");
                    }
                }
            }
            else if (num <= 2147483652U)
            {
                switch (num)
                {
                    case 104796160:
                        return MalcolmUtil.GetResourceString("resStr71EXSurround");
                    case 2147483649:
                        return MalcolmUtil.GetResourceString("resStrHpAndLineIn");
                    case 2147483650:
                        return MalcolmUtil.GetResourceString("resStrHpAndMic");
                    case 2147483651:
                        return MalcolmUtil.GetResourceString("resStrHpAndLineIn");
                    case 2147483652:
                        return MalcolmUtil.GetResourceString("resStrHpAndMic");
                }
            }
            else
            {
                switch (num)
                {
                    case 2147516416:
                        return MalcolmUtil.GetResourceString("resStrHpAndHp");
                    case 2147516419:
                        return MalcolmUtil.GetResourceString("resStrHpAndLineIn");
                    case 2147516420:
                        return MalcolmUtil.GetResourceString("resStrHpAndMicIn");
                    case 2147524611:
                        return MalcolmUtil.GetResourceString("resStriPhoneAndLineIn");
                    case 2147524612:
                        return MalcolmUtil.GetResourceString("resStriPhoneAndMicIn");
                    case 2147532800:
                        return MalcolmUtil.GetResourceString("resStrPhoneAndHp");
                    case 2147532803:
                        return MalcolmUtil.GetResourceString("resStrPhoneAndLineIn");
                    case 2147532804:
                        return MalcolmUtil.GetResourceString("resStrPhoneAndMicIn");
                }
            }
            return "";
        }

        public static bool GetShowJackConfigDlgFromReg()
        {
            string str = MalcolmUtil.GetCurrentUserRegistryKey("", "DoNotShowJackConfigDlg", "");
            if (str != "1" && str != "0")
                str = MalcolmUtil.GetHKLMAppRegistryKey("DoNotShowJackConfigDlg");
            return str != "1";
        }

        public static void SetShowJackConfigDlg(bool bEnable)
        {
            if (bEnable)
                MalcolmUtil.SetCurrentUserRegistryKey("", "DoNotShowJackConfigDlg", "0");
            else
                MalcolmUtil.SetCurrentUserRegistryKey("", "DoNotShowJackConfigDlg", "1");
        }

        public static void RunJackSetupDialog()
        {
            string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MalcolmUtil.JackSetupFilename);
            if (!File.Exists(str))
                return;
            MalcolmUtil.ProcessStart(str, "/r /appid=" + App.AppID + " /pdtid=" + (object)ProductInfo.GetSupportedSoftwareFlag());
        }

        public static void StopJackSetupDialog()
        {
            string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MalcolmUtil.JackSetupFilename);
            if (!File.Exists(str))
                return;
            MalcolmUtil.ProcessStart(str, "/u /appid=" + App.AppID);
        }

        public static bool CheckHasMalcolmControlFeature(string AEndpointID, MalcolmControlID AID)
        {
            bool flag = false;
            string malcolmClsid = MalLgcyLib.GetMalcolmCLSID(AEndpointID);
            if (malcolmClsid != "")
            {
                SoundCoreObject soundCoreObject = new SoundCoreObject(malcolmClsid, AEndpointID);
                if (soundCoreObject.IsInitialized)
                {
                    flag = soundCoreObject.ValidateParamInfo(AID);
                    soundCoreObject.Uninitialize();
                }
            }
            return flag;
        }

        public enum SpeakerConfig
        {
            SpkConfigBuildInSpk,
            SpkConfigHp,
            SpkConfigStereo,
            SpkConfigQuad,
            SpkConfig51,
            SpkConfig71,
            SpkConfig71EX,
            Unknown,
        }
    }
}
