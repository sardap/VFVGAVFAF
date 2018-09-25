using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RectOutlineRendCom : IRenderableComponent
	{
		private long _rectangleComponetID;
		private ComponentManager _componentManager;

		public Texture2D Texture { get; set; }
		public SpriteBatch SpriteBatch { get; set; }
		public Color Color { get; set; }
		public int LineWidth { get; set; }

		public RectOutlineRendCom(ComponentManager componentManager, long rectangleComponetID)
		{
			_componentManager = componentManager;
			_rectangleComponetID = rectangleComponetID;
		}

		public void Render(double deltaTime)
		{
			var rectangle = _componentManager.GetComponent<RectPosCom>(_rectangleComponetID).Rectangle.ToMonoGameRectangle();
			SpriteBatch.Draw(Texture, new Rectangle(rectangle.X, rectangle.Y, LineWidth, rectangle.Height + LineWidth), Color);
			SpriteBatch.Draw(Texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + LineWidth, LineWidth), Color);
			SpriteBatch.Draw(Texture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, LineWidth, rectangle.Height + LineWidth), Color);
			SpriteBatch.Draw(Texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + LineWidth, LineWidth), Color);
		}
	}
}
