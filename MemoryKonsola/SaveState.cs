namespace MemoryKonsola
{
	public struct SaveState
	{
		public int width;
		public int height;
		public Card[,] cards;
		public int playersturn;
		public Player[] players;

		public SaveState(int width, int height, Card[,] cards, int playersturn, Player[] players)
		{
			this.width = width;
			this.height = height;
			this.cards = cards;
			this.playersturn = playersturn;
			this.players = players;
		}
	}
}
