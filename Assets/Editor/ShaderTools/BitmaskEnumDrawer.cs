using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class BitmaskEnumDrawer : MaterialPropertyDrawer
{
	public static Type[] GetTypesFromAssembly(Assembly assembly)
	{
		if (assembly == null)
		{
			return new Type[0];
		}

		Type[] result;
		try
		{
			result = assembly.GetTypes();
		}
		catch (ReflectionTypeLoadException)
		{
			result = new Type[0];
		}

		return result;
	}

	private readonly string[] names;

	public BitmaskEnumDrawer(string enumName)
	{
		Type[] source = AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly x) => GetTypesFromAssembly(x)).ToArray<Type>();
		try
		{
			Type enumType = source.FirstOrDefault((Type x) => x.IsSubclassOf(typeof(Enum)) && (x.Name == enumName || x.FullName == enumName));
			string[] array = Enum.GetNames(enumType);
			names = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				names[i] = array[i];
			}
		}
		catch (Exception)
		{
			Debug.LogWarningFormat(
				"Failed to create MaterialEnum, enum {0} not found",
				new object[]
				{
					enumName
				}
			);
			throw;
		}
	}

	public BitmaskEnumDrawer(string enumName, string exclude1)
		: this(enumName)
	{
		List<string> n = new List<string>(names);

		n.Remove(exclude1);

		names = n.ToArray();
	}

	public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
	{
		EditorGUI.BeginChangeCheck();
		EditorGUI.showMixedValue = prop.hasMixedValue;

		int num = Mathf.RoundToInt(prop.floatValue);

		num = EditorGUI.MaskField(position, label, num, names);

		// DAFAQU is this??? when you select everything negative values are returned -.-
		if (num < 0) num = Mathf.RoundToInt(Mathf.Pow(2, names.Length)) + num;

		EditorGUI.showMixedValue = false;
		if (EditorGUI.EndChangeCheck())
		{
			prop.floatValue = (float)num;
		}
	}
}