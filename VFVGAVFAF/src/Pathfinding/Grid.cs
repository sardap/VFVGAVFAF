using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Pathfinding
{
	class Grid
	{
		private const int CELL_SIZE = 10;
		private const int GRID_HEIGHT = GameInfo.VIRTUAL_HEIGHT / CELL_SIZE;
		private const int GRID_WIDTH = GameInfo.VIRTUAL_WIDTH / CELL_SIZE;

		private Dictionary<long, List<CellPostion>> _idCellMap = new Dictionary<long, List<CellPostion>>();

		public List<List<Cell>> Cells = new List<List<Cell>>();

		public Grid()
		{
			Clear();
		}

		public void Clear()
		{
			_idCellMap.Clear();
			Cells.Clear();

			for (int i = 0; i < GRID_HEIGHT; i++)
			{
				Cells.Add(new List<Cell>());

				for (int j = 0; j < GRID_WIDTH; j++)
				{
					Cells[i].Add(new Cell());
				}
			}
		}

		public CellPostion Transform(Postion2D postion2D)
		{
			int left = Math.Max((int)Math.Floor(postion2D.X / CELL_SIZE), 0);
			int top = Math.Max((int)Math.Floor(postion2D.Y / CELL_SIZE), 0);
			return new CellPostion(top, left);
		}

		public Postion2D Transform(CellPostion cellPostion)
		{
			double left = cellPostion.X * CELL_SIZE;
			double top = cellPostion.Y * CELL_SIZE;
			return new Postion2D(left, top);
		}

		public List<Postion2D> Transform(List<CellPostion> cellPostions)
		{
			var result = new List<Postion2D>();
			cellPostions.ForEach(i => result.Add(Transform(i)));
			return result;
		}

		public void Insert(Paultangle paultangle, long id, List<string> tags)
		{
			if (paultangle.Width > 0 && paultangle.Height > 0)
			{
				int left = Math.Max((int)Math.Floor(paultangle.Left / CELL_SIZE), 0);
				int top = Math.Max((int)Math.Floor(paultangle.Top / CELL_SIZE), 0);
				int right = Math.Min((int)Math.Floor(paultangle.Right / CELL_SIZE), GRID_WIDTH - 1);
				int bottom = Math.Min((int)Math.Floor(paultangle.Bottom / CELL_SIZE), GRID_HEIGHT - 1);

				_idCellMap.Add(id, new List<CellPostion>());

				for (int y = top; y < bottom; y++)
				{
					for (int x = left; x < right; x++)
					{
						Cells[y][x].Conatins.Add(id);
						Cells[y][x].CollectiveTags.AddRange(tags);
						_idCellMap[id].Add(new CellPostion(y, x));
					}
				}
			}
		}

		public List<CellPostion> GetNeighbors(CellPostion cellPostion, string matchPattern)
		{
			var result = new HashSet<CellPostion>();

			int col = cellPostion.Y;
			int row = cellPostion.X;

			for (int y = col - 1; y <= (col + 1); y += 1)
			{
				for (int x = row - 1; x <= (row + 1); x += 1)
				{
					if (
						!((y == col) && (x == row))
					)
					{
						if (VaildCellPostion(x, y))
						{
							var cell = Cells[y][x];
							if (!cell.CollectiveTags.Any(i => Regex.IsMatch(i, matchPattern)))
							{
								result.Add(new CellPostion(y, x));
							}
						}
					}
				}
			}

			return result.ToList();
		}

		public double DistanceBetween(CellPostion a, CellPostion b)
		{
			return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
		}

		public int GetHeuristic(CellPostion cell, CellPostion target)
		{
			int result = Math.Abs(cell.X - target.X) + Math.Abs(cell.Y - target.Y);

			return result;
		}

		private bool VaildCellPostion(int x, int y)
		{
			return x < GRID_WIDTH && y < GRID_HEIGHT && x > 0 && y > 0;
		}
	}
}
