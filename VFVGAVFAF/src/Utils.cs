using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	static class Utils
	{
		public static bool AInsideB(Paultangle a, Paultangle b)
		{
			bool result = a.Right < b.Right && a.Left > b.Left && a.Top > b.Top && a.Bottom < b.Bottom;

			return result;
		}

		public static bool AOverlapB(Paultangle a, Paultangle b)
		{
			return a.Intersects(b);
		}

		public static T DeepClone<T>(T obj)
		{
			using (var ms = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, obj);
				ms.Position = 0;

				return (T)formatter.Deserialize(ms);
			}
		}

		public static Random Random = new Random();

		public static double GetRandomDouble(double minimum, double maximum)
		{
			return Random.NextDouble() * (maximum - minimum) + minimum;
		}

		public static Postion2D RandomPostionInBounds(Rectangle rectangle)
		{
			double x = Random.NextDouble() * (rectangle.Right - rectangle.Left) + rectangle.Left;
			double y = Random.NextDouble() * (rectangle.Bottom - rectangle.Top) + rectangle.Top;
			return new Postion2D(x, y);
		}
	}
}
