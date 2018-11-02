using Microsoft.Xna.Framework;
using MonoGame.Extended;
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

		public Postion2D(Vector2 vector2): this(vector2.X, vector2.Y)
		{

		}

		public Point2 ToPoint2()
		{
			return new Point2((float) X, (float) Y);
		}

		public Vector2 ToVector()
		{
			return new Vector2((float)X, (float)Y);
		}

		public Rectangle ToRectangle(int width, int height)
		{
			return new Rectangle((int)Math.Round(X), (int)Math.Round(Y), width, height);
		}

		public bool IsAt(Postion2D target)
		{
			return X == target.X && Y == target.Y;
		}

		public bool WithinRadius(Postion2D target, double radius)
		{
			var d = Math.Sqrt(Math.Pow((X - target.X), 2) + Math.Pow((Y - target.Y), 2));

			return d <= radius;
		}

		public bool AtOrPast(Postion2D target)
		{
			return IsAt(target) || X > target.X || Y > target.Y;
		}

		public static Postion2D operator +(Postion2D a, Postion2D b)
		{
			return new Postion2D(a.X + b.X, a.Y + b.Y);
		}

		public static Postion2D operator *(Postion2D a, double b)
		{
			return new Postion2D(a.X * b, a.Y * b);
		}

	}
}
