using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class FontManger
	{
		private Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();

		public ContentManager Content { get; set; }

		public SpriteFont GetFont(string name)
		{
			if (!_fonts.ContainsKey(name))
			{
				_fonts.Add(name, Content.Load<SpriteFont>(name));
			}

			return _fonts[name];
		}
	}
}
