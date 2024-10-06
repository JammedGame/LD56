namespace Night
{
	public class UserState
	{
		public readonly Wallet Gold = new Wallet(100);

		
		// singleton shit
		private static UserState instance;
		public static UserState Instance => instance ??= new();

		public static void Reset()
		{
			instance = new();
		}
	}
}