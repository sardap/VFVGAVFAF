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
		public const int VIRTUAL_WIDTH = 800;
		public const int VIRTUAL_HEIGHT = 600;

		private GraphicsDeviceManager _graphics = null;

		public GameInfo(GraphicsDeviceManager graphics)
		{
			_graphics = graphics;
		}

		public Paultangle GetBounds()
		{
			return new Paultangle(0, 0, VIRTUAL_WIDTH, VIRTUAL_HEIGHT);
		}


	}
}
