using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Pathfinding;

namespace VFVGAVFAF.src
{
	interface INeedGrid
	{
		[JsonIgnore]
		Grid Grid { get; set; }
	}
}
