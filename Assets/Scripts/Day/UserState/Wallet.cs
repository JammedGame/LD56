namespace Night
{
	public class Wallet
	{
		public int Currency { get; private set; }

		public Wallet(int startAmount)
		{
			Currency = startAmount;
		}

		public bool CanAfford(int cost)
		{
			return Currency >= cost;
		}

		public bool TrySpend(int cost)
		{
			if (Currency >= cost)
			{
				Currency -= cost;
				return true;
			}

			return false;
		}

		public void Add(int lootAmount)
		{
			Currency += lootAmount;
		}
	}
}