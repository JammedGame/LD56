﻿using UnityEngine;
using UnityEditor;

public class VectorTwoDrawer : MaterialPropertyDrawer
{
	public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
	{
		if (prop.type == MaterialProperty.PropType.Vector)
		{
			EditorGUIUtility.labelWidth = 0f;
			EditorGUIUtility.fieldWidth = 0f;

			if (!EditorGUIUtility.wideMode)
			{
				EditorGUIUtility.wideMode = true;
				EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 212;
			}

			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = prop.hasMixedValue;
			position.width = position.width / 100 * 85f;
			Vector4 vec = EditorGUI.Vector2Field(position, label, prop.vectorValue);
			if (EditorGUI.EndChangeCheck())
			{
				prop.vectorValue = vec;
			}
		}
		else
		{
			editor.DefaultShaderProperty(prop, label.text);
		}
	}
}