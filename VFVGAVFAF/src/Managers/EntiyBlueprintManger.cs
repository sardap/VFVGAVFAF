using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Json;

namespace VFVGAVFAF.src
{
	class BlueprintManger
	{
		private JsonSerializerSettings _settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto
		};

		public Dictionary<string, EnityToJson> EntiyBlueprints { get; set; }
		public Dictionary<string, IComponent> ComBlueprints { get; set; }

		public EnityToJson Get(string name)
		{
			var blueprint = EntiyBlueprints[name];
			var jsonString = JsonConvert.SerializeObject(blueprint, _settings);

			return JsonConvert.DeserializeObject<EnityToJson>(jsonString, _settings);
		}

		public IComponent GetCom(string name)
		{
			var blueprint = ComBlueprints[name];
			var jsonString = JsonConvert.SerializeObject(blueprint, typeof(IComponent), _settings);
			return JsonConvert.DeserializeObject<IComponent>(jsonString, _settings);
		}
	}
}
