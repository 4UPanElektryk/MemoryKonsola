using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryKonsola
{
	internal class Card
	{
		public string HiddenText;
		public bool IsHidden;
		private static readonly int width = 6;
		private static readonly int height = 6;
		public Card(string Text) { HiddenText = Text; }
		public void Draw()
		{
			if (!IsHidden)
			{
				DrawShown();
				return;
			}
			DrawShown();
		}
		public void DrawShown()
		{

		}
		public void DrawHidden()
		{

		}
	}
}
