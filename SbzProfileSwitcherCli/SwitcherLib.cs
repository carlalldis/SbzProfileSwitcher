using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CTSoundCore;
using Malcolm;
using Newtonsoft.Json;
using System.Reflection;

namespace SbzProfileSwitcherCli
{
    public class SwitcherLib
    {
        private MalcolmControl malCtrl;
        private Config config;
        public string ConfigPath { get; set; }

        public SwitcherLib(string configFile, bool useDefault)
        {
            string exeDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            ConfigPath = Path.Combine(exeDir, configFile);
            LoadConfig();
            malCtrl = getAndInitMalcolmControl();
            if (config.DefaultProfile != null && useDefault)
                {
                SetProfile(int.Parse(config.DefaultProfile.ToString()));
                }
        }

        public void LoadConfig()
        {
            string configText = File.ReadAllText(ConfigPath);
            config = JsonConvert.DeserializeObject<Config>(configText);
        }

        public static string[] GetAudioDeviceNames()
        {
            AudioDevice[] audioDevices = AudioDevice.GetDevices();
            string[] deviceList = audioDevices.Select(i => i.DeviceName).ToArray();
            return deviceList;
        }

        static void SetDefaultAudioDevice(string deviceName)
        {
            AudioDevice[] deviceList = AudioDevice.GetDevices();
            AudioDevice selectedDevice = deviceList.First(i => i.DeviceName == deviceName);
            selectedDevice.SetDefault();
        }

        public string[] GetProfileNames()
        {
            return config.Profiles.Select(i => i.ProfileName).ToArray();
        }

