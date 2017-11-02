using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Menu.Settings
{
	public class SettingsGeneral : MonoBehaviour
	{
		public static int fov;

		[SerializeField]private Text fovValue;
		[SerializeField]private Slider fovSlider;

		void OnEnable ()
		{
			LoadPrefs ();
		}


		public void DefaultPrefs ()
		{
			PlayerPrefs.SetInt ("fov", 75);
			LoadPrefs ();
		}

		public void LoadPrefs ()
		{
			setFOV (PlayerPrefs.GetInt ("fov"));
		}

		public void SavePrefs ()
		{
			PlayerPrefs.SetInt ("fov", fov);
		}

		public void setFOV (float value)
		{
			fov = (int)value;
			fovSlider.value = value;
			fovValue.text = value.ToString ();
			/*if (FindObjectOfType<GameScene.MainPlayer.Player> ())
				FindObjectOfType<GameScene.MainPlayer.Player> ().camera.fieldOfView = fov;*/

		}
	}
}
