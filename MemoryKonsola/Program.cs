using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MemoryKonsola
{
	internal class Program
	{
		static Game game;
		static string SavesDir = AppDomain.CurrentDomain.BaseDirectory + "Saves\\";
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			game = null;
			Menu();
			Console.Clear();
		}
		static void Menu()
		{
			while (true)
			{
				Console.ResetColor();
				Console.Clear();
				Console.WriteLine("Podaj Akcję: ");
				Console.WriteLine("0. Wyjdź");
				Console.WriteLine("1. Jak Grać?");
				Console.WriteLine("2. Nowa Gra");
				if (Directory.Exists(SavesDir))
				{
					Console.WriteLine("3. Wczytaj Grę");
				}
				if (game != null && game.whoWon == null)
				{
					Console.WriteLine("4. Zapisz Grę");
					Console.WriteLine("5. Kontynuuj Grę");
				}

				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.D0:
					case ConsoleKey.NumPad0:
						return;
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:
						TitlePage();
						break;
					case ConsoleKey.D2:
					case ConsoleKey.NumPad2:
						NewGame();
						game.Start();
						break;

					case ConsoleKey.D3:
					case ConsoleKey.NumPad3:
						if (LoadGame())
						{
							game.Start();
						}
						break;
					case ConsoleKey.D4:
					case ConsoleKey.NumPad4:
						SaveGame();
						break;
					case ConsoleKey.D5:
					case ConsoleKey.NumPad5:
						game.Start();
						break;
					default:
						Console.WriteLine("Podaj Poprawną akcję!");
						Console.ReadKey(true);
						break;
				}
			}


		}
		static void TitlePage()
		{
			Console.WriteLine();

			Console.WriteLine("Instrukcja Tutaj");
			Console.WriteLine("Karty zaznaczamy jak w programie Excel (eg. A1, b2, ...) ");
			Console.WriteLine("Kolumy - Litery");
			Console.WriteLine("Wiersze - czyfry");
			Console.WriteLine();
			Program.WriteColor("Naciśnij Dowolny Przycisk aby zamknąć", ConsoleColor.Yellow);
			Console.ReadKey(true);
		}
		static void NewGame()
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
			game = new Game(x, y, new Player[] { player1, player2 });
			Console.Clear();
		}
		static void SaveGame()
		{
			GameSave save = game.Save();
			if (!Directory.Exists(SavesDir))
			{
				Directory.CreateDirectory(SavesDir);
			}
			Console.Clear();
			Program.WriteColor("Podaj Nazwę dla tego zapisu: ", ConsoleColor.Yellow);
			string name = Console.ReadLine();
			name = name
				.Replace("<", "")
				.Replace(">", "")
				.Replace(":", "")
				.Replace("\"", "")
				.Replace("/", "")
				.Replace("\\", "")
				.Replace("|", "")
				.Replace("?", "")
				.Replace("*", ""); //Zabronione zanki w nazwie pliku
			File.WriteAllText(SavesDir + name + ".json", JsonConvert.SerializeObject(save));
			Program.WriteColor("Zapisano pomyślnie jako: " + SavesDir + name + ".json", ConsoleColor.Yellow);
			Console.ReadKey(true);
		}
		static bool LoadGame()
		{
			string[] files = Directory.GetFiles(SavesDir);
			Console.Clear();
            Console.WriteLine($"Wybierz zapis: (0 - {files.Length})");
            Console.WriteLine("0. Powróć do menu");
            Console.WriteLine();

			for (int i = 0; i < files.Length; i++)
			{
                Console.WriteLine($"{i+1}. {Path.GetFileName(files[i])}");
			}
            Console.WriteLine();
			string input = Console.ReadLine();
			int t = 0;
			while((!int.TryParse(input, out t) || t < 0) && t-1 < files.Length)
			{
                Console.Write("Niepoprawna wartość! Podaj Ponownie: ");
				input = Console.ReadLine();
			}
			if (t == 0)
			{
				return false;
			}
			game = new Game(JsonConvert.DeserializeObject<GameSave>(File.ReadAllText(files[t-1])));
			return true;
		}
		internal static void WriteColor(string text, ConsoleColor color = ConsoleColor.Gray)
		{
			Console.ForegroundColor = color;
			Console.Write(text);
			Console.ResetColor();
		}
	}
}
