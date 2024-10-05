using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class ComputeSphericalHarmonics : MonoBehaviour
{
	public static string GetAssetPath(string defaultName)
	{
		const string prefsKey = "SphericalHarmonicsToFile";
		string previousPath = EditorPrefs.GetString(prefsKey, "Assets");
		string path = EditorUtility.SaveFilePanel("Save Spherical Harmonics coefficients", previousPath, defaultName, "txt");
		if (string.IsNullOrEmpty(path))
		{
			return null;
		}

		EditorPrefs.SetString(prefsKey, new FileInfo(path).Directory.FullName);
		return path;
	}

	[MenuItem("Assets/Compute Spherical Harmonics and save to file")]
	private static void SaveSHToFile()
	{
		var selection = Selection.activeObject;
		var material = selection as Material;
		if (material == null)
		{
			Debug.LogError("The selected item is not a material!");
			return;
		}

		if (material.shader == null /*|| material.shader.name != "Skybox/Cubemap"*/)
		{
			Debug.LogError("The selected material does not have a shader assigned.");
			return;
		}

		string assetPath = AssetDatabase.GetAssetPath(selection);
		string outputPath = GetAssetPath(Path.GetFileNameWithoutExtension(assetPath) + "_SH");

		SphericalHarmonicsL2 harmonics = GetSphericalHarmonicsL2(material);

		WriteSphericalHarmonicsToFile(outputPath, harmonics);
	}

	private static void WriteSphericalHarmonicsToFile(string outputPath, SphericalHarmonicsL2 harmonics)
	{
		Vector4[] data = PackSphericalHarmonicsData(harmonics);
		string[] names = { "SHAr", "SHAg", "SHAb", "SHBr", "SHBg", "SHBb", "SHC" };

		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < names.Length; i++)
		{
			stringBuilder.Append("half4(" + data[i].x + "," + data[i].y + "," + data[i].z + "," + data[i].w + ")" + " //" + names[i] + "\n");
		}

		stringBuilder.Append("\n\n");

		for (int i = 0; i < names.Length; i++)
		{
			stringBuilder.Append("#define sh_" + names[i] + " half4(" + data[i].x + "," + data[i].y + "," + data[i].z + "," + data[i].w + ")\n");
		}

		File.WriteAllText(outputPath, stringBuilder.ToString());
	}

	private static SphericalHarmonicsL2 GetSphericalHarmonicsL2(Material skyboxMaterial)
	{
		// See RenderSettings.cpp / RenderSettings::CalculateAmbientProbeFromSkybox()
		// The desired texture (material) has to be set into Lighting window on Skybox Material property.
		// From that texture, DynamicGI.UpdateEnvironment() will generate spherical harmonic coefficients.

		AmbientMode oldAmbientMode = RenderSettings.ambientMode;
		Material oldSkyboxMaterial = RenderSettings.skybox;

		RenderSettings.ambientMode = AmbientMode.Skybox;
		RenderSettings.skybox = skyboxMaterial;
		DynamicGI.UpdateEnvironment();

		RenderSettings.ambientMode = oldAmbientMode;
		RenderSettings.skybox = oldSkyboxMaterial;

		return RenderSettings.ambientProbe;
	}

	private static Vector4[] PackSphericalHarmonicsData(SphericalHarmonicsL2 harmonics)
	{
		// This is from SHConstantCache.cpp, ApplySHConstants()
		// and from SphericalHarmonicsL2.cpp, GetShaderConstantsFromNormalizedSH()
		// So, from SHL2, the data[] array is filled and the data is directly used
		// for uniforms in the sahder. That is done in ApplySHConstants.
		// The data[] corresponds in order to: SHAr = data[0], SHAg = data[1], SHAb = ..., SHBr, SHBg, SHBb, SHC

		Vector4[] data = new Vector4[7];
		for (int channel = 0; channel < 3; channel++)
		{
			data[channel].x = harmonics[channel, 3];
			data[channel].y = harmonics[channel, 1];
			data[channel].z = harmonics[channel, 2];
			data[channel].w = harmonics[channel, 0] - harmonics[channel, 6];
		}

		for (int channel = 0; channel < 3; channel++)
		{
			data[channel + 3].x = harmonics[channel, 4];
			data[channel + 3].y = harmonics[channel, 5];
			data[channel + 3].z = harmonics[channel, 6] * 3.0f;
			data[channel + 3].w = harmonics[channel, 7];
		}

		data[6].x = harmonics[0, 8];
		data[6].y = harmonics[1, 8];
		data[6].z = harmonics[2, 8];
		data[6].w = 1.0f;

		return data;
	}
}