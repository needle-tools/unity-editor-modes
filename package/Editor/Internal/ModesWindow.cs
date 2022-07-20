using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Needle
{
	public class ModesWindow : EditorWindow
	{
		[MenuItem("Editor Modes/Modes Window")]
		private static void OpenModesWindow()
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
			EditorGUILayout.Space(10);
			if (ModeService.modeNames != null)
			{
				if (GUILayout.Button("Scan Modes"))
					ModeService.ScanModes();
				GUILayout.Space(10);
				
				for (var index = 0; index < ModeService.modeNames.Length; index++)
				{
					var mode = ModeService.modeNames[index];
					if (GUILayout.Button(mode))
					{
						ChangeModeDelayed(index, mode);
					}
				}
			}
		}

		private void ChangeModeDelayed(int index, string id)
		{
			Debug.Log($"Change mode \"{id}\", index {index}");
			ModeService.ChangeModeByIndex(index);
			EditorApplication.delayCall += OpenModesWindow;
			ModeService.Update();
		}
	}
}