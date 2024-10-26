using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text.RegularExpressions;

namespace MemoryKonsola
{
	public class Game
	{
		private string[] wordlist =
		{
			"Hydrogen",
			"Helium",
			"Lithium",
			"Beryllium",
			"Boron",
			"Carbon",
			"Nitrogen",
			"Oxygen",
			"Fluorine",
			"Neon",
			"Sodium",
			"Magnesium",
			"Aluminium",
			"Silicon",
			"Phosphorus",
			"Sulfur",
			"Chlorine",
			"Argon",
			"Potassium",
			"Calcium",
			"Scandium",
			"Titanium",
			"Vanadium",
			"Chromium",
			"Manganese",
			"Iron",
			"Cobalt",
			"Nickel",
			"Copper",
			"Zinc",
			"Gallium",
			"Germanium"
		};
		private int Width;
		private int Height;
		private Card[,] Cards;
		private int playersturn;
		public Player whoWon;
		public Player[] Players;
		public Game(int width, int height, Player[] players) { 
			this.Width = width;
			this.Height = height;
			this.Players = players;
			SetCards();
			playersturn = 0;
		}
		public Game(GameSave save)
		{
			this.Width = save.width;
			this.Height = save.height;
			this.Cards = save.cards;
			this.playersturn = save.playersturn;
			this.Players = save.players;
		}
		public GameSave Save()
		{
			return new GameSave()
			{
				width = Width,
				height = Height,
				cards = Cards,
				playersturn = playersturn,
				players = Players,
			};
		}
		private void SetCards()
		{
			Cards = new Card[Width, Height];
			List<Card> posibleCards = new List<Card>();
			for (int i = 0; i < (Width * Height) / 2; i++)
			{
				posibleCards.Add(new Card(i * 2, wordlist[i]));
				posibleCards.Add(new Card(i * 2 + 1, wordlist[i]));
			}
			Random rnd = new Random();
			for (int x = 0; x < Width; x++) 
			{
				for (int y = 0; y < Height; y++) 
				{
					Card card = posibleCards[rnd.Next(posibleCards.Count())];
					Cards[x, y] = card;
					posibleCards.Remove(card);
				}
			}
		}
		private void Draw()
		{
			int spacingWidth = 17;
			int spacingHeight = 9;
			int leftOffset = 6;
			int topOffset = 4;
			int requiredWidth = Width > 3 ? Width * spacingWidth + leftOffset : 3 * spacingWidth + leftOffset;
			int requiredHeight = Height * spacingHeight + topOffset + 1; // +1 to linijka z nazwami i punktami graczy

			int oldWindowWidth = 0;
			int oldWindowHeight = 0;
			while (Console.WindowWidth < requiredWidth || Console.WindowHeight < requiredHeight)
			{
				if (!(oldWindowWidth != Console.WindowWidth || oldWindowHeight != Console.WindowHeight)) { Debug.WriteLine("Window Size not changed");  continue; }// sprawdzanie czy rozmiar okna się zmienił
				else
				{
					oldWindowHeight = Console.WindowHeight;
					oldWindowWidth = Console.WindowWidth;
				}
				Console.Clear();
				Console.Write("Szerokość okna: ");
				if (Console.WindowWidth < requiredWidth)
				{
					Console.ForegroundColor = ConsoleColor.Red;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Green;
				}
                Console.Write(Console.WindowWidth);
				Console.ResetColor();
                Console.WriteLine($" Wymagana: {requiredWidth}");
				Console.Write("Wysokość okna: ");
				if (Console.WindowHeight < requiredHeight)
				{
					Console.ForegroundColor = ConsoleColor.Red;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Green;
				}
				Console.Write(Console.WindowHeight);
				Console.ResetColor();
				Console.WriteLine($" Wymagana: {requiredHeight}");
			}
			Console.Clear();
			#region Grid
			Console.CursorTop = 1;
			Console.CursorLeft = 0;
			Console.Write("───╥─");
			for (int i = 0; i < Width; i++)
			{
				Console.Write("─────────────────");
			}
			Console.Write($"╥");
			Console.CursorTop++;
			Console.CursorLeft = 3;
            Console.Write("║ ");
			for (int i = 0; i < Width; i++)
			{
				char text = (char)('A' + i);
				Console.Write($"        {text}        ");
				if (i == Width - 1)
				{
					Console.Write($"║");
				}
			}
			Console.CursorTop++;
			Console.CursorLeft = 0;
			Console.Write("═══╬═");
			for (int i = 0; i < Width; i++)
			{
				Console.Write("═════════════════");
			}
            Console.Write("╝");
			Console.CursorTop++;
			Console.CursorLeft = 0;
			for (int y = 0; y < Height; y++)
			{
				char text = (char)('1' + y);
				Console.WriteLine($"   ║");
				Console.WriteLine($"   ║");
				Console.WriteLine($"   ║");
				Console.WriteLine($" {text} ║");
				Console.WriteLine($"   ║");
				Console.WriteLine($"   ║");
				Console.WriteLine($"   ║");
				Console.WriteLine($"   ║");
				if (y != Height - 1)
				{
					Console.WriteLine("   ║");
				}
				else
				{
                    Console.WriteLine("═══╝");
				}
			}
			#endregion
			#region Players
			Console.CursorTop = 0;
			Console.Write("Punkty: " );
			for (int i = 0; i < Players.Length; i++)
			{
				if (i == playersturn % Players.Length)
				{
					Console.ForegroundColor = ConsoleColor.Green;
				}
				Console.Write($"{Players[i].Name} - {Players[i].Points}, ");
				Console.ResetColor();
			}
			#endregion
			#region Cards
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Debug.WriteLine($"Drawing Card @ X: {x * spacingWidth + leftOffset}, Y: {y * spacingHeight + topOffset}");
					Cards[x, y].Draw(x * spacingWidth + leftOffset, y * spacingHeight + topOffset);
				}
			}
			#endregion
			Console.CursorTop = spacingHeight * Height + topOffset;
			Console.CursorLeft = 0;
		}
		private bool AllCardsUncovered()
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					if (Cards[x,y].IsHidden)
					{
						return false;
					}
				}
			}
			return true;
		}
		public void Start()
		{
			whoWon = null;
			while (whoWon == null) {
				Player current = Players[playersturn % Players.Length];
				#region Player Input

				string input = "";
				bool wasvalid = false;
				int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
				while (!Regex.IsMatch(input, $"[a-{(char)('a'+Width - 1)}][1-{Height}]|^exit$|^menu$") || !wasvalid)
				{
					Draw();
					Program.WriteColor($"Gracz {current.Name} podaj pierwszą kartę lub otwórz [Menu]: ");
					input = Console.ReadLine().ToLower();
					if (input == "menu" | input == "exit") { return; }
					else if (Regex.IsMatch(input, $"[a-{(char)('a' + Width - 1)}][1-{Height}]"))
					{
						x1 = input[0] - 'a';
						y1 = int.Parse(input[1].ToString()) - 1;
						wasvalid = Cards[x1, y1].IsHidden;
					}
				}
				Cards[x1,y1].IsHidden = false;
				input = "";
				wasvalid = false;
				while (!Regex.IsMatch(input, $"[a-{(char)('a' + Width - 1)}][1-{Height}]") || !wasvalid)
				{
					Draw();
					Program.WriteColor($"Gracz {current.Name} podaj Drugą kartę: ");
					input = Console.ReadLine().ToLower();
					if (Regex.IsMatch(input, $"[a-{(char)('a' + Width - 1)}][1-{Height}]"))
					{
						x2 = input[0] - 'a';
						y2 = int.Parse(input[1].ToString()) - 1;
						wasvalid = Cards[x2, y2].IsHidden;
					}
				}
				Cards[x2, y2].IsHidden = false;
				Draw();
				#endregion

				if (Cards[x1, y1].Text == Cards[x2, y2].Text)
				{
					Program.WriteColor("Gratulacje! Odkryto Parę", ConsoleColor.Green);
					Cards[x1, y1].AlreadyTaken = true;
					Cards[x2, y2].AlreadyTaken = true;
					Players[playersturn % Players.Length].Points++;
				}
				else
				{
					Program.WriteColor("Karty nie są parą", ConsoleColor.Red);
					Cards[x1, y1].IsHidden = true;
					Cards[x2, y2].IsHidden = true;
					playersturn++;
				}
				Console.ReadKey(true);
				if (AllCardsUncovered())
				{
					Console.Clear();
					whoWon = Players[playersturn % Players.Length];
                    Console.WriteLine($"Wygrał: {whoWon.Name} z {whoWon.Points} punktami");
				}
			}

			Console.ReadKey(true);
		}
	}
}
