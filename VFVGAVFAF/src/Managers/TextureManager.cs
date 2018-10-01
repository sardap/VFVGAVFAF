using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace VFVGAVFAF.src
{
	class TextureManager
	{
		private Dictionary<string, Dictionary<Color, Texture2D>> _textures = new Dictionary<string, Dictionary<Color, Texture2D>>();

		public ContentManager Content { get; set; }

		public Texture2D GetTexture(string name, Color color)
		{
			if (!_textures.ContainsKey(name))
			{
				_textures.Add(name, new Dictionary<Color, Texture2D>());
				_textures[name].Add(Color.Black, Content.Load<Texture2D>(name));
			}

			if(!_textures[name].ContainsKey(color))
			{
				var texture = _textures[name].First().Value;
				Color[] tcolor = new Color[texture.Width * texture.Height];
				texture.GetData(tcolor);

				for (int i = 0; i < tcolor.Length; i++)
				{
					tcolor[i].R = color.R;
					tcolor[i].G = color.G;
					tcolor[i].B = color.B;
				}
				texture.SetData(tcolor);

				_textures[name].Add(color, texture);
			}

			return _textures[name][color];
		}
	}
}
