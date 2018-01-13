using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Menu.Settings
{
	public class SettingsGraphics: MonoBehaviour
	{
		public int fullScreen;
		public int resolution;
		public int quality;
		public int vSync;
		public int antiAliasing;
		public int tripleBuffering;
		public int anisotrophicFiltering;

		[SerializeField]private Toggle fullScreenValue;
		[SerializeField]private Toggle vSyncValue;
		[SerializeField]private Toggle tripleBufferingValue;
		[SerializeField]private Toggle anisotrophicFilteringValue;
		[SerializeField]private Dropdown resolutionList;
		[SerializeField]private Text qualityValue;
		[SerializeField]private Text antiAliasingValue;

		public List <Resolution> resolutions = new List<Resolution> ();

		void OnEnable ()
		{
			LoadPrefs ();
		}

		public void DefaultPrefs ()
		{
			PlayerPrefs.SetInt ("fullScreen", 1);
			PlayerPrefs.SetInt ("resolution", 0);
			PlayerPrefs.SetInt ("quality", 5);
			PlayerPrefs.SetInt ("vSync", 1);
			PlayerPrefs.SetInt ("antiAliasing", 8);
			PlayerPrefs.SetInt ("tripleBuffering", 1);
			PlayerPrefs.SetInt ("anisotrophicFiltering", 1);

			LoadPrefs ();
		}

		public void LoadPrefs ()
		{
			LoadResolution ();

			fullScreen = PlayerPrefs.GetInt ("fullScreen");
			vSync = PlayerPrefs.GetInt ("vSync");
			tripleBuffering = PlayerPrefs.GetInt ("tripleBuffering");
			anisotrophicFiltering = PlayerPrefs.GetInt ("anisotrophicFiltering");
			resolution = PlayerPrefs.GetInt ("resolution");
			quality = PlayerPrefs.GetInt ("quality");
			antiAliasing = PlayerPrefs.GetInt ("antiAliasing");


			fullScreenValue.isOn = fullScreen == 1 ? true : false;
			vSyncValue.isOn = vSync == 1 ? true : false;
			tripleBufferingValue.isOn = tripleBuffering == 1 ? true : false;
			anisotrophicFilteringValue.isOn = anisotrophicFiltering == 1 ? true : false;
			resolutionList.value = resolution;
			qualityValue.text = "Quality: " + QualitySettings.names [quality];
			antiAliasingValue.text = "Antialiasing: x" + antiAliasing;

		}

		public void SavePrefs ()
		{
			PlayerPrefs.SetInt ("fullScreen", fullScreen);
			PlayerPrefs.SetInt ("vSync", vSync);
			PlayerPrefs.SetInt ("tripleBuffering", tripleBuffering);
			PlayerPrefs.SetInt ("anisotrophicFiltering", anisotrophicFiltering);
			PlayerPrefs.SetInt ("resolution", resolution);
			PlayerPrefs.SetInt ("quality", quality);
			PlayerPrefs.SetInt ("antiAliasing", antiAliasing);

			ApplyPrefs ();
		}

		public void ApplyPrefs ()
		{
			LoadPrefs ();
			Screen.fullScreen = fullScreen == 1 ? true : false;
			QualitySettings.vSyncCount = vSync;
			QualitySettings.maxQueuedFrames = tripleBuffering == 1 ? 3 : 0;
			QualitySettings.anisotropicFiltering = anisotrophicFiltering == 1 ? AnisotropicFiltering.Enable : AnisotropicFiltering.Disable;
			Screen.SetResolution (resolutions [resolution].width, resolutions [resolution].height, fullScreen == 1 ? true : false);
			QualitySettings.SetQualityLevel (quality);
			QualitySettings.antiAliasing = antiAliasing;
		}

		private void LoadResolution ()
		{
			resolutions.Clear ();
			foreach (Resolution res in Screen.resolutions) {
				if (!resolutions.Contains (res)) {
					resolutions.Add (res);
				}
			}
			resolutions.Reverse ();
			resolutionList.ClearOptions ();
			resolutionList.AddOptions (resolutions.ConvertAll (x => (string)x.ToString ()));
		}


		public void SetFullScreen (bool state)
		{
			fullScreen = state == false ? 0 : 1;
		}

		public void SetVSync (bool state)
		{
			vSync = state == false ? 0 : 1;
		}

		public void SetTripleBuffering (bool state)
		{
			tripleBuffering = state == false ? 0 : 1;
		}

		public void SetAnisotrophicFiltering (bool state)
		{
			anisotrophicFiltering = state == false ? 0 : 1;
		}

		public void SetResolution (int value)//uses 'resolutionDimension' instead of resX/resY
		{
			resolution = value;
		}

		public void SetQuality (int value)
		{
			if (value == 1)
				quality++;
			if (value == -1)
				quality--;
			
			quality = Mathf.Clamp (quality, 0, QualitySettings.names.Length - 1);
			qualityValue.text = "Quality: " + QualitySettings.names [quality];
		}

		public void SetAntiAliasing (int value)
		{
			if (value == 1)
				antiAliasing += 2;
			if (value == -1)
				antiAliasing -= 2;
			antiAliasing = Mathf.Clamp (antiAliasing, 0, 8);
			antiAliasingValue.text = "Antialiasing: x" + antiAliasing;
		}
	}
}
