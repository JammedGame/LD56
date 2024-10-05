using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ShaderGlobalVariablesController : EditorWindow
{
	private static ShaderGlobalVariablesController Instance { get; set; }

	private List<GameObject> objects;

	private GameObject go1;
	private GameObject go2;
	private GameObject go3;
	private GameObject go4;
	private GameObject go5;

	private float amount;
	private float vertexCount;
	private float darkUnitsAmount;
	private float shirtRandomColor;
	private GameObject meshRenderer;
	private bool deleteVertecies;

	[MenuItem("Art/CustomStuff/ShaderGlobalVariablesController")]
	private static void ShowWindow()
	{
		if (Instance)
		{
			Instance.Close();
			Instance = null;
		}
		else
		{
			Instance = GetWindow<ShaderGlobalVariablesController>("ShaderGlobalVariablesController");
			Instance.Show();
		}
	}

	private void OnEnable()
	{
		Instance = this;
	}

	private void OnGUI()
	{
		go1 = (GameObject)EditorGUILayout.ObjectField("Object1:", go1, typeof(GameObject), true);
		go2 = (GameObject)EditorGUILayout.ObjectField("Object2:", go2, typeof(GameObject), true);
		go3 = (GameObject)EditorGUILayout.ObjectField("Object3:", go3, typeof(GameObject), true);
		go4 = (GameObject)EditorGUILayout.ObjectField("Object4:", go4, typeof(GameObject), true);
		go5 = (GameObject)EditorGUILayout.ObjectField("Object5:", go5, typeof(GameObject), true);

		meshRenderer = (GameObject)EditorGUILayout.ObjectField("Mesh:", meshRenderer, typeof(GameObject), true);

		objects = new List<GameObject>
		{
			go1, go2, go3, go4, go5
		};

		//Global var
		amount = EditorGUILayout.Slider("crowdAmount", amount, 0f, 1f);
		Shader.SetGlobalFloat("Amount", amount);

		darkUnitsAmount = EditorGUILayout.Slider("darkUnitsAmount", darkUnitsAmount, 0f, 1);
		shirtRandomColor = EditorGUILayout.Slider("shirtRandomColor", shirtRandomColor, 0f, 1);

		vertexCount = EditorGUILayout.Slider("vertexCount", vertexCount, 0f, 1f);

		if (GUILayout.Button("Delete vert"))
		{
			// deleteVertecies = !deleteVertecies;
			var m = meshRenderer.GetComponent<MeshFilter>().sharedMesh;
			var vertecies = m.vertices.ToList();
			var triangles = m.triangles.ToList();

			vertecies.RemoveRange(0, 300);
			triangles.RemoveRange(0, 100);
			m.SetVertices(vertecies.ToArray());

			// m.SetTriangles(triangles);
			m.RecalculateBounds();
		}

		foreach (GameObject o in objects)
		{
			if (o != null)
			{
				o.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_DarkUnitsAmount", darkUnitsAmount);
				o.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_ShirtRandomColor", shirtRandomColor);
			}
		}

		if (GUILayout.Button("Align Scene Cam"))
		{
			var view = SceneView.lastActiveSceneView;
			if (view)
			{
				SceneView.lastActiveSceneView.cameraSettings.fieldOfView = Camera.main.fieldOfView;
				SceneView.lastActiveSceneView.AlignViewToObject(Camera.main.transform);
				SceneView.lastActiveSceneView.Repaint();
			}
		}

		if (GUILayout.Button("Reset Camera FOV"))
		{
			var view = SceneView.lastActiveSceneView;
			if (view)
			{
				SceneView.lastActiveSceneView.cameraSettings.fieldOfView = 60;
				SceneView.lastActiveSceneView.Repaint();
			}
		}
	}

	private void OnInspectorUpdate() {}
}