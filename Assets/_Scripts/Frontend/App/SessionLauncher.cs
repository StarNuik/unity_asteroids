using DevLocker.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class SessionLauncher : MonoBehaviour
	{
		[Editor] SceneReference Session;

		private void Start()
		{
			var op = SceneManager.LoadSceneAsync(Session.ScenePath, LoadSceneMode.Additive);
			op.allowSceneActivation = true;
		}
	}
}
