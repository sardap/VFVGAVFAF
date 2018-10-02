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

		public EnityToJson()
		{
		}

		public void PopluateFromEntiy(IEntity entity)
		{
			Components = entity.GetComponents;
		}
	}
}
