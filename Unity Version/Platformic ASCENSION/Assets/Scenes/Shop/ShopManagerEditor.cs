using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ShopManager))]
public class ShopManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		ShopManager sm = (ShopManager)target;
		if (sm == null ) { return; }

		if (GUILayout.Button("Reset Upgrade Variables"))
		{
			sm.ResetUpgradeVariables();
		}

		DrawDefaultInspector();
	}
}
#endif