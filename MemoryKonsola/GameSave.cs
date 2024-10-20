namespace MemoryKonsola
{
	public struct GameSave
	{
		public int width;
		public int height;
		public Card[,] cards;
		public int playersturn;
		public Player[] players;
	}
}
