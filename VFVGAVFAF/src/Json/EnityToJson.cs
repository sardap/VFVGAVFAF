using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Json
{
	class EnityToJson
	{
		public List<IComponent> Components { get; set; }

		public List<string> Tags { get; set; }

		public EnityToJson()
		{
			Tags = new List<string>();
		}

		public EnityToJson(GameObject gameObject)
		{
			PopluateFromEntiy(gameObject);
		}

		public T GetbyAlais<T>(string alais)where T : IHaveAlias
		{
			foreach(var com in Components)
			{
				if(com is IHaveAlias && ((IHaveAlias)com).Alias == alais)
				{
					return (T)com;
				}
			}

			throw new KeyNotFoundException();
		}

		public void PopluateFromEntiy(IEntity entity)
		{
			Components = new List<IComponent>();
			var coms = entity.GetComponents;
			coms.ForEach(i => Components.Add(i.Item2));
		}
	}
}
