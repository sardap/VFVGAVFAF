﻿using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	[Serializable]
	class Paultangle
	{
		public Postion2D Postion2D { get; set; }

		public double Width { get; set; } 

		public double Height { get; set; }

		[JsonIgnore]
		public double X { get { return Postion2D.X; } }
		[JsonIgnore]
		public double Y { get { return Postion2D.Y; } }
		[JsonIgnore]
		public double X1 { get { return X; } }
		[JsonIgnore]
		public double Y1 { get { return Y; } }
		[JsonIgnore]
		public double X2 { get { return X + Width; } }
		[JsonIgnore]
		public double Y2 { get { return Y + Height; } }
		[JsonIgnore]
		public double Top { get { return Postion2D.Y; } }
		[JsonIgnore]
		public double Bottom { get { return Postion2D.Y + Height; } }
		[JsonIgnore]
		public double Left { get { return Postion2D.X; } }
		[JsonIgnore]
		public double Right { get { return Postion2D.X + Width; } }

		public Paultangle(double x, double y, double width, double height)
		{
			Postion2D = new Postion2D(x, y);
			Width = width;
			Height = height;
		}

		public Paultangle(Postion2D postion2D, double width, double height) : this(postion2D.X, postion2D.Y, width, height)
		{
		}

		public Paultangle(Rectangle rectangle) : this(new Postion2D(rectangle), rectangle.Width, rectangle.Height)
		{
		}

		public Paultangle(Paultangle paultangle) : this(paultangle.Postion2D, paultangle.Width, paultangle.Height)
		{
		}

		public Paultangle() : this(new Postion2D(0, 0), 0, 0)
		{
		}

		public RectangleF ToRectangleF()
		{
			return new RectangleF((float)X, (float)Y, (float)Width, (float)Height);
		}

		public bool Intersects(Paultangle other)
		{
			Paultangle RectA = this;
			Paultangle RectB = other;

			return RectA.X1 < RectB.X2 && 
				RectA.X2 > RectB.X1 &&
				RectA.Y1 < RectB.Y2 && 
				RectA.Y2 > RectB.Y1;
		}

		public Rectangle ToMonoGameRectangle()
		{
			return new Rectangle
			(
				(int)Math.Round(Postion2D.X), 
				(int)Math.Round(Postion2D.Y),
				(int)Math.Round(Width),
				(int)Math.Round(Height)
			);
		}

		public bool Conatins(Postion2D postion2D)
		{
			return postion2D.X > X && postion2D.X < Right & postion2D.Y > Y && postion2D.Y < Bottom;
		}
	}
}
