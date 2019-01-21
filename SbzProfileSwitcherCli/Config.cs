using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SbzProfileSwitcherCli
{
    public class Config
    {
        public object DefaultProfile { get; set; }
        public Profile[] Profiles { get; set; }
    }

    public class Profile
    {
        public string ProfileName { get; set; }
        public string OutputDevice { get; set; }
        public string SpeakerMode { get; set; }
        public object Volume { get; set; }
        public ProfileOptions Options { get; set; }
    }

    public class ProfileOptions
    {
        public object SurroundEnable { get; set; }
        public object SurroundAmount { get; set; }
        public object CrystalEnable { get; set; }
        public object CrystalAmount { get; set; }
        public object BassEnable { get; set; }
        public object BassFreq { get; set; }
        public object BassAmount { get; set; }
        public object SmartVolumeEnable { get; set; }
        public object SmartVolumeMode { get; set; }
        public object SmartVolumeAmount { get; set; }
        public object DialogPlusEnable { get; set; }
        public object DialogPlusAmount { get; set; }
        public object BassRedirEnable { get; set; }
        public object BassRedirFreq { get; set; }
        public object BassRedirBoostEnable { get; set; }
        public object EqualizerEnable { get; set; }
        public object EqualizerProfile { get; set; }
    }
}
