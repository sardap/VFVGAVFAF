using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class CircleRendCom : IRenderableComponent, INeedEnityManger, INeedSpriteBatch
	{
		private TextureManager _textureManager;
		private Texture2D _texture;

		public long EntID { get; set; }

		public string RectPosAlais { get; set; }

		public Color Color { get; set; }

		public EntityManager EntityManager { get; set; }

		[JsonIgnore]
		public SpriteBatch SpriteBatch { get; set; }

		public void Render(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var com = ent.GetComponent<CirclePostionCom>(RectPosAlais);

			ShapeExtensions.DrawCircle(SpriteBatch, com.ToMonogameCircle(), 50, Color, (float)com.Radius);
		}
	}
}
