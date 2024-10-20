using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
		private int width;
		private int height;
		private Card[,] cards;
		private int playersturn;
		public Player whoWon;
		public Player[] players;
		public Game() { }

		private void SetCards()
		{
			cards = new Card[width, height];
			List<Card> posibleCards = new List<Card>();
			for (int i = 0; i < (width * height) / 2; i++)
			{
				posibleCards.Add(new Card(i * 2, wordlist[i]));
				posibleCards.Add(new Card(i * 2 + 1, wordlist[i]));
			}
			Random rnd = new Random();
			for (int x = 0; x < width; x++) 
			{
				for (int y = 0; y < height; y++) 
				{
					Card card = posibleCards[rnd.Next(posibleCards.Count())];
					cards[x, y] = card;
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
			Console.WindowWidth = width > 3 ? width * spacingWidth + leftOffset : 3 * spacingWidth + leftOffset;
			Console.WindowHeight = height * spacingHeight + topOffset + 1;
			Console.Clear();
			#region Grid
			Console.CursorTop = 1;
			Console.CursorLeft = 0;
			Console.Write("───╥");
			for (int i = 4; i < Console.WindowWidth; i++)
			{
				if (i == Console.WindowWidth - 1)
				{
					Console.Write($"╥");
				}
				else
				{
					Console.Write("─");
				}
			}
			Console.CursorTop++;
			Console.CursorLeft = 3;
            Console.Write("║ ");
			for (int i = 0; i < width; i++)
			{
				char text = (char)('A' + i);
				Console.Write($"        {text}        ");
				if (i == width - 1)
				{
					Console.Write($"║");
				}
			}
			Console.CursorTop++;
			Console.CursorLeft = 0;
			Console.Write("═══╬");
			for (int i = 4; i < Console.WindowWidth; i++)
			{
				if (i == Console.WindowWidth - 1)
				{
                    Console.Write("╝");
				}
				else
				{
					Console.Write("═");
				}
			}
			Console.CursorTop++;
			Console.CursorLeft = 0;
			for (int y = 0; y < height; y++)
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
				if (y != height - 1)
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
			for (int i = 0; i < players.Length; i++)
			{
				if (i == playersturn % players.Length)
				{
					Console.ForegroundColor = ConsoleColor.Green;
				}
				Console.Write($"{players[i].Name} - {players[i].Points}, ");
				Console.ResetColor();
			}
			#endregion
			#region Cards
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Debug.WriteLine($"Drawing Card @ X: {x * spacingWidth + leftOffset}, Y: {y * spacingHeight + topOffset}");
					cards[x, y].Draw(x * spacingWidth + leftOffset, y * spacingHeight + topOffset);
				}
			}
			#endregion
			Console.CursorTop = spacingHeight * height + topOffset;
			Console.CursorLeft = 0;
		}
		private bool AllCardsUncovered()
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (cards[x,y].IsHidden)
					{
						return false;
					}
				}
			}
			return true;
		}
		public void Start()
		{
			Menu(false);
			TitlePage();
			if (cards == null)
			{
				SetCards();
			}
			whoWon = null;
			string input;
			while (whoWon == null) {
				Console.Clear();
				Draw();
				#region Player Input
				Player current = players[playersturn % players.Length];
				Program.WriteColor($"Gracz {current.Name} podaj Pierwszą kartę: ");
				input = Console.ReadLine().ToLower();


				int x1 = input[0] - 'a', y1 = int.Parse(input[1].ToString()) - 1;
				cards[x1,y1].IsHidden = false;
				Draw();

				Program.WriteColor($"Gracz {current.Name} podaj Drugą kartę: ");
				input = Console.ReadLine().ToLower();

				int x2 = input[0] - 'a', y2 = int.Parse(input[1].ToString()) - 1;
				cards[x2, y2].IsHidden = false;
				Draw();
				#endregion
				if (cards[x1, y1].Text == cards[x2, y2].Text)
				{
					Program.WriteColor("Gratulacje! Odkryto Parę", ConsoleColor.Green);
					cards[x1, y1].AlreadyTaken = true;
					cards[x2, y2].AlreadyTaken = true;
					players[playersturn % players.Length].Points++;
				}
				else
				{
					Program.WriteColor("Karty nie są parą", ConsoleColor.Red);
					cards[x1, y1].IsHidden = true;
					cards[x2, y2].IsHidden = true;
					playersturn++;
				}
				Console.ReadKey(true);
				if (AllCardsUncovered())
				{
					Console.Clear();
					whoWon = players[playersturn % players.Length];
                    Console.WriteLine($"Wygrał: {whoWon} z {whoWon.Points} punktami");
				}
			}

			Console.ReadKey(true);
		}
	}
}
