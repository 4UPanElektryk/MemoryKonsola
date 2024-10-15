using System;
using System.Text;

namespace MemoryKonsola
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			WriteColor("Podaj Nazwę Pierwszego Gracza: ", ConsoleColor.Yellow);
			Player player1 = new Player(Console.ReadLine());
			WriteColor("Podaj Nazwę Drugiego Gracza: ", ConsoleColor.Yellow);
			Player player2 = new Player(Console.ReadLine());
			WriteColor("Podaj Wymiary Planszy (szerokość,wysokość): ", ConsoleColor.Yellow);
			string[] size = Console.ReadLine().Split(',');
			int x = Convert.ToInt32(size[0]), y = Convert.ToInt32(size[1]);
			Console.Clear();
			Game game = new Game(x, y);
			game.Start(new Player[] { player1, player2 });
			Console.Clear();
		}
		private static void WriteColor(string text, ConsoleColor color = ConsoleColor.Gray)
		{
			Console.ForegroundColor = color;
			Console.Write(text);
			Console.ResetColor();
		}
	}
}
