using UnityEngine;

namespace Util
{
	public static class Sound
	{
		public static void SetVolume (this AudioSource audio, float volume)
		{
			audio.volume = volume;
		}
	}
}
