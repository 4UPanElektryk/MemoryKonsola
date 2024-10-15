using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			"Oxygen", //TODO: Uzupełnić
			//...
			"Indium"
		};
		private int width;
		private int height;
		private Card[,] cards;
		private int playersturn;
		public Player whoWon;
		public Player[] players;
		public Game(int width, int height, Player[] players)
		{
			this.width = width;
			this.height = height;
			this.players = players;
			this.whoWon = null;
			this.playersturn = 0;
		}
		private void SetCards()
		{
			cards = new Card[width, height];
			List<Card> posibleCards = new List<Card>();
			for (int i = 0; i < (width * height) / 2; i++)
			{
				posibleCards.Add(new Card(i * 2, i, wordlist[i]));
				posibleCards.Add(new Card(i * 2 + 1, i, wordlist[i]));
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
			int spacing_width = 17;
			int spacing_height = 9;
			Console.WindowWidth = height > 3 ? width * spacing_width : 3 * spacing_width;
			Console.WindowHeight = height * spacing_height + 3;
			Console.Clear();
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
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Debug.WriteLine($"Drawing Card @ X: {x * spacing_width}, Y: {y * spacing_height + 1}");
					cards[x, y].Draw(x * spacing_width, y * spacing_height + 1);
				}
			}
			Console.CursorTop = spacing_height * height + 1;
			Console.CursorLeft = 0;
		}
		private void TitlePage()
		{
            Console.WriteLine();

            Console.WriteLine("Instrukcja Tutaj");
            Console.WriteLine("Karty zaznaczamy jak w programie Excel (eg. A1, b2, ...) ");
            Console.WriteLine("Kolumy - Litery");
            Console.WriteLine("Wiersze - czyfry");
            Console.WriteLine();
            Program.WriteColor("Naciśnij Dowolny Przycisk aby Rozpocząć grę",ConsoleColor.Yellow);
			Console.ReadKey(true);
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
			SetCards();
			TitlePage();
			whoWon = null;
			string input = "";
			while (whoWon == null) {
				Console.Clear();
				Draw();
				Player current = players[playersturn % players.Length];
				Program.WriteColor($"Gracz {current.Name} podaj Pierwszą kartę: ");
				input = Console.ReadLine().ToLower();

				int x1 = input[0] - 'a', y1 = int.Parse(input[1].ToString()) - 1;
				cards[x1,y1].IsHidden = false;
				Console.Clear();
				Draw();

				Program.WriteColor($"Gracz {current.Name} podaj Drugą kartę: ");
				input = Console.ReadLine().ToLower();

				int x2 = input[0] - 'a', y2 = int.Parse(input[1].ToString()) - 1;
				cards[x2, y2].IsHidden = false;
				Console.Clear();
				Draw();

				if (cards[x1, y1].PairID == cards[x2, y2].PairID)
				{
					Program.WriteColor("Gratulacje Odkryto Parę", ConsoleColor.Green);
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
