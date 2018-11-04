using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class CenterHorizontal: ICenterOperation
	{
		public Postion2D Calcaute(Paultangle bounds, Postion2D size)
		{
			return new Postion2D((bounds.Width / 2) - (size.X / 2), 0);
		}

	}
}
