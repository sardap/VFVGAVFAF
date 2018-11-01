using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RectOutlineRendCom : IRenderableComponent, INeedEnityManger, INeedSpriteBatch, INeedTextureManager
	{
		private TextureManager _textureManager;
		private Texture2D _texture;

		public long EntID { get; set; }

		public string HitBoxAlias { get; set; }

		public string TextureName { get; set; }

		public Color Color { get; set; }

		public int LineWidth { get; set; }

		[JsonIgnore]
		public TextureManager TextureManager
		{
			get
			{
				return _textureManager;
			}
			set
			{
				_textureManager = value;
				_texture = _textureManager.GetTexture(TextureName, Color);
			}
		}

		public SpriteBatch SpriteBatch { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Render(double deltaTime)
		{
			var rectangle = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(HitBoxAlias).Paultangle.ToMonoGameRectangle();
			SpriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y, LineWidth, rectangle.Height + LineWidth), Color);
			SpriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + LineWidth, LineWidth), Color);
			SpriteBatch.Draw(_texture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, LineWidth, rectangle.Height + LineWidth), Color);
			SpriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + LineWidth, LineWidth), Color);
		}
	}
}
