﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using VFVGAVFAF.src.Sence;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VFVGAVFAF.src.Managers;
using Newtonsoft.Json.Linq;

namespace VFVGAVFAF.src
{
	static class Utils
	{
		public static JsonSerializerSettings JsonSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto
		};

		public static void ExportToJsonFile<T>(T toExport, string fileName)
		{
			string jsonString = JsonConvert.SerializeObject(toExport, JsonSettings);

			using (var streamWriter = new StreamWriter(fileName))
			{
				streamWriter.Write(JToken.Parse(jsonString).ToString(Formatting.Indented));
			}
		}

		public static T GetFromJsonFile<T>(string fileName)
		{
			using (var streamReader = new StreamReader(fileName))
			{
				var jsonString = streamReader.ReadToEnd();
				return JsonConvert.DeserializeObject<T>(jsonString, JsonSettings);
			}
		}
		
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

		public static string ReadEntireFile(string fileName)
		{
			using (StreamReader streamReader = new StreamReader(fileName))
			{
				return streamReader.ReadToEnd();
			}

			throw new FileLoadException();
		}

		public static void RemoveAll<K, V>(Dictionary<K, V> dict, IEnumerable<K> toRemove)
		{
			foreach (var i in toRemove)
			{
				dict.Remove(i);
			}
		}

		public static List<T> RemoveAll<T>(List<T> target, List<T> toRemove)
		{
			var result = target.ToList();

			foreach (var i in toRemove)
			{
				result.Remove(i);
			}

			return result;
		}

		public static double FindCenter(double bounds, double pos)
		{
			return Math.Floor(bounds / 2) - Math.Floor(pos / 2);
		}

		private delegate bool CheckCon(Paultangle a, Paultangle B);

		public static bool Check(Paultangle a, Paultangle b, CheckType type, bool inside)
		{
			CheckCon d;

			switch (type)
			{
				case CheckType.Contains:
					d = new CheckCon(Utils.AInsideB);
					break;
				case CheckType.Overlapping:
					d = new CheckCon(Utils.AOverlapB);
					break;
				default:
					throw new NotImplementedException();
			}

			bool result;

			if (inside)
				result = d(a, b);
			else
				result = !d(a, b);

			return result;
		}

		public static void AddRange<T>(ISet<T> set, IEnumerable<T> toAdd)
		{
			foreach (var entry in toAdd)
			{
				set.Add(entry);
			}
		}

		public static void PushRange<T>(Stack<T> stack, IEnumerable<T> toAdd)
		{
			foreach(var entry in toAdd)
			{
				stack.Push(entry);
			}
		}

		public static MangersEnum MangerToMangerEnum(IManger manger)
		{
			if(manger is RenderManager)
			{
				return MangersEnum.RenderManger;
			}
			if (manger is ColssionComManger)
			{
				return MangersEnum.CollsionManger;
			}
			if (manger is StepManager)
			{
				return MangersEnum.StepManger;
			}
			if (manger is InputManger)
			{
				return MangersEnum.InputManger;
			}

			throw new NotImplementedException();
		}

		public static T RandomElement<T>(IEnumerable<T> set)
		{
			var index = Random.Next(set.Count() - 1);

			return set.ToList()[index];
		}

		public static Postion2D IncrmnetForPoint(Postion2D start, Postion2D target, double deltaTime, double speed)
		{
			var startVec = start.ToVector();
			var targetVec = target.ToVector();

			float distance = Vector2.Distance(startVec, targetVec);
			var direction = Vector2.Normalize(targetVec - startVec);

			var inc = direction * (float)speed * (float)deltaTime;

			while (Vector2.Distance(startVec, inc + startVec) >= distance)
			{
				speed -= 0.1;
				inc = direction * (float)speed * (float)deltaTime;
			}

			return new Postion2D(inc);
		}

	}
}
