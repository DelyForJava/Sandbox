using UnityEngine;
using System.Collections;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class GameConfig : MonoBehaviour
{
	public enum ScreenRatio
	{
		None,
		_1610,
		_169,
		_43,
		_32,
	}
		
	static ScreenRatio _Ratio = ScreenRatio.None;
	static bool IsLowDevices = false;

	static public ScreenRatio GetScreenRatio ()
	{
		if (_Ratio != ScreenRatio.None)
			return _Ratio;

		float ratio = (float)Screen.width / Screen.height;

		if (Mathf.Abs (ratio - (4 / 3f)) < 0.05f)
			_Ratio = ScreenRatio._43;
		else if (Mathf.Abs (ratio - (3 / 2f)) < 0.05f)
			_Ratio = ScreenRatio._32;
		else if (Mathf.Abs (ratio - (16 / 9f)) < 0.05f)
			_Ratio = ScreenRatio._169;
		else if (Mathf.Abs (ratio - (16 / 10f)) < 0.05f)
			_Ratio = ScreenRatio._1610;
		else
			_Ratio = ScreenRatio._169;

		return _Ratio;
	}
		
	static public void SetQualitySetting ()
	{
#if UNITY_IOS
		DeviceGeneration iOSGen = Device.generation;

		switch (iOSGen)
		{
		// ipad
		case DeviceGeneration.iPad1Gen:
		case DeviceGeneration.iPad2Gen:
		case DeviceGeneration.iPad3Gen:
		case DeviceGeneration.iPad4Gen:
		case DeviceGeneration.iPadAir1:
		case DeviceGeneration.iPadAir2:
		case DeviceGeneration.iPadMini1Gen:
		case DeviceGeneration.iPadMini2Gen:
		case DeviceGeneration.iPadMini3Gen:
		case DeviceGeneration.iPadMini4Gen:
		case DeviceGeneration.iPadPro1Gen:
		case DeviceGeneration.iPadUnknown:

		// iphone
		case DeviceGeneration.iPhone:
		case DeviceGeneration.iPhone3G:
		case DeviceGeneration.iPhone3GS:
		case DeviceGeneration.iPhone4:
		case DeviceGeneration.iPhone4S:
		case DeviceGeneration.iPhone5:
		case DeviceGeneration.iPhone5C:
		case DeviceGeneration.iPhone5S:
		case DeviceGeneration.iPhone6:
		case DeviceGeneration.iPhone6Plus:
		case DeviceGeneration.iPhone6S:
		case DeviceGeneration.iPhone6SPlus:
		case DeviceGeneration.iPhoneUnknown:
		// touch
		case DeviceGeneration.iPodTouch1Gen:
		case DeviceGeneration.iPodTouch2Gen:
		case DeviceGeneration.iPodTouch3Gen:
		case DeviceGeneration.iPodTouch4Gen:
		case DeviceGeneration.iPodTouch5Gen:
		case DeviceGeneration.iPodTouchUnknown:

		// unknow
		case DeviceGeneration.Unknown:
		IsLowDevices = false;
		break;
		}
#elif UNITY_ANDROID
	
#endif
		
//		QualitySettings.SetQualityLevel (IsLowDevices ? 0 : 1);
	}

//	[RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
	static public void DeviceInfo ()
	{
		Debug.LogError ("deviceModel : " + SystemInfo.deviceModel);
		Debug.LogError ("deviceType : " + SystemInfo.deviceType);
		Debug.LogError ("deviceMuid : " + SystemInfo.deviceUniqueIdentifier);
		Debug.LogError ("graphicsDeviceName : " + SystemInfo.graphicsDeviceName);
		Debug.LogError ("graphicsDeviceType : " + SystemInfo.graphicsDeviceType);
		Debug.LogError ("graphicsDeviceID : " + SystemInfo.graphicsDeviceID);
		Debug.LogError ("graphicsDeviceVendor : " + SystemInfo.graphicsDeviceVendor);
		Debug.LogError ("systemMemorySize : " + SystemInfo.systemMemorySize);
		Debug.LogError ("graphicsMemorySize : " + SystemInfo.graphicsMemorySize);
		Debug.LogError ("processorCount : " + SystemInfo.processorCount);
		Debug.LogError ("dpi : " + Screen.dpi);
		Debug.LogError ("orientation : " + Input.deviceOrientation.ToString ());
	}
}