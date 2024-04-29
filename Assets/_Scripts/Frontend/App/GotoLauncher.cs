using System.Diagnostics;
using Asteroids.Frontend;
using DevLocker.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	[DefaultExecutionOrder(-997)]
	public class GotoLauncher : MonoBehaviour
	{
		[Editor] SceneReference launcher;

		// [Conditional("UNITY_EDITOR")]
		private void Awake()
		{
			if (Locator.Connection == null)
			{
				SceneManager.LoadScene(launcher.ScenePath);
			}
			Destroy(this);
		}
	}
}
