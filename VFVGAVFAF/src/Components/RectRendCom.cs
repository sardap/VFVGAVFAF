using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Needs;

namespace VFVGAVFAF.src.Components
{
	class RectRendCom : IRenderableComponent, INeedEnityManger, INeedSpriteBatch, INeedTextureManager
	{
		private long _rectangleComponetID;
		private TextureManager _textureManager;
		private Texture2D _texture;

		public long EntID { get; set; }
		public EntityManager EntityManager { get; set; }
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

		public RectRendCom(EntityManager entityManager, long rectangleComponetID)
		{
			EntityManager = entityManager;
			_rectangleComponetID = rectangleComponetID;
		}

		public RectRendCom()
		{

		}

		public void Render(double deltaTime)
		{
			var rect = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(_rectangleComponetID);
			SpriteBatch.Draw(_texture, rect.Rectangle.ToMonoGameRectangle(), Color);
		}
	}
}
