namespace Asteroids
{
	public struct InputMap
	{
		public bool PrimaryFire;
		public bool UltimateFire;
		public bool Accelerate;
		public int Rotate;

		public override string ToString()
		{
			return $"[InputMap]: primary: {PrimaryFire}, ultimate: {UltimateFire}, accelerate: {Accelerate}, rotate: {Rotate}";
		}
	}
}