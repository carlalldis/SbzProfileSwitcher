// Decompiled with JetBrains decompiler
// Type: CTGetPdtLibrary
// Assembly: SBZ, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null
// MVID: F576CC9B-1B1F-4112-BD5C-14BC70FE47C8
// Assembly location: C:\Program Files (x86)\Creative\Sound Blaster Z-Series\Sound Blaster Z-Series Control Panel\SBZ.exe

using System.Runtime.InteropServices;

internal class CTGetPdtLibrary
{
    public static ulong CTSSF_None = 0;
    public static ulong CTSSF_SBRecon3DControlPanel = 1;
    public static ulong CTSSF_SBRecon3DiControlPanel = 2;
    public static ulong CTSSF_SBRecon3DPCIeControlPanel = 4;
    public static ulong CTSSF_SBXFiMB3ControlPanel = 8;
    public static ulong CTSSF_THXTruStudioControlPanel = 16;
    public static ulong CTSSF_THXTruStudioProControlPanel = 32;
    public static ulong CTSSF_SBTactic3DControlPanel = 64;
    public static ulong CTSSF_SBMORBControlPanel = 128;
    public static ulong CTSSF_SBAxxControlPanel = 256;
    public static ulong CTSSF_SBZSeriesControlPanel = 512;
    public static ulong CTSSF_SBRecon3DSBXControlPanel = 1024;
    public static ulong CTSSF_SBRecon3DiSBXControlPanel = 2048;
    public static ulong CTSSF_SBRecon3DPCIeSBXControlPanel = 4096;
    public static ulong CTSSF_SBZetaSeriesControlPanel = 8192;
    public static ulong CTSSF_SBAudigyFXControlPanel = 16384;
    public static ulong CTSSF_SBU2ControlPanel = 32768;
    public static ulong CTSSF_SBUoKeControlPanel = 65536;
    public static ulong CTSSF_SBCinema2ControlPanel = 131072;
    public static ulong CTSSF_SBC4ControlPanel = 262144;
    public static ulong CTSSF_SBACESeriesControlPanel = 524288;
    public static ulong CTSSF_SBESeriesControlPanel = 1048576;
    public static ulong CTSSF_SBRSeriesControlPanel = 2097152;
    public static ulong CTSSF_SBX7ControlPanel = 4194304;
    public static ulong CTSSF_All = ulong.MaxValue;

    [DllImport("CTGetPdt.dll", CharSet = CharSet.Unicode)]
    public static extern int CTGetSupportedProducts(ulong ddwSupportedSoftwareFlag, ulong ddwReserved1, ulong ddwReserved2, ulong ddwReserved3, ref MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection);

    [DllImport("CTGetPdt.dll", CharSet = CharSet.Unicode)]
    public static extern int CTReleaseSupportedProducts(ref MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection);

    [DllImport("CTGetPdt.dll", CharSet = CharSet.Unicode)]
    public static extern int CTOutputDebugStringProductIDInfoCollection(ref MalcolmLegacy.CTPRODUCTIDINFOCOLLECTION pCTProductIDInfoCollection);
}
