namespace Asteroids
{
	public struct InputDelta : IStateDelta
	{
		public bool PrimaryFire;
		public bool UltimateFire;
		public bool Accelerate;
		public int Rotate;

		public void ApplyTo(SessionState state)
		{
			state.PlayerInput = this;
		}

		public static InputDelta ConstructFrom(SessionState state)
		{
			return state.PlayerInput;
		}

		public override string ToString()
		{
			return $"[InputMap]: primary: {PrimaryFire}, ultimate: {UltimateFire}, accelerate: {Accelerate}, rotate: {Rotate}";
		}
	}
}