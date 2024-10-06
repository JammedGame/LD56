﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class PropertyDrawerUtil
{
	/// <summary>
	/// Gets value represented by this SerializedProperty.
	/// </summary>
	public static T GetValue<T>(this SerializedProperty property)
	{
		var nodes = property.ParsePath();

		object value = property.serializedObject.targetObject;
		T returnValue = value is T ? (T)value : default(T);

		foreach (var node in nodes)
		{
			IList list;
			value = node.GetValue(value, out list);

			// check if this is maybe the main return value.
			if (value is T) { returnValue = value is T ? (T)value : default(T); }
			else if (list is T) { returnValue = (T) list; }
		}

		return returnValue;
	}

	/// <summary>
	/// Sets new value to the field or array element represented by this SerializedProperty.
	/// </summary>
	public static void SetValue<T>(this SerializedProperty property, T newValue)
	{
		var nodes = property.ParsePath();
		object value = property.serializedObject.targetObject;

		for (int i = 0; i < nodes.Count - 1; i++)
		{
			IList list;
			value = nodes[i].GetValue(value, out list);
		}

		var lastNode = nodes[nodes.Count - 1];
		lastNode.SetValue(value, newValue);
	}

	/// <summary>
	/// Turns property path into a list of nodes.
	/// </summary>
	public static List<Node> ParsePath(this SerializedProperty property)
	{
		List<Node> nodes;
		var path = property.propertyPath;
		if (!nodeCache.TryGetValue(path, out nodes))
		{
			nodes = new List<Node>();
			foreach(var n in path.Replace(".Array.data[", "[").Split('.'))
			{
				nodes.Add(new Node(n));
			}
			
			nodeCache.Add(path, nodes);
		}
		return nodes;
	}

	/// <summary>
	/// PropertyPaths are strings, cache list of nodes after parsing.
	/// </summary>
	private static readonly Dictionary<string, List<Node>> nodeCache = new Dictionary<string, List<Node>>();

	/// <summary>
	/// Node of SerializedProperty path. Represetns one nested field or array element.
	/// </summary>
	public class Node
	{
		public string fieldName;
		public int arrayIndex;

		public Node(string path)
		{
			fieldName = path;

			// check if node represents list element
			arrayIndex = -1;
			var arrayIndexStart = path.IndexOf('[');
			if (arrayIndexStart != -1)
			{
				arrayIndex = Int32.Parse(path.Substring(arrayIndexStart + 1, path.Length - arrayIndexStart - 2));
				fieldName = fieldName.Substring(0, arrayIndexStart);
			}
		}

		public object GetValue(object value, out IList list)
		{
			value = value.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(value);

			list = value as IList;
			return list != null ? list[arrayIndex] : value;
		}

		public void SetValue<T>(object value, T newValue)
		{
			var field = value.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if (arrayIndex != -1)
			{
				var list = (IList<T>)field.GetValue(value);
				list[arrayIndex] = newValue;
			}
			else
			{
				field.SetValue(value, newValue);
			}
		}
	}
}