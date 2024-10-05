using UnityEngine;
using UnityEditor;

public class HideWhenOnDrawer : MaterialPropertyDrawer
{
    private string parameter;
    private float setTo = 1;

    public HideWhenOnDrawer(string parameter)
    {
        this.parameter = parameter;
    }

    public HideWhenOnDrawer(string parameter, float value)
    {
        this.parameter = parameter;
        setTo = value;
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        Material targetMat = editor.target as Material;

        if (!targetMat.HasProperty(parameter) || targetMat.GetFloat(parameter) == setTo) return 0;

        if (prop.type == MaterialProperty.PropType.Vector)
        {
            return 32;
        }
        else if (prop.type == MaterialProperty.PropType.Texture)
        {
            return 64;
        }

        return 16f;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        Material targetMat = editor.target as Material;

        if (!targetMat.HasProperty(parameter) || targetMat.GetFloat(parameter) == setTo) return;

        if (prop.type == MaterialProperty.PropType.Texture) editor.TextureProperty(position, prop, label, false);
        else if (prop.type == MaterialProperty.PropType.Vector) editor.VectorProperty(position, prop, label);
        else if (prop.type == MaterialProperty.PropType.Range) editor.RangeProperty(position, prop, label);
        else if (prop.type == MaterialProperty.PropType.Float) editor.FloatProperty(position, prop, label);
        else if (prop.type == MaterialProperty.PropType.Color) editor.ColorProperty(position, prop, label);
        else if (prop.type == MaterialProperty.PropType.Int) editor.IntegerProperty(position, prop, label);
    }
}