        public void SetProfile(int profileIdx)
        {
            Profile profile = config.Profiles[profileIdx];

            Func<MalcolmControlID, Object, Tuple<MalcolmControlID, Object>> tc = Tuple.Create;

            var optionsBool = new List<Tuple<MalcolmControlID,Object>>
            {
                tc(MalcolmControlID.THXSurroundEnable,      profile.Options.SurroundEnable),
                tc(MalcolmControlID.THXCrystalEnable,       profile.Options.CrystalEnable),
                tc(MalcolmControlID.THXBassEnable,          profile.Options.BassEnable),
                tc(MalcolmControlID.THXSmrtVolEnable,       profile.Options.SmartVolumeEnable),
                tc(MalcolmControlID.THXDlgPlusEnable,       profile.Options.DialogPlusEnable),
                tc(MalcolmControlID.SpkSwfMgtEnable,        profile.Options.BassRedirEnable),
                tc(MalcolmControlID.SpkSwfBoostEnable,      profile.Options.BassRedirBoostEnable),
                tc(MalcolmControlID.EqualizerEnable,        profile.Options.EqualizerEnable)
            };

            var optionsFloat = new List<Tuple<MalcolmControlID, Object>>
            {
                tc(MalcolmControlID.THXSurroundValue,       profile.Options.SurroundAmount),
                tc(MalcolmControlID.THXCrystalValue,        profile.Options.CrystalAmount),
                tc(MalcolmControlID.THXBassFreqValue,       profile.Options.BassFreq),
                tc(MalcolmControlID.THXBassValue,           profile.Options.BassAmount),
                tc(MalcolmControlID.THXSmrtVolValue,        profile.Options.SmartVolumeAmount),
                tc(MalcolmControlID.THXDlgPlusValue,        profile.Options.DialogPlusAmount),
                tc(MalcolmControlID.SpkSwfMgtFreqValue,     profile.Options.BassRedirFreq)
            };

            var optionsInt = new List<Tuple<MalcolmControlID, Object>>
            {
                tc(MalcolmControlID.THXSmrtVolMode,         profile.Options.SmartVolumeMode),
                tc(MalcolmControlID.EqualizerActivePreset,  profile.Options.EqualizerProfile)
            };

            foreach (var option in optionsBool)
            {
                if (option.Item2 != null)
                {
                    var optionVal = bool.Parse(option.Item2.ToString());
                    malCtrl.SetParamBool(option.Item1, optionVal);
                }
            }

            foreach (var option in optionsFloat)
            {
                if (option.Item2 != null)
                {
                    var optionVal = float.Parse(option.Item2.ToString());
                    malCtrl.SetParamFloat(option.Item1, optionVal);
                }
            }

            foreach (var option in optionsFloat)
            {
                if (option.Item2 != null)
                {
                    var optionVal = uint.Parse(option.Item2.ToString());
                    malCtrl.SetParamInt(option.Item1, optionVal);
                }
            }

            // Set Audio Device
            SetDefaultAudioDevice(profile.OutputDevice);

            // Set Volume
            if (profile.Volume != null)
            {
                float volume = float.Parse(profile.Volume.ToString());
                SetAudioLevel.SetVolume(volume);
            }

            // Set Jack Mode (Speakers or Headphones) // No longer required with Set Speaker Mode
            //if      (profile.JackMode == "Speakers")  { setNewOutType(malCtrl, MultiplexOutTypes.Speaker); }
            //else if (profile.JackMode == "Headphones") { setNewOutType(malCtrl, MultiplexOutTypes.Headphones); }
            //else { throw new Exception("A value other than Speakers or Headphones for JackMode was specified"); }

            // Set Speaker Mode
            // Speaker Mode: 0 = 5.1, 1 = Stereo, 2 = Headphones, 3 = Stereo Direct (Causes startup failure for switcher if left on this mode)
            if (profile.SpeakerMode != null)
            {
                List<_stEnumSpeakerConfigData> ASpkConfigList = new List<_stEnumSpeakerConfigData>();
                malCtrl.GetSpkConfigList(MalcolmControlID.SpkEnumSpkConfig, ASpkConfigList);
                uint selectedSpeakerMask;
                switch (profile.SpeakerMode)
                {
                    case "5.1":
                        selectedSpeakerMask = ASpkConfigList[0].speakerMask;
                        break;
                    case "Stereo":
                        selectedSpeakerMask = ASpkConfigList[1].speakerMask;
                        break;
                    case "Headphones":
                        selectedSpeakerMask = ASpkConfigList[2].speakerMask;
                        break;
                    default:
                        throw new Exception("A value other than 5.1, Stereo, or Headphones for SpeakerMode was specified");
                }
                malCtrl.SetParamInt(MalcolmControlID.SpkConfig, selectedSpeakerMask);
            }

            /*
            // Virtual Surround

            if (profile.Options.SurroundEnable != null)
            {
                bool surroundEnable = bool.Parse(profile.Options.SurroundEnable.ToString());
                malCtrl.SetParamBool(MalcolmControlID.THXSurroundEnable, surroundEnable);
            }

            if (profile.Options.SurroundAmount != null)
            {
                float surroundAmount = float.Parse(profile.Options.SurroundAmount.ToString()) / 100;
                malCtrl.SetParamFloat(MalcolmControlID.THXSurroundValue, surroundAmount);
            }

            // Crystalizer

            if (profile.Options.CrystalEnable != null)
            {
                bool crystalEnable = bool.Parse(profile.Options.CrystalEnable.ToString());
                malCtrl.SetParamBool(MalcolmControlID.THXCrystalEnable, crystalEnable);
            }

            if (profile.Options.CrystalAmount != null)
            {
                float crystalAmount = float.Parse(profile.Options.CrystalAmount.ToString()) / 100;
                malCtrl.SetParamFloat(MalcolmControlID.THXCrystalValue, crystalAmount);
            }


            // Surround, bool / float
            malCtrl.SetParamBool(MalcolmControlID.THXSurroundEnable, true);
            malCtrl.SetParamFloat(MalcolmControlID.THXSurroundValue, 0.3f);

            // Crystalizer
            malCtrl.SetParamBool(MalcolmControlID.THXCrystalEnable, true);
            malCtrl.SetParamFloat(MalcolmControlID.THXCrystalValue, 0.9f);

            // Bass
            malCtrl.SetParamBool(MalcolmControlID.THXBassEnable, true);
            malCtrl.SetParamFloat(MalcolmControlID.THXBassFreqValue, 300);
            malCtrl.SetParamFloat(MalcolmControlID.THXBassValue, 0.75f);

            // Smart Volume
            malCtrl.SetParamBool(MalcolmControlID.THXSmrtVolEnable, true);
            malCtrl.SetParamInt(MalcolmControlID.THXSmrtVolMode, 2);
            malCtrl.SetParamFloat(MalcolmControlID.THXSmrtVolValue, 0.75f);

            // Dialog Plus
            malCtrl.SetParamBool(MalcolmControlID.THXDlgPlusEnable, true);
            malCtrl.SetParamFloat(MalcolmControlID.THXDlgPlusValue, 0.75f);



            // Speaker Bass Redirection
            malCtrl.SetParamBool(MalcolmControlID.SpkSwfMgtEnable, true);
            malCtrl.SetParamFloat(MalcolmControlID.SpkSwfMgtFreqValue, 120);
            malCtrl.SetParamBool(MalcolmControlID.SpkSwfBoostEnable, true);

            // Equalizer
            malCtrl.SetParamBool(MalcolmControlID.EqualizerEnable, true);
            malCtrl.SetParamInt(MalcolmControlID.EqualizerActivePreset, 11);

            // Default Audio Device
            SetDefaultAudioDevice("Speakers (Sound Blaster Z)");
            */
        }

