using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using VFVGAVFAF.src.Sence;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VFVGAVFAF.src
{
	static class Utils
	{
		public static bool AInsideB(Paultangle a, Paultangle b)
		{
			bool result = a.Right < b.Right && a.Left > b.Left && a.Top >= b.Top && a.Bottom < b.Bottom;

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

		public static Postion2D RandomPostionInBounds(Paultangle rectangle)
		{
			double x = Random.NextDouble() * (rectangle.Right - rectangle.Left) + rectangle.Left;
			double y = Random.NextDouble() * (rectangle.Bottom - rectangle.Top) + rectangle.Top;
			return new Postion2D(x, y);
		}

		public static Paultangle RandomPostionInBounds(Paultangle bounds, Paultangle paultangle)
		{
			do
			{
				paultangle.Postion2D = RandomPostionInBounds(bounds);
			} while (!AInsideB(paultangle, bounds));

			return paultangle;
		}

		public static bool ImplementsInterface(Type subject, Type inter)
		{
			var subjectInterfaces = subject.GetInterfaces();
			if (subjectInterfaces.Any(i => i == inter))
			{
				return true;
			}
			else
			{
				foreach (var type in subjectInterfaces)
				{
					return ImplementsInterface(type, inter);
				}

				return false;
			}
		}

		public async static Task<IFactorySenceData> LoadSenceDataFromFile(string file)
		{
			SenceData senceData = null;

			using (StreamReader streamReader = new StreamReader(file))
			{
				senceData = JsonConvert.DeserializeObject<SenceData>(await streamReader.ReadToEndAsync());
			}

			return senceData;
		}

		public static void RemoveAll<K, V>(Dictionary<K, V> dict, IEnumerable<K> toRemove)
		{
			foreach(var i in toRemove)
			{
				dict.Remove(i);
			}
		}

		public static List<T> RemoveAll<T>(List<T> target, List<T> toRemove)
		{
			foreach(var i in toRemove)
			{
				target.Remove(i);
			}

			return target;
		}

		public static T RandomEntry<T>(IEnumerable<T> list)
		{
			int i = 0;
			int toGet = Random.Next(list.Count());

			foreach(var entry in list)
			{
				if(i == toGet)
				{
					return entry;
				}

				i++;
			}

			throw new System.Exception();
		}

		public static double FindCenter(double bounds, double pos)
		{
			return Math.Floor(bounds / 2) - Math.Floor(pos / 2);
		}

	}
}
