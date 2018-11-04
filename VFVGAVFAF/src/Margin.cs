using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class Margin
	{
		public double Left { get; set; }
		public double Top { get; set; }

		public Postion2D Apply(Postion2D postion)
		{
			postion.X += Left;
			postion.Y += Top;

			return postion;
		}
	}
}
