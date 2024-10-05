using UnityEditor;
using UnityEngine;

public class HideWhenOffDrawer : MaterialPropertyDrawer
{
	private readonly string parameter;
	private readonly float setTo;

	public HideWhenOffDrawer(string parameter)
	{
		this.parameter = parameter;
	}

	public HideWhenOffDrawer(string parameter, float value)
	{
		this.parameter = parameter;
		setTo = value;
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		var targetMat = editor.target as Material;

		if (!targetMat.HasProperty(parameter) || targetMat.GetFloat(parameter) == setTo) return 0;

		if (prop.type == MaterialProperty.PropType.Vector)
			return 32;
		if (prop.type == MaterialProperty.PropType.Texture) return 64;

		return 16f;
	}

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		var targetMat = editor.target as Material;

		if (!targetMat.HasProperty(parameter) || targetMat.GetFloat(parameter) == setTo) return;

		if (prop.type == MaterialProperty.PropType.Texture) editor.TextureProperty(position, prop, label);
		else if (prop.type == MaterialProperty.PropType.Vector) editor.VectorProperty(position, prop, label);
		else if (prop.type == MaterialProperty.PropType.Range) editor.RangeProperty(position, prop, label);
		else if (prop.type == MaterialProperty.PropType.Float) editor.FloatProperty(position, prop, label);
		else if (prop.type == MaterialProperty.PropType.Color) editor.ColorProperty(position, prop, label);
		else if (prop.type == MaterialProperty.PropType.Int) editor.IntegerProperty(position, prop, label);
	}
}