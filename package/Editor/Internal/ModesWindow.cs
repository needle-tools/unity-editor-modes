using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEditorInternal;
using UnityEngine;

namespace Needle
{
	public class ModesWindow : EditorWindow
	{
		[InitializeOnLoadMethod]
		private static async void Init()
		{
			while (EditorApplication.isUpdating || EditorApplication.isCompiling) 
				await Task.Delay(100);
			var args = Environment.GetCommandLineArgs();
			for (var index = 0; index < args.Length; index++)
			{
				var arg = args[index];
				if (arg == "-mode" && (index + 1) < args.Length)
				{
					TryChangeMode(args[index + 1], false);
					return;
				}
			}
		}

		[Shortcut("Open Modes Window", KeyCode.F12, ShortcutModifiers.Action)]
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
						ChangeMode(index, mode, true);
					}
				}
			}
		}

		private static void TryChangeMode(string name, bool allowWindow)
		{
			if (ModeService.modeNames == null) return;
			for (var index = 0; index < ModeService.modeNames.Length; index++)
			{
				var mode = ModeService.modeNames[index];
				if (mode == name)
				{
					ChangeMode(index, name, allowWindow);
					return;
				}
			}
			Debug.LogWarning("Could not find mode \"" + name + "\", <b>Options<b/>:\n" + string.Join(",\n", ModeService.modeNames));
		}

		private static void ChangeMode(int index, string id, bool allowWindow)
		{
			Debug.Log($"Set mode to \"{id}\"");
			if (allowWindow)
				EditorApplication.delayCall += OpenModesWindow;
			ModeService.ChangeModeByIndex(index);
			ModeService.Update();
		}
	}
}