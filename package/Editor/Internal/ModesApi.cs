using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Needle
{
	public static class ModesApi
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
					TryChangeMode(args[index + 1]);
					return;
				}
			}
		}

		
		public static void TryChangeMode(string name)
		{
			if (ModeService.modeNames == null) return;
			for (var index = 0; index < ModeService.modeNames.Length; index++)
			{
				var mode = ModeService.modeNames[index];
				if (mode == name)
				{
					Debug.Log($"Set mode to \"{mode}\"");
					ChangeMode(index);
					return;
				}
			}
			Debug.LogWarning("Could not find mode \"" + name + "\", <b>Options<b/>:\n" + string.Join(",\n", ModeService.modeNames));
		}

		public static void ChangeMode(int index)
		{
			ModeService.ChangeModeByIndex(index);
			ModeService.Update();
		}
	}
}