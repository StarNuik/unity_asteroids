using UnityEngine;
using InputContext = UnityEngine.InputSystem.InputAction.CallbackContext;
using Editor = UnityEngine.SerializeField;
using UnityEngine.InputSystem;

namespace Asteroids
{
	public class InputListener : MonoBehaviour
	{
		[Editor] InputActionAsset actionsAsset;

		public InputMap? InputDelta { get; set; }
		
		private InputAction primaryFire;
		private InputAction ultimateFire;
		private InputAction accelerate;
		private InputAction rotate;

		private void Awake()
		{
			Locator.ClientInput = this;

			var actionMap = actionsAsset.FindActionMap("Player");
			actionMap.actionTriggered += AssembleInputDelta;

			primaryFire = actionMap.FindAction("PrimaryFire");
			ultimateFire = actionMap.FindAction("UltimateFire");
			accelerate = actionMap.FindAction("Accelerate");
			rotate = actionMap.FindAction("Rotate");
		}

		private void AssembleInputDelta(InputContext ctx)
		{
			var input = new InputMap
			{
				PrimaryFire = primaryFire.phase == InputActionPhase.Performed,
				UltimateFire = ultimateFire.phase == InputActionPhase.Performed,
				Accelerate = accelerate.phase == InputActionPhase.Performed,
				Rotate = Mathf.RoundToInt(rotate.ReadValue<float>())
			};
			InputDelta = input;
			// Debug.Log($"New input delta:\n{InputDelta.Value}");
		}

		private void Update()
		{
			if (InputDelta.HasValue)
			{
				Debug.Log($"[InputListener.Update()] New input delta:\n{InputDelta.Value}");
			}
		}
	}
}