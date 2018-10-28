using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class GameInfo
	{
		private GraphicsDeviceManager _graphics = null;

		public GameInfo(GraphicsDeviceManager graphics)
		{
			_graphics = graphics;
		}

		public Paultangle GetBounds()
		{
			return new Paultangle(_graphics.GraphicsDevice.Viewport.Bounds);
		}


	}
}
