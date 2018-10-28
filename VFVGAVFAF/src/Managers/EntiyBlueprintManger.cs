using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Json;

namespace VFVGAVFAF.src
{
	class EntiyBlueprintManger
	{
		public Dictionary<string, EnityToJson> Blueprints { get; set; }

		public EnityToJson Get(string name)
		{
			var settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto
			};

			var blueprint = Blueprints[name];
			var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(blueprint, settings);

			return Newtonsoft.Json.JsonConvert.DeserializeObject<EnityToJson>(jsonString, settings);
		}
	}
}
