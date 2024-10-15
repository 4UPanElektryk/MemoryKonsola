using System;
using System.Text;

namespace MemoryKonsola
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			WriteColor("Podaj Imię Pierwszego Gracza: ", ConsoleColor.Yellow);
			Player player1 = new Player(Console.ReadLine());
			WriteColor("Podaj Imię Drugiego Gracza: ", ConsoleColor.Yellow);
			Player player2 = new Player(Console.ReadLine());
			WriteColor("Podaj Wymiary Planszy (8x8): ", ConsoleColor.Yellow);
			string[] size = Console.ReadLine().Split('x');
			int x = Convert.ToInt32(size[0]), y = Convert.ToInt32(size[1]);
			Console.Clear();
			Game game = new Game(x, y, new Player[] { player1, player2 });
			game.Start();
			Console.Clear();
		}
		internal static void WriteColor(string text, ConsoleColor color = ConsoleColor.Gray)
		{
			Console.ForegroundColor = color;
			Console.Write(text);
			Console.ResetColor();
		}
	}
}
