using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class CenterVertical : ICenterOperation
	{
		public Postion2D Calcaute(Paultangle bounds, Postion2D size)
		{
			return new Postion2D(0, Math.Floor(bounds.Height / 2) - Math.Floor(size.Y / 2));
		}
	}
}
