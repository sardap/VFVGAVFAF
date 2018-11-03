using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Pathfinding
{
	class Cell
	{
		public List<long> Conatins { get; set; }
		public List<string> CollectiveTags { get; set; }

		public Cell()
		{
			Conatins = new List<long>();
			CollectiveTags = new List<string>();
		}
	}
}
