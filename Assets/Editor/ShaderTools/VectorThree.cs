using UnityEngine;
using UnityEditor;

public class VectorThreeDrawer : MaterialPropertyDrawer
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
			Vector4 vec = EditorGUI.Vector3Field(position, label, prop.vectorValue);
			EditorGUILayout.IntSlider(label, 2, 0, 8);
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