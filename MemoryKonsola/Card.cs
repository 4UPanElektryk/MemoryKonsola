using System;
namespace MemoryKonsola
{
	public class Card
	{
		public int CardID;
		public string Text;
		public bool IsHidden;
		public bool AlreadyTaken;
		public Card(int cardID, string Text) 
		{
			CardID = cardID;
			Text = Text;
			IsHidden = true;
		}
		public void Draw(int x, int y)
		{
			if (!IsHidden)
			{
				DrawShown(x,y);
				return;
			}
			DrawHidden(x,y);
		}
		private void DrawShown(int x, int y)
		{
			if (AlreadyTaken)
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
			}
			Console.CursorTop = y;
			Console.CursorLeft = x;                      Console.Write("╔═════════════╗");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║ " + Text.PadRight(11) + " ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║             ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║             ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║             ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║             ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║ " + Text.PadLeft(11) + " ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("╚═════════════╝");
			//Console.CursorLeft = x; Console.CursorTop++; Console.Write($"Pair ID: {PairID}");
			Console.ResetColor();
		}
		private void DrawHidden(int x, int y)
		{
			Console.CursorTop = y;
			Console.CursorLeft = x;                      Console.Write("╔═════════════╗");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║ M           ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║   E         ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║     M       ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║       O     ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║         R   ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("║           Y ║");
			Console.CursorLeft = x; Console.CursorTop++; Console.Write("╚═════════════╝");
			//Console.CursorLeft = x; Console.CursorTop++; Console.Write($"Pair ID: {PairID}");
		}
	}
}
