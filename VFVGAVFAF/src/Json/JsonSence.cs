using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Json
{
	class JsonSence
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

		public void Load(GameObjectFactory gameObjectFactory)
		{
			foreach(var entry in Entries)
			{
				for(int i = 0; i < entry.Count; i++)
				{
					gameObjectFactory.AddCreatedGameObject(entry.EnityToJson);
				}
			}			
		}
	}
}
