using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class Paultangle
	{
		public Postion2D Postion2D { get; set; }

		public int Width { get; set; } 

		public int Height { get; set; }

		public Paultangle(Rectangle rectangle)
		{
			Postion2D = new Postion2D(rectangle);
			Width = rectangle.Width;
			Height = rectangle.Height;
		}

		public Paultangle(Postion2D postion2D, int width, int height)
		{
			Postion2D = new Postion2D(postion2D);
			Width = width;
			Height = height;
		}

		public Paultangle() : this(new Postion2D(0, 0), 0, 0)
		{
		}

		public Rectangle ToMonoGameRectangle()
		{
			return new Rectangle
			(
				(int)Math.Round(Postion2D.X), 
				(int)Math.Round(Postion2D.Y), 
				Width, 
				Height
			);
		}
	}
}
