using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class RectOutlineRendCom : IRenderableComponent
	{
		private long _rectangleComponetID;
		private EntityManager _entityManager;

		public long EntID { get; set; }

		public Texture2D Texture { get; set; }
		public SpriteBatch SpriteBatch { get; set; }
		public Color Color { get; set; }
		public int LineWidth { get; set; }

		public RectOutlineRendCom(long entID, EntityManager entityManager, long rectangleComponetID)
		{
			EntID = entID;
			_entityManager = entityManager;
			_rectangleComponetID = rectangleComponetID;
		}

		public void Render(double deltaTime)
		{
			var rectangle = _entityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(_rectangleComponetID).Rectangle.ToMonoGameRectangle();
			SpriteBatch.Draw(Texture, new Rectangle(rectangle.X, rectangle.Y, LineWidth, rectangle.Height + LineWidth), Color);
			SpriteBatch.Draw(Texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + LineWidth, LineWidth), Color);
			SpriteBatch.Draw(Texture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, LineWidth, rectangle.Height + LineWidth), Color);
			SpriteBatch.Draw(Texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + LineWidth, LineWidth), Color);
		}
	}
}
