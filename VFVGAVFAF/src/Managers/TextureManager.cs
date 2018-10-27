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
		private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

		public ContentManager Content { get; set; }

		public Texture2D GetTexture(string name, Color color)
		{
			if (!_textures.ContainsKey(name))
			{
				_textures.Add(name, Content.Load<Texture2D>(name));

				var texture = _textures[name];
				Color[] tcolor = new Color[texture.Width * texture.Height];
				texture.GetData(tcolor);

				for (int i = 0; i < tcolor.Length; i++)
				{
					tcolor[i].R = 255;
					tcolor[i].G = 255;
					tcolor[i].B = 255;
				}
				texture.SetData(tcolor);
			}

			return _textures[name];
		}
	}
}
