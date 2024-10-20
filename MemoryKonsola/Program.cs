using System;
using System.Text;

namespace MemoryKonsola
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Game game = new Game();
			game.Start();
			Console.Clear();
		}
		private void TitlePage()
		{
			Console.WriteLine();

			Console.WriteLine("Instrukcja Tutaj");
			Console.WriteLine("Karty zaznaczamy jak w programie Excel (eg. A1, b2, ...) ");
			Console.WriteLine("Kolumy - Litery");
			Console.WriteLine("Wiersze - czyfry");
			Console.WriteLine();
			Program.WriteColor("Naciśnij Dowolny Przycisk aby Rozpocząć grę", ConsoleColor.Yellow);
			Console.ReadKey(true);
		}
		internal static void WriteColor(string text, ConsoleColor color = ConsoleColor.Gray)
		{
			Console.ForegroundColor = color;
			Console.Write(text);
			Console.ResetColor();
		}
		private void NewGame()
		{
			Console.ResetColor();
			Console.Clear();
			Program.WriteColor("Podaj Imię Pierwszego Gracza: ", ConsoleColor.Yellow);
			Player player1 = new Player(Console.ReadLine());
			Program.WriteColor("Podaj Imię Drugiego Gracza: ", ConsoleColor.Yellow);
			Player player2 = new Player(Console.ReadLine());
			Console.WriteLine("0. 2x2");
			Console.WriteLine("1. 2x3");
			Console.WriteLine("2. 3x4");
			Console.WriteLine("3. 4x4");
			Console.WriteLine("4. 4x5");
			Console.WriteLine("5. 5x6");
			Console.WriteLine("6. 6x6");
			Console.WriteLine("7. 6x7");
			Console.WriteLine("8. 7x8");
			Console.WriteLine("9. 8x8");
			Program.WriteColor("Wybierz Wymiary Planszy (1-8): ", ConsoleColor.Yellow);
			int x = 0, y = 0;
			bool ok = false;
			while (!ok)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.D0: case ConsoleKey.NumPad0: x = 2; y = 2; ok = true; break;
					case ConsoleKey.D1: case ConsoleKey.NumPad1: x = 2; y = 3; ok = true; break;
					case ConsoleKey.D2: case ConsoleKey.NumPad2: x = 3; y = 4; ok = true; break;
					case ConsoleKey.D3: case ConsoleKey.NumPad3: x = 4; y = 4; ok = true; break;
					case ConsoleKey.D4: case ConsoleKey.NumPad4: x = 4; y = 5; ok = true; break;
					case ConsoleKey.D5: case ConsoleKey.NumPad5: x = 5; y = 6; ok = true; break;
					case ConsoleKey.D6: case ConsoleKey.NumPad6: x = 6; y = 6; ok = true; break;
					case ConsoleKey.D7: case ConsoleKey.NumPad7: x = 6; y = 7; ok = true; break;
					case ConsoleKey.D8: case ConsoleKey.NumPad8: x = 7; y = 8; ok = true; break;
					case ConsoleKey.D9: case ConsoleKey.NumPad9: x = 8; y = 8; ok = true; break;
					default:
						Program.WriteColor("Podaj Poprawną akcję!", ConsoleColor.Red);
						Console.ReadKey(true);
						break;
				}
			}
			this.width = x;
			this.height = y;
			this.players = new Player[] { player1, player2 };
			this.whoWon = null;
			this.playersturn = 0;
			Console.Clear();
		}
		private void Menu(bool isIngame = false)
		{
			while (true)
			{
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Clear();
				Console.WriteLine("Podaj Akcję: ");
				Console.WriteLine("0. Wyjdź");
				Console.WriteLine("1. Jak Grać?");
				Console.WriteLine("2. Nowa Gra");
				Console.WriteLine("3. Wczytaj Grę");
				if (isIngame)
				{
					Console.WriteLine("4. Zapisz Grę");
				}

				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.D0:
					case ConsoleKey.NumPad0:
						Environment.Exit(0);
						break;
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:
						TitlePage();
						return;
					case ConsoleKey.D2:
					case ConsoleKey.NumPad2:
						NewGame();
						return;
					case ConsoleKey.D3:
					case ConsoleKey.NumPad3:

						break;
					default:
						Console.WriteLine("Podaj Poprawną akcję!");
						Console.ReadKey(true);
						break;
				}
			}


		}

		private void LoadGame()
		{

		}
	}
}
