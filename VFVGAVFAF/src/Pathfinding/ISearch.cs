using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Pathfinding
{
	interface ISearch
	{
		List<Postion2D> FindPath(Grid grid, Postion2D agentPos, Postion2D targetPos, string matchPattern);
	}
}
