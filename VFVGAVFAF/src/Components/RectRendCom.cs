using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class RectRendCom : IRenderableComponent, INeedEnityManger, INeedSpriteBatch, INeedTextureManager
	{
		private TextureManager _textureManager;
		private Texture2D _texture;

		public long EntID { get; set; }

		public string RectPosAlais { get; set; }

		public EntityManager EntityManager { get; set; }

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

		[JsonIgnore]
		public SpriteBatch SpriteBatch { get; set; }

		public Color Color { get; set; }

		public string TextureName { get; set; }

		public RectRendCom()
		{
			int x = 0;
		}

		public void Render(double deltaTime)
		{
			var rect = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(RectPosAlais);
			SpriteBatch.Draw(_texture, rect.Rectangle.ToMonoGameRectangle(), Color);
		}
	}
}