        private static MalcolmControl getAndInitMalcolmControl()
        {
            MalcolmControl malCtrl = new MalcolmControl();

            string appId = "Sound Blaster Z-Series Control Panel";
            string playbackId = null;
            string recordId = null;
            string adapterName = null;
            MalLgcyLib.GetLastEndpointID(appId, ref playbackId, ref recordId, ref adapterName);

            string malcolmClsId = MalLgcyLib.GetMalcolmCLSID(playbackId);

            MalCtrlInitResult bindResult = MalCtrlInitResult.Unknown;

            // Binding does usually not succeed on the first try, so try until it does or I tell it to quit.
            int tryCounter = 0;
            const int maxTries = 5;
            while (bindResult != MalCtrlInitResult.Success && tryCounter <= maxTries)
            {
                bindResult = malCtrl.BindPlaybackEndpoint(malcolmClsId, playbackId);
                tryCounter++;
            }

            if (bindResult != MalCtrlInitResult.Success)
            {
                throw new Exception("Could not bind to the playback endpoint. ({bindResult})");
            }
            else
            {
                bool selectResult = malCtrl.SelectPlaybackEndpoint(playbackId);
            }

            return malCtrl;
        }

        /*
        private static MultiplexOutTypes getNewOutType(MalcolmControl malCtrl)
        {
            MultiplexOutTypes currentOutType = getCurrentMultiplexType(malCtrl);

            MultiplexOutTypes newOutType;
            if (currentOutType == MultiplexOutTypes.Headphones)
            {
                newOutType = MultiplexOutTypes.Speaker;
            }
            else if (currentOutType == MultiplexOutTypes.Speaker)
            {
                newOutType = MultiplexOutTypes.Headphones;
            }
            else
            {
                // Don't care about anything else.
                newOutType = currentOutType;
            }

            return newOutType;
        }

        private static MultiplexOutTypes getCurrentMultiplexType(MalcolmControl malCtrl)
        {
            uint outputUint;
            malCtrl.GetParamInt(MalcolmControlID.SpkMultiplexOutput, out outputUint);
            return (MultiplexOutTypes)outputUint;
        }

        private static void setNewOutType(MalcolmControl malCtrl, MultiplexOutTypes newOutType)
        {
            bool result = malCtrl.SetParamInt(MalcolmControlID.SpkMultiplexOutput, (uint)newOutType);
        }

        private enum MultiplexOutTypes : uint
        {
            Headphones = 0U,
            Speaker = 1U
        }
        */
    }
}
