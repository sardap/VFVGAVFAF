using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	interface IRenderableComponent : IComponent
	{
		void Render(double deltaTime);
	}
}
