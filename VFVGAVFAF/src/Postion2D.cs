using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class Postion2D
	{
		public double X { get; set; }
		public double Y { get; set; }

		public Postion2D() : this(0, 0)
		{
		}

		public Postion2D(Rectangle rectangle) : this(rectangle.X, rectangle.Y)
		{
		}

		public Postion2D(Postion2D postion2D) : this(postion2D.X, postion2D.Y)
		{
		}

		public Postion2D(double x, double y)
		{
			X = x;
			Y = y;
		}

		public Rectangle ToRectangle(int width, int height)
		{
			return new Rectangle((int)Math.Floor(X), (int)Math.Floor(Y), width, height);
		}
	}
}
