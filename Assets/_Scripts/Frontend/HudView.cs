using System;
using System.Globalization;
using Asteroids.Frontend;
using TMPro;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class HudView : MonoBehaviour
	{
		[Editor] TMP_Text score;
		[Editor] TMP_Text super;
		[Editor] TMP_Text health;
		
		private ISubscribable Server => Locator.ServerIn;

		private void Awake()
		{
			Server.Sub<UpdateHud>(UpdateGui);
		}

		private void UpdateGui(UpdateHud update)
		{
			var scoreValue = update.Score.ToString("00000", CultureInfo.InvariantCulture);
			score.text = $"Score: {scoreValue}";
		}
	}
}
