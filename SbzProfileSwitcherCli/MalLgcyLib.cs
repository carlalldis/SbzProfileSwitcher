// Decompiled with JetBrains decompiler
// Type: Malcolm.MalLgcyLib
// Assembly: SBZ, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null
// MVID: F576CC9B-1B1F-4112-BD5C-14BC70FE47C8
// Assembly location: C:\Program Files (x86)\Creative\Sound Blaster Z-Series\Sound Blaster Z-Series Control Panel\SBZ.exe

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;

namespace Malcolm
{
    internal class MalLgcyLib
    {
        public const int ERROR_FAIL = 0;
        public const int IDOK = 1;
        public const int IDCANCEL = 2;

        public static string GetMalcolmCLSID(string AEndpointID)
        {
            MalcolmLegacy.PROPERTYKEY key = new MalcolmLegacy.PROPERTYKEY();
            key.fmtid = new Guid("C949C6AA-132B-4511-BB1B-35261A2A6333");
            key.pid = 0U;
            string lppwszValue = "";
            if (MalcolmLegacy.CSCTGetAudioEndpointPropertyFromPropertyStore(AEndpointID, true, ref key, ref lppwszValue) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                return lppwszValue;
            return "";
        }

        public static string GetHardwareID(string AEndpointID)
        {
            MalcolmLegacy.PROPERTYKEY key = new MalcolmLegacy.PROPERTYKEY();
            key.fmtid = new Guid("B3F8FA53-0004-438E-9003-51A46E139BFC");
            key.pid = 2U;
            string lppwszValue = "";
            string str1 = "unknown";
            if (MalcolmLegacy.CSCTGetAudioEndpointPropertyFromPropertyStore(AEndpointID, true, ref key, ref lppwszValue) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                string str2 = lppwszValue.ToUpper();
                string str3 = "USB\\";
                string str4 = "HDAUDIO\\";
                string str5 = "PCI";
                if (str2.Contains(str3))
                {
                    int num = str2.IndexOf(str3);
                    string str6 = str2.Substring(num + str3.Length);
                    int startIndex = str6.IndexOf("\\");
                    if (startIndex > 0)
                        str6 = str6.Remove(startIndex);
                    string[] strArray = str6.Split('&');
                    string str7 = "VID_";
                    string str8 = "PID_";
                    str1 = "USB";
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        if (strArray[index].StartsWith(str7) || strArray[index].StartsWith(str8))
                            str1 = str1 + "_" + strArray[index];
                    }
                }
                else if (str2.Contains(str4) || str2.Contains(str5))
                {
                    string str6 = "";
                    if (str2.Contains(str4))
                        str6 = str4;
                    else if (str2.Contains(str5))
                        str6 = str5;
                    int num = str2.IndexOf(str6);
                    if (num > 0)
                        str2 = str2.Substring(num + str6.Length);
                    int startIndex = str2.IndexOf("\\");
                    if (startIndex > 0)
                        str2 = str2.Remove(startIndex);
                    string[] strArray = str2.Split('&');
                    string str7 = "VEN_";
                    string str8 = "DEV_";
                    string str9 = "SUBSYS_";
                    str1 = str6.Replace("\\", "");
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        if (strArray[index].StartsWith(str7))
                        {
                            str1 = str1 + "_" + strArray[index];
                            break;
                        }
                    }
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        if (strArray[index].StartsWith(str8))
                        {
                            str1 = str1 + "_" + strArray[index];
                            break;
                        }
                    }
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        if (strArray[index].StartsWith(str9))
                        {
                            str1 = str1 + "_" + strArray[index];
                            break;
                        }
                    }
                }
            }
            return str1;
        }

        public static int ShowEndpointSelection(string AppID, string AppTitle, string AMessage)
        {
            int piDialogBoxResult = 2;
            MalcolmLegacy.CTAUDADAPTER pCTAudAdapter = new MalcolmLegacy.CTAUDADAPTER();
            pCTAudAdapter.dwSize = (uint)Marshal.SizeOf((object)pCTAudAdapter);
            MalcolmLegacy.CTAUDMANUFACTURER ctaudmanufacturer = MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_Creative;
            MalcolmLegacy.CTAUDMISC ctaudmisc = MalcolmLegacy.CTAUDMISC.CTAUDMISC_AllowSetAsWindowsDefaultDevice | MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI;
            if (!ProductInfo.AllowEndpointFollowWindowsDefault())
                ctaudmisc |= MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotListFollowWindowsDefaultInDeviceSelection;
            MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION pCTProprietaryAPOOutputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOOutputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION pCTProprietaryAPOInputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOInputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection = new MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION();
            pCTProductIDInfoCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTPRODUCTIDINFO));
            if (MalLgcyLib.GetSupportedProducts(ProductInfo.GetSupportedSoftwareFlag(), 0UL, 0UL, 0UL, ref pCTProductIDInfoCollection) == MalcolmLegacy.S_OK)
            {
                if (MalcolmLegacy.CSCTLaunchAudioEndpointSelectionDlgEx5(MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render, uint.MaxValue, ref pCTAudAdapter, AppID, AppTitle, (uint)ctaudmanufacturer, (uint)ctaudmisc, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active, true, (string)null, AMessage, ref pCTProprietaryAPOOutputEffectTypeCollection, ref pCTProprietaryAPOInputEffectTypeCollection, ref pCTProductIDInfoCollection, ref piDialogBoxResult) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                {
                    int num = (int)MalcolmLegacy.CSCTReleaseAudioAdapter(ref pCTAudAdapter);
                }
                else
                    piDialogBoxResult = 0;
                CTGetPdtLibrary.CTReleaseSupportedProducts(ref pCTProductIDInfoCollection);
            }
            else
                piDialogBoxResult = 0;
            return piDialogBoxResult;
        }

        public static bool GetSupportedAdapterInfo(out string AAudioAdapterName, out string APlaybackID, out string AActivePlaybackID, out string ARecordingID)
        {
            AAudioAdapterName = "";
            APlaybackID = "";
            AActivePlaybackID = "";
            ARecordingID = "";
            MalcolmLegacy.CTAUDADAPTERCOLLECTION ctaudadaptercollection = new MalcolmLegacy.CTAUDADAPTERCOLLECTION();
            ctaudadaptercollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDADAPTER));
            MalcolmLegacy.CTAUDMANUFACTURER ctaudmanufacturer = MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_Creative;
            MalcolmLegacy.CTAUDMISC ctaudmisc = MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices | MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI;
            MalcolmLegacy.CTAUDENDPOINTSTATE stateCTAudEndpoint = MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active;
            if (ProductInfo.SupportDualEndpoint())
                stateCTAudEndpoint |= MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_NotPresent;
            MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION pCTProprietaryAPOOutputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOOutputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION pCTProprietaryAPOInputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOInputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection = new MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION();
            pCTProductIDInfoCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTPRODUCTIDINFO));
            if (MalLgcyLib.GetSupportedProducts(ProductInfo.GetSupportedSoftwareFlag(), 0UL, 0UL, 0UL, ref pCTProductIDInfoCollection) == MalcolmLegacy.S_OK)
            {
                if (MalcolmLegacy.CSCTGetAudioAdaptersEx6((uint)ctaudmanufacturer, (uint)ctaudmisc, stateCTAudEndpoint, true, ref pCTProprietaryAPOOutputEffectTypeCollection, ref pCTProprietaryAPOInputEffectTypeCollection, ref pCTProductIDInfoCollection, ref ctaudadaptercollection) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                {
                    IntPtr ptr = ctaudadaptercollection.pCTAudAdapterArray;
                    for (int index = 0; (long)index < (long)ctaudadaptercollection.dwCount; ++index)
                    {
                        MalcolmLegacy.CTAUDADAPTER pCTAudAdapter = new MalcolmLegacy.CTAUDADAPTER();
                        pCTAudAdapter = (MalcolmLegacy.CTAUDADAPTER)Marshal.PtrToStructure(ptr, typeof(MalcolmLegacy.CTAUDADAPTER));
                        if (pCTAudAdapter.lpwszFriendlyName != "")
                        {
                            MalcolmLegacy.CTAUDENDPOINTCOLLECTION ctaudendpointcollection = new MalcolmLegacy.CTAUDENDPOINTCOLLECTION();
                            ctaudendpointcollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDENDPOINT));
                            if (MalcolmLegacy.CSCTGetAudioEndpointsEx5(MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_All, uint.MaxValue, ref pCTAudAdapter, (uint)ctaudmanufacturer, ProductInfo.SupportDualEndpoint() ? 67108864U : (uint)ctaudmisc, stateCTAudEndpoint, ref pCTProprietaryAPOOutputEffectTypeCollection, ref pCTProprietaryAPOInputEffectTypeCollection, ref pCTProductIDInfoCollection, ref ctaudendpointcollection) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                            {
                                MalLgcyLib.SearchSupportedEndpoints(ref ctaudendpointcollection, ref APlaybackID, ref AActivePlaybackID, ref ARecordingID);
                                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpointCollection(ref ctaudendpointcollection);
                                if (APlaybackID != "")
                                    AAudioAdapterName = pCTAudAdapter.lpwszFriendlyName;
                            }
                            if (AAudioAdapterName != "")
                                break;
                        }
                        ptr = (IntPtr)((long)ptr + (long)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDADAPTER)));
                    }
                    int num1 = (int)MalcolmLegacy.CSCTReleaseAudioAdapterCollection(ref ctaudadaptercollection);
                }
                CTGetPdtLibrary.CTReleaseSupportedProducts(ref pCTProductIDInfoCollection);
            }
            return AAudioAdapterName != "" && APlaybackID != "";
        }

        private static unsafe bool SearchSupportedEndpoints(ref MalcolmLegacy.CTAUDENDPOINTCOLLECTION AudEndpointCol, ref string APlaybackID, ref string AActivePlaybackID, ref string ARecordingID)
        {
            APlaybackID = "";
            AActivePlaybackID = "";
            ARecordingID = "";
            List<MalcolmLegacy.CTAUDENDPOINT> ctaudendpointList = new List<MalcolmLegacy.CTAUDENDPOINT>();
            IntPtr ptr = AudEndpointCol.pCTAudEndpointArray;
            for (int index = 0; (long)index < (long)AudEndpointCol.dwCount; ++index)
            {
                MalcolmLegacy.CTAUDENDPOINT ctaudendpoint = new MalcolmLegacy.CTAUDENDPOINT();
                MalcolmLegacy.CTAUDENDPOINT structure = (MalcolmLegacy.CTAUDENDPOINT)Marshal.PtrToStructure(ptr, typeof(MalcolmLegacy.CTAUDENDPOINT));
                ctaudendpointList.Add(structure);
                ptr = (IntPtr)((long)ptr + (long)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDENDPOINT)));
            }
            for (int index1 = 0; index1 < ctaudendpointList.Count; ++index1)
            {
                MalcolmLegacy.CTAUDENDPOINT ctaudendpoint = ctaudendpointList[index1];
                if (ctaudendpoint.typeAudEndpoint == MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_Speaker && APlaybackID == "" && (!ProductInfo.SupportDualEndpoint() || ctaudendpoint.stateCTAudEndpoint == MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active))
                {
                    if (ctaudendpoint.lpguidSoundCoreCOMClassID != (IntPtr)((void*)null))
                    {
                        APlaybackID = ctaudendpoint.lpwszID;
                        AActivePlaybackID = APlaybackID;
                    }
                    else
                    {
                        for (int index2 = 0; index2 < ctaudendpointList.Count; ++index2)
                        {
                            if (ctaudendpointList[index2].lpguidSoundCoreCOMClassID != (IntPtr)((void*)null) && ctaudendpointList[index2].dataflowAudEndpoint == MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render)
                            {
                                APlaybackID = ctaudendpointList[index2].lpwszID;
                                AActivePlaybackID = ctaudendpoint.lpwszID;
                            }
                        }
                    }
                }
                if (ctaudendpoint.typeAudEndpoint == MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_Microphone && ARecordingID == "" && ctaudendpoint.lpguidSoundCoreCOMClassID != (IntPtr)((void*)null))
                    ARecordingID = ctaudendpoint.lpwszID;
            }
            return APlaybackID != "";
        }

        public static string GetActiveSpeakerEndpointID(MalcolmLegacy.CTAUDADAPTER AAudAdapter)
        {
            string str = "";
            MalcolmLegacy.CTAUDENDPOINTCOLLECTION pCTAudEndpointCollection = new MalcolmLegacy.CTAUDENDPOINTCOLLECTION();
            pCTAudEndpointCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDENDPOINT));
            MalcolmLegacy.CTAUDMANUFACTURER ctaudmanufacturer = MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_Creative;
            MalcolmLegacy.CTAUDMISC ctaudmisc = MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices;
            var a = new MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION() { dwCount = 0U };
            var b = new MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION() { dwCount = 0U };
            var c = new MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION() { dwCount = 0U };
            if (MalcolmLegacy.CSCTGetAudioEndpointsEx5(MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render, 2U, ref AAudAdapter, (uint)ctaudmanufacturer, (uint)ctaudmisc, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active, ref a, ref b, ref c, ref pCTAudEndpointCollection) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                IntPtr audEndpointArray = pCTAudEndpointCollection.pCTAudEndpointArray;
                if (pCTAudEndpointCollection.dwCount > 0U)
                {
                    MalcolmLegacy.CTAUDENDPOINT ctaudendpoint = new MalcolmLegacy.CTAUDENDPOINT();
                    str = ((MalcolmLegacy.CTAUDENDPOINT)Marshal.PtrToStructure(audEndpointArray, typeof(MalcolmLegacy.CTAUDENDPOINT))).lpwszID;
                }
                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpointCollection(ref pCTAudEndpointCollection);
            }
            return str;
        }

        public static bool GetLastAdapterEndpointID(string AppID, ref string APlaybackID, ref string AActivePlaybackID, ref string ARecordingID, ref string AAudioAdapterName)
        {
            APlaybackID = "";
            AActivePlaybackID = "";
            ARecordingID = "";
            AAudioAdapterName = "";
            MalcolmLegacy.CTAUDADAPTER pCTAudAdapter = new MalcolmLegacy.CTAUDADAPTER();
            pCTAudAdapter.dwSize = (uint)Marshal.SizeOf((object)pCTAudAdapter);
            MalcolmLegacy.CTAUDENDPOINTCOLLECTION ctaudendpointcollection = new MalcolmLegacy.CTAUDENDPOINTCOLLECTION();
            ctaudendpointcollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDENDPOINT));
            MalcolmLegacy.CTAUDMISC ctaudmisc = MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices;
            if (!ProductInfo.SupportDualEndpoint())
                ctaudmisc |= MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI;
            MalcolmLegacy.CTAUDENDPOINTSTATE stateCTAudEndpoint = MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active;
            if (ProductInfo.SupportDualEndpoint())
                stateCTAudEndpoint |= MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_NotPresent;
            if (MalcolmLegacy.CSCTGetLastSelectedAudioAdapterEx3(AppID, stateCTAudEndpoint, (uint)ctaudmisc, ref pCTAudAdapter, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_All, ref ctaudendpointcollection) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                if (pCTAudAdapter.lpwszFriendlyName == null)
                {
                    string AAudioAdapterName1;
                    string APlaybackID1;
                    string AActivePlaybackID1;
                    string ARecordingID1;
                    if (MalLgcyLib.GetSupportedAdapterInfo(out AAudioAdapterName1, out APlaybackID1, out AActivePlaybackID1, out ARecordingID1))
                    {
                        AAudioAdapterName = AAudioAdapterName1;
                        APlaybackID = APlaybackID1;
                        AActivePlaybackID = AActivePlaybackID1;
                        ARecordingID = ARecordingID1;
                        int num = (int)MalcolmLegacy.CSCTSetAudioAdapter(AAudioAdapterName, AppID);
                    }
                }
                else
                {
                    AAudioAdapterName = pCTAudAdapter.lpwszFriendlyName;
                    MalLgcyLib.SearchSupportedEndpoints(ref ctaudendpointcollection, ref APlaybackID, ref AActivePlaybackID, ref ARecordingID);
                    int num1 = (int)MalcolmLegacy.CSCTReleaseAudioAdapter(ref pCTAudAdapter);
                    int num2 = (int)MalcolmLegacy.CSCTReleaseAudioEndpointCollection(ref ctaudendpointcollection);
                }
            }
            return APlaybackID != "" || ARecordingID != "";
        }

        public static int ShowAdapterSelection(string AppID, string AppTitle, string AMessage)
        {
            int piDialogBoxResult = 2;
            MalcolmLegacy.CTAUDMANUFACTURER ctaudmanufacturer = MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_Creative;
            MalcolmLegacy.CTAUDMISC ctaudmisc = MalcolmLegacy.CTAUDMISC.CTAUDMISC_AllowSetAsWindowsDefaultDevice | MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI;
            if (!ProductInfo.AllowEndpointFollowWindowsDefault())
                ctaudmisc |= MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotListFollowWindowsDefaultInDeviceSelection;
            MalcolmLegacy.CTAUDENDPOINTSTATE stateCTAudEndpoint = MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active;
            if (ProductInfo.SupportDualEndpoint())
                stateCTAudEndpoint |= MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_NotPresent;
            MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION pCTProprietaryAPOOutputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOOutputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION pCTProprietaryAPOInputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOInputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection = new MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION();
            pCTProductIDInfoCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTPRODUCTIDINFO));
            if (MalLgcyLib.GetSupportedProducts(ProductInfo.GetSupportedSoftwareFlag(), 0UL, 0UL, 0UL, ref pCTProductIDInfoCollection) == MalcolmLegacy.S_OK)
            {
                if (MalcolmLegacy.CSCTLaunchAudioAdapterSelectionDlgEx6(AppID, AppTitle, (uint)ctaudmanufacturer, (uint)ctaudmisc, stateCTAudEndpoint, true, true, (string)null, AMessage, ref pCTProprietaryAPOOutputEffectTypeCollection, ref pCTProprietaryAPOInputEffectTypeCollection, ref pCTProductIDInfoCollection, ref piDialogBoxResult) != MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                    piDialogBoxResult = 0;
                CTGetPdtLibrary.CTReleaseSupportedProducts(ref pCTProductIDInfoCollection);
            }
            else
                piDialogBoxResult = 0;
            return piDialogBoxResult;
        }

        public static bool GetSupportedEndpointID(out string APlaybackID, out string AAudioAdapterName)
        {
            APlaybackID = "";
            AAudioAdapterName = "";
            MalcolmLegacy.CTAUDENDPOINTCOLLECTION pCTAudEndpointCollection = new MalcolmLegacy.CTAUDENDPOINTCOLLECTION();
            pCTAudEndpointCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDENDPOINT));
            MalcolmLegacy.CTAUDADAPTER pCTAudAdapter = new MalcolmLegacy.CTAUDADAPTER();
            pCTAudAdapter.dwSize = (uint)Marshal.SizeOf((object)pCTAudAdapter);
            pCTAudAdapter.lpwszFriendlyName = (string)null;
            MalcolmLegacy.CTAUDMANUFACTURER ctaudmanufacturer = MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_Creative;
            MalcolmLegacy.CTAUDMISC ctaudmisc = MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices | MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI;
            MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION pCTProprietaryAPOOutputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOOutputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION pCTProprietaryAPOInputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOInputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection = new MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION();
            pCTProductIDInfoCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTPRODUCTIDINFO));
            //if (MalLgcyLib.GetSupportedProducts(ProductInfo.GetSupportedSoftwareFlag(), 0UL, 0UL, 0UL, ref pCTProductIDInfoCollection) == MalcolmLegacy.S_OK)
            {
                if (MalcolmLegacy.CSCTGetAudioEndpointsEx5(MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render, uint.MaxValue, ref pCTAudAdapter, (uint)ctaudmanufacturer, (uint)ctaudmisc, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active, ref pCTProprietaryAPOOutputEffectTypeCollection, ref pCTProprietaryAPOInputEffectTypeCollection, ref pCTProductIDInfoCollection, ref pCTAudEndpointCollection) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                {
                    IntPtr audEndpointArray = pCTAudEndpointCollection.pCTAudEndpointArray;
                    if (pCTAudEndpointCollection.dwCount > 0U)
                    {
                        MalcolmLegacy.CTAUDENDPOINT ctaudendpoint = new MalcolmLegacy.CTAUDENDPOINT();
                        MalcolmLegacy.CTAUDENDPOINT structure = (MalcolmLegacy.CTAUDENDPOINT)Marshal.PtrToStructure(audEndpointArray, typeof(MalcolmLegacy.CTAUDENDPOINT));
                        APlaybackID = structure.lpwszID;
                        AAudioAdapterName = structure.lpwszAudioAdapterFriendlyName;
                    }
                    int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpointCollection(ref pCTAudEndpointCollection);
                }
                CTGetPdtLibrary.CTReleaseSupportedProducts(ref pCTProductIDInfoCollection);
            }
            return APlaybackID != "";
        }

        private static bool GetSupportedRecordingEndpointID(string AAudioAdapterName, MalcolmLegacy.CTAUDENDPOINTTYPE APreferType, out string ARecordingEptID)
        {
            ARecordingEptID = "";
            List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList = new List<MalcolmLegacy.CTAUDENDPOINT>();
            MalcolmLegacy.CTAUDMANUFACTURER AudManufacturer = MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_Creative;
            if (MalLgcyLib.GetSupportedMicrophoneList(AAudioAdapterName, AudManufacturer, AAudEndpointList) && AAudEndpointList.Count > 0)
            {
                if (APreferType != MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_MASK_All)
                {
                    for (int index = 0; index < AAudEndpointList.Count; ++index)
                    {
                        if (AAudEndpointList[index].typeAudEndpoint == APreferType)
                        {
                            ARecordingEptID = AAudEndpointList[index].lpwszID;
                            break;
                        }
                    }
                }
                if (ARecordingEptID == "")
                    ARecordingEptID = AAudEndpointList[0].lpwszID;
            }
            return ARecordingEptID != "";
        }

        public static bool GetLastEndpointID(string AppID, ref string APlaybackID, ref string ARecordingID, ref string AAudioAdapterName)
        {
            APlaybackID = "";
            ARecordingID = "";
            AAudioAdapterName = "";
            MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint1 = new MalcolmLegacy.CTAUDENDPOINT();
            pCTAudEndpoint1.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint1);
            if (MalcolmLegacy.CSCTGetLastSelectedAudioEndpointEx2(MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render, AppID, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active, 0U, ref pCTAudEndpoint1) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                if (pCTAudEndpoint1.lpwszID == null)
                {
                    if (ProductInfo.AllowEndpointFollowWindowsDefault())
                    {
                        string APlaybackID1;
                        string AAudioAdapterName1;
                        if (MalLgcyLib.GetWindowDefaultPlaybackEndpointID(out APlaybackID1, out AAudioAdapterName1))
                        {
                            APlaybackID = APlaybackID1;
                            AAudioAdapterName = AAudioAdapterName1;
                        }
                    }
                    else
                    {
                        string APlaybackID1;
                        string AAudioAdapterName1;
                        if (MalLgcyLib.GetSupportedEndpointID(out APlaybackID1, out AAudioAdapterName1))
                        {
                            APlaybackID = APlaybackID1;
                            AAudioAdapterName = AAudioAdapterName1;
                            int num = (int)MalcolmLegacy.CSCTSetAudioEndpoint(APlaybackID, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render, AppID);
                        }
                    }
                }
                else
                {
                    APlaybackID = pCTAudEndpoint1.lpwszID;
                    AAudioAdapterName = pCTAudEndpoint1.lpwszAudioAdapterFriendlyName;
                }
                int num1 = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint1);
            }
            if (AAudioAdapterName == "")
                return false;
            if (!ProductInfo.IsMalcolmCardProduct())
            {
                MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint2 = new MalcolmLegacy.CTAUDENDPOINT();
                pCTAudEndpoint2.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint2);
                if (MalcolmLegacy.CSCTGetLastSelectedAudioEndpointEx2(MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Capture, AppID, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active, 0U, ref pCTAudEndpoint2) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                {
                    if (pCTAudEndpoint2.lpwszID != null && MalLgcyLib.GetEndpointAdapterName(pCTAudEndpoint2.lpwszID) == AAudioAdapterName)
                        ARecordingID = pCTAudEndpoint2.lpwszID;
                    string ARecordingEptID;
                    if (ARecordingID == "" && MalLgcyLib.GetSupportedRecordingEndpointID(AAudioAdapterName, MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_MicrophoneArray, out ARecordingEptID))
                    {
                        ARecordingID = ARecordingEptID;
                        int num = (int)MalcolmLegacy.CSCTSetAudioEndpoint(ARecordingID, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Capture, AppID);
                    }
                    int num1 = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint2);
                }
            }
            return APlaybackID != "" || ARecordingID != "";
        }

        public static bool GetEndpointList(List<string> AEndpointIDList, MalcolmLegacy.CTAUDENDPOINTSTATE ACTAudEndpointState, List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList)
        {
            for (int index = 0; index < AEndpointIDList.Count; ++index)
            {
                MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
                pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
                if (MalcolmLegacy.CSCTGetAudioEndpointInfoEx(AEndpointIDList[index], 0U, ref pCTAudEndpoint) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                {
                    if ((pCTAudEndpoint.stateCTAudEndpoint & ACTAudEndpointState) == pCTAudEndpoint.stateCTAudEndpoint)
                        AAudEndpointList.Add(pCTAudEndpoint);
                    int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
                }
            }
            return AAudEndpointList.Count > 0;
        }

        public static bool GetSupportedMicrophoneList(string AAudioAdapterName, MalcolmLegacy.CTAUDMANUFACTURER AudManufacturer, List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList)
        {
            return MalLgcyLib.GetEndpointList(AAudioAdapterName, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Capture, MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_Microphone | MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_MicrophoneArray | MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_Headset_Microphone, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active, AudManufacturer, MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices | MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI, AAudEndpointList);
        }

        public static bool GetEndpointList(string AAudioAdapterName, MalcolmLegacy.CTAUDENDPOINTDATAFLOW ACTAudEndpointDataFlow, MalcolmLegacy.CTAUDENDPOINTTYPE ACTAudEndpointType, MalcolmLegacy.CTAUDENDPOINTSTATE ACTAudEndpointState, MalcolmLegacy.CTAUDMANUFACTURER ACTAudManufacturer, MalcolmLegacy.CTAUDMISC ACTAudMisc, List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList)
        {
            AAudEndpointList.Clear();
            MalcolmLegacy.CTAUDENDPOINTCOLLECTION pCTAudEndpointCollection = new MalcolmLegacy.CTAUDENDPOINTCOLLECTION();
            pCTAudEndpointCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDENDPOINT));
            MalcolmLegacy.CTAUDADAPTER pCTAudAdapter = new MalcolmLegacy.CTAUDADAPTER();
            pCTAudAdapter.dwSize = (uint)Marshal.SizeOf((object)pCTAudAdapter);
            pCTAudAdapter.lpwszFriendlyName = AAudioAdapterName;
            MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION pCTProprietaryAPOOutputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOOUTPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOOutputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION pCTProprietaryAPOInputEffectTypeCollection = new MalcolmLegacy.CTPROPRIETARYAPOINPUTEFFECTTYPECOLLECTION();
            pCTProprietaryAPOInputEffectTypeCollection.dwCount = 0U;
            MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection = new MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION();
            pCTProductIDInfoCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTPRODUCTIDINFO));
            pCTProductIDInfoCollection.dwReserved = 0U;
            pCTProductIDInfoCollection.dwCount = 0U;
            bool flag = false;
            if ((ACTAudMisc & MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI) == MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI)
            {
                if (MalLgcyLib.GetSupportedProducts(ProductInfo.GetSupportedSoftwareFlag(), 0UL, 0UL, 0UL, ref pCTProductIDInfoCollection) != MalcolmLegacy.S_OK)
                    return false;
                flag = true;
            }
            if (MalcolmLegacy.CSCTGetAudioEndpointsEx5(ACTAudEndpointDataFlow, (uint)ACTAudEndpointType, ref pCTAudAdapter, (uint)ACTAudManufacturer, (uint)ACTAudMisc, ACTAudEndpointState, ref pCTProprietaryAPOOutputEffectTypeCollection, ref pCTProprietaryAPOInputEffectTypeCollection, ref pCTProductIDInfoCollection, ref pCTAudEndpointCollection) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                IntPtr ptr = pCTAudEndpointCollection.pCTAudEndpointArray;
                for (int index = 0; (long)index < (long)pCTAudEndpointCollection.dwCount; ++index)
                {
                    MalcolmLegacy.CTAUDENDPOINT ctaudendpoint = new MalcolmLegacy.CTAUDENDPOINT();
                    MalcolmLegacy.CTAUDENDPOINT structure = (MalcolmLegacy.CTAUDENDPOINT)Marshal.PtrToStructure(ptr, typeof(MalcolmLegacy.CTAUDENDPOINT));
                    AAudEndpointList.Add(structure);
                    ptr = (IntPtr)((long)ptr + (long)Marshal.SizeOf((object)structure));
                }
                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpointCollection(ref pCTAudEndpointCollection);
            }
            if (flag)
                CTGetPdtLibrary.CTReleaseSupportedProducts(ref pCTProductIDInfoCollection);
            return AAudEndpointList.Count > 0;
        }

        public static int GetSupportedProducts(ulong ddwSupportedSoftwareFlag, ulong ddwReserved1, ulong ddwReserved2, ulong ddwReserved3, ref MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection)
        {
            if ((long)ddwSupportedSoftwareFlag != (long)CTGetPdtLibrary.CTSSF_All)
                return CTGetPdtLibrary.CTGetSupportedProducts(ddwSupportedSoftwareFlag, ddwReserved1, ddwReserved2, ddwReserved3, ref pCTProductIDInfoCollection);
            pCTProductIDInfoCollection.dwCount = 0U;
            return MalcolmLegacy.S_OK;
        }

        public static string GetSpdifOutEndpointID(string AAudioAdapterName)
        {
            string str = "";
            List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList = new List<MalcolmLegacy.CTAUDENDPOINT>();
            AAudEndpointList.Clear();
            if (MalLgcyLib.GetEndpointList(AAudioAdapterName, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render, MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_MASK_All, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active | MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Disabled | MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unplugged, MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_All, MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices, AAudEndpointList))
            {
                for (int index = 0; index < AAudEndpointList.Count; ++index)
                {
                    MalcolmLegacy.CTAUDENDPOINT ctaudendpoint = AAudEndpointList[index];
                    if (ctaudendpoint.dataflowAudEndpoint == MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render && ctaudendpoint.typeAudEndpoint == MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_SPDIFInterface)
                    {
                        str = ctaudendpoint.lpwszID;
                        break;
                    }
                }
            }
            return str;
        }

        public static string GetLineInEndpointID(string AAudioAdapterName)
        {
            string str = "";
            List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList = new List<MalcolmLegacy.CTAUDENDPOINT>();
            AAudEndpointList.Clear();
            if (MalLgcyLib.GetEndpointList(AAudioAdapterName, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Capture, MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_LineConnector, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active | MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Disabled | MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unplugged, MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_All, MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices, AAudEndpointList) && AAudEndpointList.Count > 0)
                str = AAudEndpointList[0].lpwszID;
            return str;
        }

        public static bool GetEncoderEndpointID(string AAudioAdapterName, ref string ASpdifOutEndpointID, ref string AWhatUHearEndpointID)
        {
            ASpdifOutEndpointID = "";
            AWhatUHearEndpointID = "";
            List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList = new List<MalcolmLegacy.CTAUDENDPOINT>();
            AAudEndpointList.Clear();
            if (MalLgcyLib.GetEndpointList(AAudioAdapterName, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_All, MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_MASK_All, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active | MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Disabled | MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unplugged, MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_All, MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices, AAudEndpointList))
            {
                for (int index = 0; index < AAudEndpointList.Count; ++index)
                {
                    MalcolmLegacy.CTAUDENDPOINT ctaudendpoint = AAudEndpointList[index];
                    if (ctaudendpoint.dataflowAudEndpoint == MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render)
                    {
                        if (ctaudendpoint.typeAudEndpoint == MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_SPDIFInterface)
                            ASpdifOutEndpointID = ctaudendpoint.lpwszID;
                    }
                    else if (ctaudendpoint.dataflowAudEndpoint == MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Capture && ctaudendpoint.typeAudEndpoint == MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_DigitalAudioInterface)
                        AWhatUHearEndpointID = ctaudendpoint.lpwszID;
                }
            }
            return ASpdifOutEndpointID != "" && AWhatUHearEndpointID != "";
        }

        public static bool GetEncoderEndpointID(List<string> AEndpointIDList, ref string ASpdifOutEndpointID, ref string AWhatUHearEndpointID)
        {
            ASpdifOutEndpointID = "";
            AWhatUHearEndpointID = "";
            for (int index = 0; index < AEndpointIDList.Count; ++index)
            {
                MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
                pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
                if (MalcolmLegacy.CSCTGetAudioEndpointInfoEx(AEndpointIDList[index], 0U, ref pCTAudEndpoint) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                {
                    if (pCTAudEndpoint.dataflowAudEndpoint == MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render)
                    {
                        if (pCTAudEndpoint.typeAudEndpoint == MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_SPDIFInterface)
                            ASpdifOutEndpointID = pCTAudEndpoint.lpwszID;
                    }
                    else if (pCTAudEndpoint.dataflowAudEndpoint == MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Capture && pCTAudEndpoint.typeAudEndpoint == MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_DigitalAudioInterface)
                        AWhatUHearEndpointID = pCTAudEndpoint.lpwszID;
                    int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
                }
            }
            return ASpdifOutEndpointID != "" && AWhatUHearEndpointID != "";
        }

        public static bool GetWindowDefaultPlaybackEndpointID(out string APlaybackID, out string AAudioAdapterName)
        {
            APlaybackID = "";
            AAudioAdapterName = "";
            MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
            pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
            if (MalcolmLegacy.CSCTGetDefaultAudioEndpointEx(MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render, MalcolmLegacy.CTAUDENDPOINTROLE.CTAUDENDPOINTROLE_Multimedia, 0U, ref pCTAudEndpoint) != MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                return false;
            string lpwszId = pCTAudEndpoint.lpwszID;
            string adapterFriendlyName = pCTAudEndpoint.lpwszAudioAdapterFriendlyName;
            List<MalcolmLegacy.CTAUDENDPOINT> AAudEndpointList = new List<MalcolmLegacy.CTAUDENDPOINT>();
            AAudEndpointList.Clear();
            if (MalLgcyLib.GetEndpointList(adapterFriendlyName, MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Render, MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_MASK_All, MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active, MalcolmLegacy.CTAUDMANUFACTURER.CTAUDMANUFACTURER_All, MalcolmLegacy.CTAUDMISC.CTAUDMISC_DoNotNeedToCheckForCADISupportedDevices | MalcolmLegacy.CTAUDMISC.CTAUDMISC_DeviceMustBeSupportedByCreativeSoundCoreAPI, AAudEndpointList))
            {
                for (int index = 0; index < AAudEndpointList.Count; ++index)
                {
                    if (AAudEndpointList[index].lpwszID == lpwszId)
                    {
                        APlaybackID = lpwszId;
                        AAudioAdapterName = adapterFriendlyName;
                        break;
                    }
                }
            }
            int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
            return APlaybackID != "" && AAudioAdapterName != "";
        }

        public static string GetWindowDefaultEndpointID(MalcolmLegacy.CTAUDENDPOINTDATAFLOW AAudEndpointDataFlow, MalcolmLegacy.CTAUDENDPOINTROLE AAudEndpointRole)
        {
            string str = "";
            MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
            pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
            if (MalcolmLegacy.CSCTGetDefaultAudioEndpointEx(AAudEndpointDataFlow, AAudEndpointRole, 0U, ref pCTAudEndpoint) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                str = pCTAudEndpoint.lpwszID;
                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
            }
            return str;
        }

        public static bool SetWindowDefaultEndpointID(string AEndpointID)
        {
            return MalcolmLegacy.CSCTSetDefaultAudioEndpoint(AEndpointID, MalcolmLegacy.CTAUDENDPOINTROLE.CTAUDENDPOINTROLE_Console) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success && MalcolmLegacy.CSCTSetDefaultAudioEndpoint(AEndpointID, MalcolmLegacy.CTAUDENDPOINTROLE.CTAUDENDPOINTROLE_Multimedia) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success && MalcolmLegacy.CSCTSetDefaultAudioEndpoint(AEndpointID, MalcolmLegacy.CTAUDENDPOINTROLE.CTAUDENDPOINTROLE_Communications) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success;
        }

        public static string GetEndpointName(string AEndpointID)
        {
            string str = "";
            MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
            pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
            if (MalcolmLegacy.CSCTGetAudioEndpointInfoEx(AEndpointID, 0U, ref pCTAudEndpoint) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                str = pCTAudEndpoint.lpwszDesc;
                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
            }
            return str;
        }

        public static string GetEndpointFullName(string AEndpointID)
        {
            string str = "";
            MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
            pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
            if (MalcolmLegacy.CSCTGetAudioEndpointInfoEx(AEndpointID, 0U, ref pCTAudEndpoint) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                str = pCTAudEndpoint.lpwszFriendlyName;
                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
            }
            return str;
        }

        public static string GetEndpointAdapterName(string AEndpointID)
        {
            string str = "";
            MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
            pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
            if (MalcolmLegacy.CSCTGetAudioEndpointInfoEx(AEndpointID, 0U, ref pCTAudEndpoint) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                str = pCTAudEndpoint.lpwszAudioAdapterFriendlyName;
                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
            }
            return str;
        }

        public static bool IsEndpointActive(string AEndpointID)
        {
            MalcolmLegacy.CTAUDENDPOINTSTATE pCTAudEndpointState = MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unknown;
            if (MalcolmLegacy.CSCTGetAudioEndpointState(AEndpointID, ref pCTAudEndpointState) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
                return pCTAudEndpointState == MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active;
            return false;
        }

        public static bool IsEndpointInfo(string AEndpointID, string AAdapterName, MalcolmLegacy.CTAUDENDPOINTTYPE AEndpointType)
        {
            if (AEndpointID == "" || AAdapterName == "")
                return false;
            string str = "";
            MalcolmLegacy.CTAUDENDPOINTTYPE ctaudendpointtype = MalcolmLegacy.CTAUDENDPOINTTYPE.CTAUDENDPOINTTYPE_Unknown;
            MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
            pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
            if (MalcolmLegacy.CSCTGetAudioEndpointInfoEx(AEndpointID, 0U, ref pCTAudEndpoint) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                str = pCTAudEndpoint.lpwszAudioAdapterFriendlyName;
                ctaudendpointtype = pCTAudEndpoint.typeAudEndpoint;
                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
            }
            return AAdapterName != null && str != null && AAdapterName.ToLower() == str.ToLower() && AEndpointType == ctaudendpointtype;
        }

        public static bool IsEndpointInfo(string AEndpointID, string AAdapterName, MalcolmLegacy.CTAUDENDPOINTDATAFLOW ADataFlow)
        {
            if (AEndpointID == "" || AAdapterName == "")
                return false;
            string str = "";
            MalcolmLegacy.CTAUDENDPOINTDATAFLOW ctaudendpointdataflow = MalcolmLegacy.CTAUDENDPOINTDATAFLOW.CTAUDENDPOINTDATAFLOW_Unknown;
            MalcolmLegacy.CTAUDENDPOINT pCTAudEndpoint = new MalcolmLegacy.CTAUDENDPOINT();
            pCTAudEndpoint.dwSize = (uint)Marshal.SizeOf((object)pCTAudEndpoint);
            if (MalcolmLegacy.CSCTGetAudioEndpointInfoEx(AEndpointID, 0U, ref pCTAudEndpoint) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                str = pCTAudEndpoint.lpwszAudioAdapterFriendlyName;
                ctaudendpointdataflow = pCTAudEndpoint.dataflowAudEndpoint;
                int num = (int)MalcolmLegacy.CSCTReleaseAudioEndpoint(ref pCTAudEndpoint);
            }
            return AAdapterName != null && str != null && AAdapterName.ToLower() == str.ToLower() && ADataFlow == ctaudendpointdataflow;
        }

        public static string GetJackLocationStr(string AEndpointID)
        {
            string str = "";
            MalcolmLegacy.CTAUDENDPOINTJACKINFOCOLLECTION pCTAudEndpointJackInfoCollection = new MalcolmLegacy.CTAUDENDPOINTJACKINFOCOLLECTION();
            pCTAudEndpointJackInfoCollection.dwSizePerArrayElement = (uint)Marshal.SizeOf(typeof(MalcolmLegacy.CTAUDENDPOINTJACKINFO));
            if (MalcolmLegacy.CSCTGetAudioEndpointJackInfo(AEndpointID, ref pCTAudEndpointJackInfoCollection) == MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success)
            {
                if ((int)pCTAudEndpointJackInfoCollection.dwCount == 1)
                {
                    IntPtr ptr = pCTAudEndpointJackInfoCollection.pCTAudEndpointJackInfoArray;
                    for (int index = 0; (long)index < (long)pCTAudEndpointJackInfoCollection.dwCount; ++index)
                    {
                        MalcolmLegacy.CTAUDENDPOINTJACKINFO ctaudendpointjackinfo = new MalcolmLegacy.CTAUDENDPOINTJACKINFO();
                        ctaudendpointjackinfo = (MalcolmLegacy.CTAUDENDPOINTJACKINFO)Marshal.PtrToStructure(ptr, typeof(MalcolmLegacy.CTAUDENDPOINTJACKINFO));
                        str = MalLgcyLib.GetJackGeometricLocationText(ctaudendpointjackinfo.jackGeometricLocation);
                        ptr = (IntPtr)((long)ptr + (long)Marshal.SizeOf((object)ctaudendpointjackinfo));
                    }
                }
                int num = (int)MalcolmLegacy.CSCTReleaseParamJackInfoCollection(ref pCTAudEndpointJackInfoCollection);
            }
            return str;
        }

        private static string GetJackGeometricLocationText(MalcolmLegacy.CTJACKGEOMETRICLOCATION AJackGeometricLocation)
        {
            switch (AJackGeometricLocation)
            {
                case MalcolmLegacy.CTJACKGEOMETRICLOCATION.CTJACKGEOMETRICLOCATION_GeoLocRear:
                    return MalcolmUtil.GetResourceString("resStrJackLocRearPanel");
                case MalcolmLegacy.CTJACKGEOMETRICLOCATION.CTJACKGEOMETRICLOCATION_GeoLocFront:
                    return MalcolmUtil.GetResourceString("resStrJackLocFrontPanel");
                case MalcolmLegacy.CTJACKGEOMETRICLOCATION.CTJACKGEOMETRICLOCATION_GeoLocLeft:
                    return MalcolmUtil.GetResourceString("resStrJackLocLeftPanel");
                case MalcolmLegacy.CTJACKGEOMETRICLOCATION.CTJACKGEOMETRICLOCATION_GeoLocRight:
                    return MalcolmUtil.GetResourceString("resStrJackLocRightPanel");
                case MalcolmLegacy.CTJACKGEOMETRICLOCATION.CTJACKGEOMETRICLOCATION_GeoLocTop:
                    return MalcolmUtil.GetResourceString("resStrJackLocTopPanel");
                case MalcolmLegacy.CTJACKGEOMETRICLOCATION.CTJACKGEOMETRICLOCATION_GeoLocBottom:
                    return MalcolmUtil.GetResourceString("resStrJackLocBottomPanel");
                default:
                    return "";
            }
        }

        public static void StopAllOutputEndpointGlobalMonitorAudioInputEndpoint(string ADestEndpointID, string AAdapterName)
        {
            int num = (int)MalLgcyLib.StopGlobalMonitorAudioInputEndpoint("", ADestEndpointID);
        }

        public static bool GetGlobalMonitorAudioInputEndpointStatus(string ASrcEndpointID, string ADestEndpointID, out string AEncoderGuid)
        {
            bool pfMonitoringInProgress = false;
            MalcolmLegacy.CTAUDENCODING pAudEncoding = new MalcolmLegacy.CTAUDENCODING();
            pAudEncoding.dwSize = (uint)Marshal.SizeOf((object)pAudEncoding);
            MalcolmLegacy.CTAUDEFFECTCOLLECTION pCTAudEffectCollection = new MalcolmLegacy.CTAUDEFFECTCOLLECTION();
            AEncoderGuid = MalcolmLegacy.CSCTGetGlobalMonitorAudioInputEndpointStatusEx(ASrcEndpointID, ADestEndpointID, ref pfMonitoringInProgress, ref pAudEncoding, ref pCTAudEffectCollection) != MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success ? "" : (!pfMonitoringInProgress ? "" : pAudEncoding.guidCTAudEncoding.ToString());
            return pfMonitoringInProgress;
        }

        public static MalcolmLegacy.CTAUDENDPOINTRESULT StartGlobalMonitorAudioInputEndpoint(string ASrcEndpointID, string ADestEndpointID, string AGuidCTAudEncoding, MalcolmLegacy.CTAUDOUTPUTMONITORINGFLAG AMonitoringFlag)
        {
            MalcolmLegacy.CTAUDENCODING encodingAud = new MalcolmLegacy.CTAUDENCODING();
            encodingAud.dwSize = (uint)Marshal.SizeOf((object)encodingAud);
            encodingAud.guidCTAudEncoding = new Guid(AGuidCTAudEncoding);
            if (encodingAud.guidCTAudEncoding.ToString() == MalcolmLegacy.CTAUDENCODING_DolbyDigitalLive)
            {
                encodingAud.guidEncoding.encodingDolbyDigitalLive = new MalcolmLegacy.CTDOLBYDIGITALLIVEENCODING();
                encodingAud.guidEncoding.encodingDolbyDigitalLive.dwSize = (uint)Marshal.SizeOf((object)encodingAud.guidEncoding.encodingDolbyDigitalLive);
                encodingAud.guidEncoding.encodingDolbyDigitalLive.dwChannelMask = 0U;
            }
            else if (encodingAud.guidCTAudEncoding.ToString() == MalcolmLegacy.CTAUDENCODING_DTSInteractive)
            {
                encodingAud.guidEncoding.encodingDTSInteractive = new MalcolmLegacy.CTDTSINTERACTIVEENCODING();
                encodingAud.guidEncoding.encodingDTSInteractive.dwSize = (uint)Marshal.SizeOf((object)encodingAud.guidEncoding.encodingDTSInteractive);
                encodingAud.guidEncoding.encodingDTSInteractive.dwChannelMask = 0U;
            }
            MalcolmLegacy.CTAUDEFFECTCOLLECTION collectionAudEffect = new MalcolmLegacy.CTAUDEFFECTCOLLECTION();
            collectionAudEffect.dwCount = 0U;
            float flOutputVolume = 1f;
            string lpwszAudInputEndpointID = ASrcEndpointID;
            string lpwszAudOutputEndpointID = ADestEndpointID;
            Util.PrintDebug("Start Global Monitoring: " + ASrcEndpointID + " / " + ADestEndpointID + " / " + AGuidCTAudEncoding);
            return MalcolmLegacy.CSCTStartGlobalMonitorAudioInputEndpointEx2(lpwszAudInputEndpointID, lpwszAudOutputEndpointID, uint.MaxValue, 2U, AMonitoringFlag, encodingAud, collectionAudEffect, IntPtr.Zero, flOutputVolume);
        }

        public static MalcolmLegacy.CTAUDENDPOINTRESULT StopGlobalMonitorAudioInputEndpoint(string ASrcEndpointID, string ADestEndpointID)
        {
            string lpwszAudInputEndpointID = ASrcEndpointID;
            string lpwszAudOutputEndpointID = ADestEndpointID;
            Util.PrintDebug("Stop Global Monitoring: " + ASrcEndpointID + " / " + ADestEndpointID);
            return MalcolmLegacy.CSCTStopGlobalMonitorAudioInputEndpointEx(lpwszAudInputEndpointID, lpwszAudOutputEndpointID, IntPtr.Zero);
        }

        public static bool GetGlobalMonitorAudioOutputEndpointStatus(string ASrcEndpointID, string ADestEndpointID, out string AEncoderGuid)
        {
            bool pfMonitoringInProgress = false;
            MalcolmLegacy.CTAUDENCODING pAudEncoding = new MalcolmLegacy.CTAUDENCODING();
            pAudEncoding.dwSize = (uint)Marshal.SizeOf((object)pAudEncoding);
            AEncoderGuid = MalcolmLegacy.CSCTGetGlobalMonitorAudioOutputEndpointStatusEx(ASrcEndpointID, ADestEndpointID, ref pfMonitoringInProgress, ref pAudEncoding) != MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success ? "" : (!pfMonitoringInProgress ? "" : pAudEncoding.guidCTAudEncoding.ToString());
            return pfMonitoringInProgress;
        }

        public static MalcolmLegacy.CTAUDENDPOINTRESULT StartGlobalMonitorAudioOutputEndpoint(string ASrcEndpointID, string ADestEndpointID, string AGuidCTAudEncoding, MalcolmLegacy.CTAUDOUTPUTMONITORINGFLAG AMonitoringFlag)
        {
            MalcolmLegacy.CTAUDENCODING encodingAud = new MalcolmLegacy.CTAUDENCODING();
            encodingAud.dwSize = (uint)Marshal.SizeOf((object)encodingAud);
            encodingAud.guidCTAudEncoding = new Guid(AGuidCTAudEncoding);
            if (encodingAud.guidCTAudEncoding.ToString() == MalcolmLegacy.CTAUDENCODING_DolbyDigitalLive)
            {
                encodingAud.guidEncoding.encodingDolbyDigitalLive = new MalcolmLegacy.CTDOLBYDIGITALLIVEENCODING();
                encodingAud.guidEncoding.encodingDolbyDigitalLive.dwSize = (uint)Marshal.SizeOf((object)encodingAud.guidEncoding.encodingDolbyDigitalLive);
                encodingAud.guidEncoding.encodingDolbyDigitalLive.dwChannelMask = 0U;
            }
            else if (encodingAud.guidCTAudEncoding.ToString() == MalcolmLegacy.CTAUDENCODING_DTSInteractive)
            {
                encodingAud.guidEncoding.encodingDTSInteractive = new MalcolmLegacy.CTDTSINTERACTIVEENCODING();
                encodingAud.guidEncoding.encodingDTSInteractive.dwSize = (uint)Marshal.SizeOf((object)encodingAud.guidEncoding.encodingDTSInteractive);
                encodingAud.guidEncoding.encodingDTSInteractive.dwChannelMask = 0U;
            }
            uint maxValue1 = uint.MaxValue;
            uint maxValue2 = uint.MaxValue;
            Util.PrintDebug("Start Global MAOE " + ASrcEndpointID + " / " + ADestEndpointID + " / " + AGuidCTAudEncoding);
            return MalcolmLegacy.CSCTStartGlobalMonitorAudioOutputEndpointEx(ASrcEndpointID, ADestEndpointID, maxValue1, maxValue2, AMonitoringFlag, encodingAud, IntPtr.Zero);
        }

        public static MalcolmLegacy.CTAUDENDPOINTRESULT StopGlobalMonitorAudioOutputEndpoint(string ASrcEndpointID, string ADestEndpointID)
        {
            string lpwszAudOutputEndpointIDSrc = ASrcEndpointID;
            string lpwszAudOutputEndpointIDDest = ADestEndpointID;
            Util.PrintDebug("Stop Global MAOE: " + ASrcEndpointID + " / " + ADestEndpointID);
            return MalcolmLegacy.CSCTStopGlobalMonitorAudioOutputEndpointEx(lpwszAudOutputEndpointIDSrc, lpwszAudOutputEndpointIDDest, IntPtr.Zero);
        }

        public static void ForceEnableEndpointID(string AEndpointID)
        {
            MalcolmLegacy.CTAUDENDPOINTSTATE pCTAudEndpointState = MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unknown;
            if (MalcolmLegacy.CSCTGetAudioEndpointState(AEndpointID, ref pCTAudEndpointState) != MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success || pCTAudEndpointState != MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Disabled)
                return;
            int num = (int)MalcolmLegacy.CSCTEnableAudioEndpoint(AEndpointID, true);
        }

        public static string GetEndpointIDStateStr(string AEndpointID)
        {
            MalcolmLegacy.CTAUDENDPOINTSTATE pCTAudEndpointState = MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unknown;
            if (MalcolmLegacy.CSCTGetAudioEndpointState(AEndpointID, ref pCTAudEndpointState) != MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success || (pCTAudEndpointState == MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unknown || pCTAudEndpointState == MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Active))
                return "";
            string str = "";
            if ((pCTAudEndpointState & MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_NotPresent) == MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_NotPresent)
                str = MalcolmUtil.GetResourceString("resStrCurrentlyUnavailable");
            if ((pCTAudEndpointState & MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Disabled) == MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Disabled)
                str = !(str == "") ? str + ", " + MalcolmUtil.GetResourceString("resStrDisabled").ToLower() : MalcolmUtil.GetResourceString("resStrDisabled");
            if ((pCTAudEndpointState & MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unplugged) == MalcolmLegacy.CTAUDENDPOINTSTATE.CTAUDENDPOINTSTATE_Unplugged)
                str = !(str == "") ? str + ", " + MalcolmUtil.GetResourceString("resStrUnplugged").ToLower() : MalcolmUtil.GetResourceString("resStrUnplugged");
            if (str != "")
                return " (" + str + ")";
            return "";
        }

        public static void ShowEncoderErrMessage(MalcolmLegacy.CTAUDENDPOINTRESULT ACTAudEndpointResult, string AEndpointID, string AEncodingGuid)
        {
            string messageBoxText;
            switch (ACTAudEndpointResult)
            {
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_CoInitializeFailed:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_CoInitializeHasNotBeenCalled:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_UnknownError:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_BufferTooSmall:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_BadParam:
                    Util.PrintDebug("Encoder Error: " + ACTAudEndpointResult.ToString());
                    return;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_CertificateSignatureCannotBeVerified:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_LicenseCannotBeVerified:
                    string encoderName = MalLgcyLib.GetEncoderName(AEncodingGuid);
                    messageBoxText = !(encoderName != "") ? MalcolmUtil.GetResourceString("resStrErrLicenseInvalid") : string.Format(MalcolmUtil.GetResourceString("resStrErrNoLicense"), (object)encoderName);
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_UnsupportedCPUBecauseNoSSE2:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrDDLNotSupported");
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_UnsupportedAudioEffect:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_UnsupportedAudioFormat:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrFormatNotSupported");
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_AudioServiceNotRunning:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrSrvNotRunning");
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_FunctionNotImplementedInAudioService:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrNotCompatible");
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_Success:
                    return;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_OperationFailed:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_CannotPerformRegistryOperations:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrOperationFail");
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_AccessDenied:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_ExclusiveModeNotAllowed:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrExclusiveMode");
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_OutOfMemory:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrNoMemory");
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_AudioDeviceInUse:
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_AudioDeviceCannotBeCreated:
                    messageBoxText = string.Format(MalcolmUtil.GetResourceString("resStrErrDeviceInUsed"), (object)MalLgcyLib.GetEndpointFullName(AEndpointID));
                    break;
                case MalcolmLegacy.CTAUDENDPOINTRESULT.CTAUDENDPOINTRESULT_AudioDeviceInvalidated:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrDeviceRemoved");
                    break;
                default:
                    messageBoxText = MalcolmUtil.GetResourceString("resStrErrOperationFail");
                    break;
            }
            int num = (int)MessageBox.Show(messageBoxText, App.AppID);
        }

        private static string GetEncoderName(string AEncodingGuid)
        {
            if (AEncodingGuid == MalcolmLegacy.CTAUDENCODING_DolbyDigitalLive)
                return MalcolmUtil.GetResourceString("resStrDolbyDigitalLive");
            if (AEncodingGuid == MalcolmLegacy.CTAUDENCODING_DTSInteractive)
                return MalcolmUtil.GetResourceString("resStrDTSConnect");
            return "";
        }

        public enum AudioEndpointType
        {
            PlaybackDevice = 1,
            RecordingDevice = 2,
        }
    }
}
