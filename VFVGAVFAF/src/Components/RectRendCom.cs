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
		private ComponentManager _componentManager;

		public Texture2D Texture { get; set; }
		public SpriteBatch SpriteBatch { get; set; }
		public Color Color { get; set; }

		public RectRendCom(ComponentManager componentManager, long rectangleComponetID)
		{
			_componentManager = componentManager;
			_rectangleComponetID = rectangleComponetID;
		}

		public void Render(double deltaTime)
		{
			var rect = _componentManager.GetComponent<RectPosCom>(_rectangleComponetID);
			SpriteBatch.Draw(Texture, rect.Rectangle, Color);
		}
	}
}
