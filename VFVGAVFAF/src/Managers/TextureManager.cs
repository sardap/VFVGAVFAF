using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class TextureManager
	{
		private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

		public ContentManager Content { get; set; }

		public Texture2D GetTexture(string name)
		{
			if(!_textures.ContainsKey(name))
			{
				_textures.Add(name, Content.Load<Texture2D>(name));
			}

			return _textures[name];
		}
	}
}
