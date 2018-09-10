using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TinForge.SceneryGenerator {

	[CustomEditor (typeof (SceneryGenerator))]
	public class SceneryGeneratorEditor : Editor {
		SerializedProperty raycastAllProp;
		SerializedProperty terrainProp;
		SerializedProperty sortSizeProp;
		SerializedProperty overwriteProp;
		SerializedProperty autogenProp;

		SerializedProperty manualSeedProp;
		SerializedProperty randomSeedProp;

		SerializedProperty raycastHeightProp;
		SerializedProperty radiusProp;

		SerializedProperty prefabCountProp;

		//
		SerializedProperty prefabProp;
		SerializedProperty amountProp;
		SerializedProperty alignToGroundProp;
		SerializedProperty randomRotationProp;
		SerializedProperty minTiltProp;
		SerializedProperty maxTiltProp;
		SerializedProperty randomScaleProp;
		SerializedProperty minScaleProp;
		SerializedProperty maxScaleProp;
		SerializedProperty verticalOffsetProp;
		//

		static bool PlacementFoldout = true;
		static bool PrefabsFoldout = true;
		static bool RandomFoldout = true;

		void OnEnable () {
			raycastAllProp = serializedObject.FindProperty ("raycastAll");
			terrainProp = serializedObject.FindProperty ("terrain");
			sortSizeProp = serializedObject.FindProperty ("sortSize");
			overwriteProp = serializedObject.FindProperty ("overwrite");
			autogenProp = serializedObject.FindProperty ("autogen");

			manualSeedProp = serializedObject.FindProperty ("manualSeed");
			randomSeedProp = serializedObject.FindProperty ("randomSeed");

			raycastHeightProp = serializedObject.FindProperty ("raycastHeight");
			radiusProp = serializedObject.FindProperty ("radius");

			prefabCountProp = serializedObject.FindProperty ("prefabCount");
			prefabsProp = serializedObject.FindProperty ("prefabs");
			//get array index and value
		}

		public override void OnInspectorGUI () {
			serializedObject.Update ();

			#region Placement Settings
			PlacementFoldout = EditorGUILayout.Foldout (PlacementFoldout, "Placement Settings", EditorStyles.boldLabel);
			if (PlacementFoldout) {
				EditorGUILayout.BeginVertical (EditorStyles.helpBox);
				EditorGUILayout.PropertyField (raycastAllProp, new GUIContent ("Place Anywhere", "Scenery prefabs will be generated on any raycasted surface. Recommend using a target mesh instead."));
				EditorGUI.indentLevel = 1;
				EditorGUILayout.BeginHorizontal (); //Encapsulate with disabledgroup
				if (!raycastAllProp.boolValue) EditorGUI.indentLevel = 1;
				EditorGUILayout.PropertyField (terrainProp, new GUIContent ("Target Mesh"));
				EditorGUI.indentLevel = 0;
				EditorGUILayout.EndHorizontal (); //
				EditorGUILayout.PropertyField (raycastHeightProp, new GUIContent ("Raycast Height", "Height should be higher than tallest point of terrain mesh"));
				EditorGUILayout.PropertyField (radiusProp, new GUIContent ("Radius"));
				GUILayout.Label (new GUIContent ("Position Scenery Generator under target mesh. Red plane should not be visible."), EditorStyles.miniLabel);
				EditorGUILayout.EndVertical ();
			}
			#endregion

			EditorGUILayout.Space ();

			#region Prefab Panel

			PrefabsFoldout = EditorGUILayout.Foldout (PrefabsFoldout, "Prefabs Settings", EditorStyles.boldLabel);
			if (PrefabsFoldout) {

				for (int i = 0; i < prefabCountProp.intValue; i++) {
					EditorGUILayout.BeginVertical (EditorStyles.helpBox);
					EditorGUILayout.BeginHorizontal ();

					EditorGUILayout.LabelField ("Scenery Prefab " + (i + 1), EditorStyles.boldLabel);
					if (GUILayout.Button ("Delete Prefab")) {
						prefabCountProp.intValue--;
						prefabsProp.DeleteArrayElementAtIndex (i);
					}
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.Space ();
					EditorGUI.indentLevel = 1;

					//there seems to be a problem with gui.changed recognizing properties in array?

					EditorGUILayout.PropertyField (prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("prefab"));
					EditorGUILayout.PropertyField (prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("amount"));
					EditorGUILayout.PropertyField (prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("alignToGround"));
					EditorGUILayout.PropertyField (prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("randomRotation"));
					if (prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("randomRotation").boolValue) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUI.indentLevel = 2;
						float minTilt = prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("minTilt").floatValue;
						float maxTilt = prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("maxTilt").floatValue;
						EditorGUILayout.PrefixLabel ("Min " + (int) minTilt + " Max " + (int) maxTilt);
						EditorGUI.indentLevel = 0;
						EditorGUILayout.MinMaxSlider (ref minTilt, ref maxTilt, SceneryGenerator.minPrefabTilt, SceneryGenerator.maxPrefabTilt);
						EditorGUI.indentLevel = 1;
						prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("minTilt").floatValue = minTilt;
						prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("maxTilt").floatValue = maxTilt;
						EditorGUILayout.EndHorizontal ();
					}
					EditorGUILayout.PropertyField (prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("randomScale"));
					if (prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("randomScale").boolValue) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUI.indentLevel = 2;
						float minScale = prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("minScale").floatValue;
						float maxScale = prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("maxScale").floatValue;
						EditorGUILayout.PrefixLabel ("Min " + minScale.ToString ("0.0") + " Max " + maxScale.ToString ("0.0"));
						EditorGUI.indentLevel = 0;
						EditorGUILayout.MinMaxSlider (ref minScale, ref maxScale, SceneryGenerator.minPrefabScale, SceneryGenerator.maxPrefabScale);
						EditorGUI.indentLevel = 1;
						prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("minScale").floatValue = minScale;
						prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("maxScale").floatValue = maxScale;
						EditorGUILayout.EndHorizontal ();
						EditorGUI.indentLevel = 2;
						EditorGUILayout.Slider (prefabsProp.GetArrayElementAtIndex (i).FindPropertyRelative ("verticalOffset"), 0, SceneryGenerator.maxPrefabVertical);
					}
					EditorGUI.indentLevel = 0;
					EditorGUILayout.EndVertical ();

					EditorGUILayout.Space ();
				}

				if (GUILayout.Button ("Add Prefab")) {
					prefabCountProp.intValue++;
					prefabsProp.arraySize++;
				}
			}
			#endregion

			EditorGUILayout.Space ();

			#region Random Settings
			RandomFoldout = EditorGUILayout.Foldout (RandomFoldout, "Random Settings", EditorStyles.boldLabel);
			if (PrefabsFoldout) {
				EditorGUILayout.BeginVertical (EditorStyles.helpBox);

				EditorGUILayout.PropertyField (manualSeedProp, new GUIContent ("Use manual seed"));
				if (manualSeedProp.boolValue) {
					EditorGUI.indentLevel = 1;
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.PropertyField (randomSeedProp);
					if (GUILayout.Button ("Randomise Seed"))
						randomSeedProp.intValue = Random.Range (0, 9999);
					EditorGUI.indentLevel = 0;
					EditorGUILayout.EndHorizontal ();
				}
				EditorGUILayout.EndVertical ();
			}
			#endregion

			EditorGUILayout.Space ();

			#region Control Panel
			GUILayout.Label (new GUIContent ("Control Panel"), EditorStyles.boldLabel);

			EditorGUILayout.Space ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.BeginVertical (EditorStyles.helpBox);

			EditorGUILayout.PropertyField (sortSizeProp, new GUIContent ("Sort prefabs by size"));
			//EditorGUILayout.PropertyField(overwriteProp, new GUIContent("Overwrite generated"));
			EditorGUILayout.PropertyField (autogenProp, new GUIContent ("Auto generate"));

			EditorGUILayout.EndVertical ();
			#endregion

			EditorGUILayout.Space ();

			#region Buttons
			EditorGUILayout.BeginVertical (EditorStyles.helpBox);
			EditorGUI.BeginDisabledGroup (autogenProp.boolValue);
			if (GUILayout.Button ("Generate"))
				Selection.activeTransform.GetComponent<SceneryGenerator> ().Generate ();

			if (GUILayout.Button ("Remove All"))
				Selection.activeTransform.GetComponent<SceneryGenerator> ().RemoveAll ();
			EditorGUI.EndDisabledGroup ();

			EditorGUILayout.EndVertical ();
			EditorGUILayout.EndHorizontal ();

			#endregion

			EditorGUILayout.Space ();

			#region Info

			GUILayout.Label (new GUIContent ("About"), EditorStyles.boldLabel);
			EditorGUILayout.BeginVertical (EditorStyles.helpBox);
			GUILayout.Label (new GUIContent ("A useful tool for quickly generating randomised scenery during editing. Put in prefabs, adjust settings, and generate! Uses raycasting and random range for placement. Childs and sorts objects by prefabs."), EditorStyles.wordWrappedLabel);

			GUILayout.Label (new GUIContent ("*Detach children from Scenery Generator when happy with results"), EditorStyles.wordWrappedMiniLabel);
			GUILayout.Label (new GUIContent ("*Prefabs get generated in numerical order. Largest object should be placed first. Ex. grass should go last."), EditorStyles.wordWrappedMiniLabel);
			GUILayout.Label (new GUIContent ("*Clipping is still possible as raycast do not check for thickness."), EditorStyles.wordWrappedMiniLabel);

			GUILayout.Label (new GUIContent ("v1.0 By TinForge 2018"), EditorStyles.centeredGreyMiniLabel);
			EditorGUILayout.EndVertical ();
			#endregion

			EditorGUILayout.Space ();

			if (GUI.changed)
				if (autogenProp.boolValue && overwriteProp.boolValue)
					Selection.activeTransform.GetComponent<SceneryGenerator> ().Generate ();

			serializedObject.ApplyModifiedProperties ();
		}

		public void OnSceneGUI () {
			SceneryGenerator sg = target as SceneryGenerator;
			Handles.color = Color.blue;
			Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
			Handles.DrawLine (sg.transform.position, sg.transform.position + Vector3.up * raycastHeightProp.intValue);

			Color red = Color.red;
			red.a = 0.35f;
			Handles.color = red;
			Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
			Vector3[] verts = new [] {
				new Vector3 (sg.transform.position.x - radiusProp.intValue, sg.transform.position.y, sg.transform.position.z - radiusProp.intValue),
					new Vector3 (sg.transform.position.x - radiusProp.intValue, sg.transform.position.y, sg.transform.position.z + radiusProp.intValue),
					new Vector3 (sg.transform.position.x + radiusProp.intValue, sg.transform.position.y, sg.transform.position.z + radiusProp.intValue),
					new Vector3 (sg.transform.position.x + radiusProp.intValue, sg.transform.position.y, sg.transform.position.z - radiusProp.intValue)
			};
			Handles.DrawSolidRectangleWithOutline (verts, red, red);

		}

	}
}