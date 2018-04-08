using UnityEngine;

namespace Util
{
	public static class LerpType
	{
		/// <summary>
		/// Ramps when approaching 1
		/// </summary>
		public static float Sinerp (float t)
		{
			return Mathf.Sin (t * Mathf.PI * 0.5f);
		}

		/// <summary>
		/// Ramps when approaching 0
		/// </summary>
		public static float Coserp (float t)
		{
			return Mathf.Cos (t * Mathf.PI * 0.5f);
		}

		/// <summary>
		/// Steep movement
		/// </summary>
		public static float Quadratic (float t)
		{
			return t * t;
		}

		/// <summary>
		/// Ramps when approaching 0 and 1
		/// </summary>
		public static float SmoothStep (float t)
		{
			return t * t * (3f - 2f * t);
		}

		/// <summary>
		/// Heavily ramps when approaching 0 and 1
		/// Lots of calculation, use sparingly
		/// </summary>
		public static float SmootherStep (float t)
		{
			return t * t * t * (t * (6 * t * t - 15 * t) + 10 * t);
		}

	}
}
