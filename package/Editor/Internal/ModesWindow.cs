using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace Needle
{
	public class ModesWindow : EditorWindow
	{
		[Shortcut("Open Modes Window", KeyCode.F12, ShortcutModifiers.Action)]
		[MenuItem("Window/Needle/Unity Modes Window")]
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

		private Vector2 scroll;

		private void OnGUI()
		{
			EditorGUILayout.Space(10);
			if (ModeService.modeNames != null)
			{
				using var sv = new EditorGUILayout.ScrollViewScope(scroll);
				scroll = sv.scrollPosition;

				if (GUILayout.Button("Scan Modes", GUILayout.Height(30)))
					ModeService.ScanModes();
				GUILayout.Space(10);

				for (var index = 0; index < ModeService.modeNames.Length; index++)
				{
					var mode = ModeService.modeNames[index];
					if (GUILayout.Button(mode))
					{
						Debug.Log($"Set mode to \"{mode}\"");
						ModesApi.ChangeMode(index);
						EditorApplication.delayCall += OpenModesWindow;
					}
				}
			}
		}
	}
}