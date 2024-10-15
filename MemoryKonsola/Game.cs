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
			"Oxygen",
		};
		private int width;
		private int height;
		private Card[,] cards;
		public Game(int width, int height)
		{
			this.width = width;
			this.height = height;
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
		private void DrawCards()
		{
			int spacing_width = 17;
			int spacing_height = 9;
			Console.WindowWidth = width * spacing_width;
			Console.WindowHeight = height * spacing_height+2;
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Debug.WriteLine($"Drawing @ X: {x * spacing_width}, Y: {y * spacing_height}");
					cards[x, y].Draw(x * spacing_width, y * spacing_height);
				}
			}
			Console.CursorTop = spacing_height * height;
			Console.CursorLeft = 0;
            Console.WriteLine("coś");
		}
		private void TitlePage()
		{
            Console.WriteLine();

            Console.WriteLine("Instrukcja Tutaj");
            Console.WriteLine("Karty zaznaczamy jak w programie Excel (eg. A1, b2, 3c) ");
			
            Console.WriteLine("Naciśnij Dowolny Przycisk aby Rozpocząć grę");
			Console.ReadKey(true);
		}
		public void Start(Player[] player)
		{
			SetCards();
			DrawCards();
			Console.ReadKey(true);
		}
	}
}
