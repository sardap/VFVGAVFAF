using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Json
{
	class JsonSence : ISenceData
	{
		public class Entry
		{
			public EnityToJson EnityToJson { get; set; }
			public long Count { get; set; }

		}

		public List<Entry> Entries { get; set; }

		public JsonSence()
		{
			Entries = new List<Entry>();
		}

		public List<long> Load(GameObjectFactory gameObjectFactory, List<IPassValue> passValues)
		{
			var result = new List<long>();

			foreach(var entry in Entries)
			{
				var toPass = passValues.FindAll(i => entry.EnityToJson.Tags.Any(j => Regex.IsMatch(j, i.Tag)));

				for(int i = 0; i < entry.Count; i++)
				{
					result.Add(gameObjectFactory.AddCreatedGameObject(entry.EnityToJson, toPass));
				}
			}

			return result;
		}
	}
}
