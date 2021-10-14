using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class ModesWindow : EditorWindow
{
	[InitializeOnLoadMethod]
	private static void Init()
	{
		if (!HasOpenInstances<ModesWindow>())
		{
			var window = CreateWindow<ModesWindow>();
			window.Show();
		}
		else
		{
			FindObjectOfType<ModesWindow>()?.Show();
		}
	}

	private void OnGUI()
	{
		if (GUILayout.Button("Refresh Modes")) 
			ModeService.Update(); 
		EditorGUILayout.Space(10);
		if (ModeService.modeNames != null)
		{
			for (var index = 0; index < ModeService.modeNames.Length; index++)
			{
				var mode = ModeService.modeNames[index];
				if (GUILayout.Button(mode))
				{
					ChangeModeDelayed(mode);
				}
			}
		}
	}

	private async void ChangeModeDelayed(string id)
	{
		await Task.Delay(10);
		Debug.Log("Selected " + id);
		ModeService.ChangeModeById(id);
		EditorApplication.delayCall += Show;
	}
}