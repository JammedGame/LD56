using UnityEngine;
using UnityEditor;

public class FeatureEnumDrawer : MaterialPropertyDrawer
{
	public string[] keywords;

	public FeatureEnumDrawer(string s1)
	{
		keywords = new[] { s1 };
	}

	public FeatureEnumDrawer(string s1, string s2)
	{
		keywords = new[] { s1, s2 };
	}

	public FeatureEnumDrawer(string s1, string s2, string s3)
	{
		keywords = new[] { s1, s2, s3 };
	}

	public FeatureEnumDrawer(string s1, string s2, string s3, string s4)
	{
		keywords = new[] { s1, s2, s3, s4 };
	}

	public FeatureEnumDrawer(string s1, string s2, string s3, string s4, string s5)
	{
		keywords = new[] { s1, s2, s3, s4, s5 };
	}

	public FeatureEnumDrawer(string s1, string s2, string s3, string s4, string s5, string s6)
	{
		keywords = new[] { s1, s2, s3, s4, s5, s6 };
	}

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		Material targetMat = editor.target as Material;

		int index = (int)prop.floatValue;
		EditorGUI.BeginChangeCheck();
		index = EditorGUI.Popup(position, label, index, keywords);
		prop.floatValue = index;

		for (int i = 0; i < keywords.Length; i++)
		{
			string keyword = prop.name + "_" + keywords[i];
			if (i == index)
			{
				targetMat.EnableKeyword(keyword);
			}
			else
			{
				targetMat.DisableKeyword(keyword);
			}
		}
	}
}