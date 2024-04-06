using UnityEngine;
using InputContext = UnityEngine.InputSystem.InputAction.CallbackContext;
using Editor = UnityEngine.SerializeField;
using UnityEngine.InputSystem;
using Asteroids.Lib;

namespace Asteroids.Frontend
{
	public class InputListener : MonoBehaviour
	{
		[Editor] InputActionAsset actionsAsset;

		private InputAction primaryFire;
		private InputAction ultimateFire;
		private InputAction accelerate;
		private InputAction rotate;

		private IPublisher outStream => Locator.ClientOut;

		private void Awake()
		{
			var actionMap = actionsAsset.FindActionMap("Player");
			actionMap.actionTriggered += AssembleInputDelta;

			primaryFire = actionMap.FindAction("PrimaryFire");
			ultimateFire = actionMap.FindAction("UltimateFire");
			accelerate = actionMap.FindAction("Accelerate");
			rotate = actionMap.FindAction("Rotate");
		}

		private void AssembleInputDelta(InputContext ctx)
		{
			var input = new InputDelta
			{
				PrimaryFire = primaryFire.phase.IsInProgress(),
				UltimateFire = ultimateFire.phase.IsInProgress(),
				Accelerate = accelerate.phase.IsInProgress(),
				Rotate = Mathf.RoundToInt(rotate.ReadValue<float>())
			};

			outStream.Pub(input);
		}
	}
}