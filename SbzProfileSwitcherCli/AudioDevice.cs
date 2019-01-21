// ----------------------------------------------------------------------------
// AudioDevice.cs
// Creates an AudioDevice class in C#.
// Part of QuickSoundSwitch.
// Aaron Brooks, 2012.
// ----------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices; //allows our interop code

namespace SbzProfileSwitcherCli
{
    class AudioDevice
    {
        private readonly UnmanagedType deviceUnmanagedID;

        public int DeviceNum { get; }
        public string DeviceName { get; }
        public string DeviceID { get; }

        public AudioDevice(int deviceNum)
        {
            this.DeviceNum = deviceNum;
            this.DeviceID = GetDeviceID(this.DeviceNum);
            this.deviceUnmanagedID = GetUnmanagedDeviceID(this.DeviceNum);
            this.DeviceName = GetDeviceFriendlyName(this.DeviceNum);
        }

        public void SetDefault()
        {
            SetDefaultAudioPlaybackDevice(deviceUnmanagedID);
        }

        [DllImport("AudioEndPointController.dll", EntryPoint = "?SetDefaultAudioPlaybackDevice@@YAJPB_W@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern Int32 SetDefaultAudioPlaybackDevice(UnmanagedType deviceID);

        [DllImport("AudioEndPointController.dll", EntryPoint = "?GetDeviceFriendlyName@@YAPA_WH@Z", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        static extern string GetDeviceFriendlyName(int deviceNum);

        [DllImport("AudioEndPointController.dll", EntryPoint = "?GetDeviceID@@YAPB_WH@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern UnmanagedType GetUnmanagedDeviceID(int deviceNum);

        [DllImport("AudioEndPointController.dll", EntryPoint = "?GetDeviceIDasLPWSTR@@YAPA_WH@Z", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        static extern string GetDeviceID(int deviceNum);

        [DllImport("AudioEndPointController.dll", EntryPoint = "?GetDeviceCount@@YAHXZ", CallingConvention = CallingConvention.Cdecl)] //Get the entrypoint using dumpbin.exe /exports.
        static extern int GetDeviceCount();

        public static AudioDevice[] GetDevices()
        {
            int deviceCount = GetDeviceCount();
            AudioDevice[] deviceList = new AudioDevice[deviceCount];

            for (int i = 0; i < deviceCount; i++)
            {
                deviceList[i] = new AudioDevice(i);
            }

            return deviceList;
        }
    }
}
