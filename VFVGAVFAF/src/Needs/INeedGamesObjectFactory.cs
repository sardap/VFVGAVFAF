using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	interface INeedGamesObjectFactory
	{
		[JsonIgnore]
		GameObjectFactory GameObjectFactory { get; set; }
	}
}
