using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RectRendCom : IRenderableComponent
	{
		private long _rectangleComponetID;
		private EntityManager _entityManager;

		public long EntID { get; set; }

		public Texture2D Texture { get; set; }
		public SpriteBatch SpriteBatch { get; set; }
		public Color Color { get; set; }

		public RectRendCom(long entID, EntityManager entityManager, long rectangleComponetID)
		{
			EntID = entID;
			_entityManager = entityManager;
			_rectangleComponetID = rectangleComponetID;
		}

		public void Render(double deltaTime)
		{
			var rect = _entityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(_rectangleComponetID);
			SpriteBatch.Draw(Texture, rect.Rectangle.ToMonoGameRectangle(), Color);
		}
	}
}
