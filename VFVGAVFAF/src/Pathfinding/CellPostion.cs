using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Pathfinding
{
	struct CellPostion
	{
		public int Y;
		public int X;

		public CellPostion(int y, int x)
		{
			Y = y;
			X = x;
		}

		public bool Equals(CellPostion other)
		{
			return this.X == other.X && this.Y == other.Y;
		}
	}
}
