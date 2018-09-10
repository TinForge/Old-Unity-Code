using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinForge.SceneryGenerator {
	[System.Serializable]
	public class SceneryPrefab {
		public GameObject prefab;

		public int amount;
		public bool alignToGround;
		public bool randomRotation;
		public float minTilt;
		public float maxTilt;
		public bool randomScale;
		public float minScale;
		public float maxScale;
		public float verticalOffset;

		public Vector3 Bounds { get { return prefab.GetComponentInChildren<Renderer> ().bounds.size; } }
		public float Volume { get { return Bounds.x * Bounds.y * Bounds.z; } }

		public SceneryPrefab () {
			prefab = null;
			amount = 25;
			alignToGround = true;
			randomRotation = true;
			minTilt = SceneryGenerator.minPrefabTilt;
			maxTilt = SceneryGenerator.maxPrefabTilt;
			randomScale = true;
			minScale = SceneryGenerator.minPrefabScale;
			maxScale = SceneryGenerator.maxPrefabScale;
			verticalOffset = 0;
		}
	}

	public class SceneryGenerator : MonoBehaviour {

		//Min Max Values
		public const int minPrefabTilt = 0;
		public const int maxPrefabTilt = 90;
		public const float minPrefabScale = 1f;
		public const float maxPrefabScale = 5f;
		public const float maxPrefabVertical = 2f;

		//Random Placement Configurations
		public bool raycastAll;
		public Transform terrain;
		public bool sortSize;
		public bool overwrite;
		public bool autogen;

		public bool manualSeed;
		public int randomSeed;

		//public int maxTangent;
		public int raycastHeight;
		public int radius;

		//Prefabs and properties
		public int prefabCount;
		public SceneryPrefab[] prefabs;

		//-----------------------

		public void Generate () {
			if (overwrite || autogen)
				RemoveAll ();

			if (sortSize)
				SortSizePrefabs ();

			for (int i = 0; i < prefabs.Length; i++) {
				if (manualSeed)
					Random.InitState (randomSeed + i); //Reset seed for consistency between prefab setting changes

				for (int j = 0; j < prefabs[i].amount; j++) {
					Vector3 pos;
					Quaternion rot;
					RaycastPlacement (prefabs[i].alignToGround, out pos, out rot);
					if (pos == Vector3.zero)
						continue;

					Transform obj = PlaceScenery (prefabs[i], pos, rot);
					if (prefabs[i].randomRotation)
						RandomRotation (obj);
					if (prefabs[i].randomRotation)
						RandomTilt (obj, prefabs[i].minTilt, prefabs[i].maxTilt);
					if (prefabs[i].randomScale)
						RandomScale (obj, prefabs[i].minScale, prefabs[i].maxScale, prefabs[i].verticalOffset);
				}
			}

		}

		public void RemoveAll () {
			for (int i = transform.childCount - 1; i >= 0; i--)
				DestroyImmediate (transform.GetChild (i).gameObject);
		}

		public void Remove (Transform group) {
			DestroyImmediate (group.gameObject);
		}

		public void SortSizePrefabs () {
			for (int i = 0; i < prefabs.Length; i++)
				for (int j = 0; j < prefabs.Length; j++) {
					if (prefabs[i].Volume < prefabs[j].Volume) {
						SceneryPrefab temp = prefabs[i];
						prefabs[i] = prefabs[j];
						prefabs[j] = temp;
					}
				}
		}

		public void RaycastPlacement (bool align, out Vector3 pos, out Quaternion rot) {
			Vector3 origin = transform.position + Vector3.up * raycastHeight;
			origin = origin + new Vector3 (Random.Range (-radius, radius), 0, Random.Range (-radius, radius));

			pos = Vector3.zero;
			rot = Quaternion.identity;
			RaycastHit hit;

			if (Physics.Raycast (new Ray (origin, Vector3.down), out hit, raycastHeight)) {
				if (!raycastAll && hit.transform != terrain)
					return;

				pos = hit.point;
				if (align)
					rot = Quaternion.FromToRotation (Vector3.up, hit.normal);
			}
		}

		public Transform PlaceScenery (SceneryPrefab obj, Vector3 pos, Quaternion rot) {
			Transform parent = transform.Find (obj.prefab.name);
			if (parent == null) {
				parent = new GameObject ().transform;
				parent.name = obj.prefab.name;
				parent.transform.parent = transform;
			}
			return Instantiate (obj.prefab, pos, rot, parent).transform;
		}

		public void RandomRotation (Transform obj) {
			obj.Rotate (Vector3.up * Random.Range (0, 360));
		}

		public void RandomTilt (Transform obj, float min, float max) {
			obj.Rotate (Vector3.forward * Random.Range (min, max));
		}

		public void RandomScale (Transform obj, float min, float max, float vRange) {
			float s = Random.Range (min, max);
			obj.localScale = new Vector3 (s, s + Random.Range (0, vRange), s);
		}

	}
}