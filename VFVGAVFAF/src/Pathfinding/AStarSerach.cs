using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Pathfinding
{
	class AStarSerach : ISearch
	{
		private class Node
		{
			public bool Visted { get; set; }

			public bool Expanded{ get; set;}

			public Node Parenet { get; set; }

			public CellPostion CellPostion { get; set; }

			public Node(CellPostion  cellPostion)
			{
				Visted = false;
				Expanded = false;
				CellPostion = cellPostion;
			}

			public override int GetHashCode()
			{
				return CellPostion.GetHashCode();
			}
		}

		private List<CellPostion> ConsturctPath(CellPostion state, Dictionary<CellPostion, CellPostion?> meta)
		{
			var result = new List<CellPostion>();
			var end = state;

			while (true)
			{
				var row = meta[state];
				if (row != null)
				{
					state = (CellPostion)row;
					result.Add((CellPostion)row);
				}
				else
				{
					break;
				}
			}

			result.Reverse();
			if(result.Count > 0)
				result.Remove(result[0]);
			result.Add(end);

			return result;
		}

		public List<Postion2D> FindPath(Grid grid, Postion2D agentPos, Postion2D targetPos, string matchPattern)
		{
			var result = new List<Postion2D>();

			var agent = grid.Transform(agentPos);
			var target = grid.Transform(targetPos);

			//Holds the qeue of nodes to check which is sorted by there fscore
			SimplePriorityQueue<CellPostion, double> qeue = new SimplePriorityQueue<CellPostion, double>();
			qeue.Enqueue(agent, 0);
			//Holds each visted node
			HashSet<CellPostion> closedSet = new HashSet<CellPostion> { };
			//Used to find the path
			Dictionary<CellPostion, CellPostion?> meta = new Dictionary<CellPostion, CellPostion?>();
			// Used to create the fscore
			Dictionary<CellPostion, double> gScore = new Dictionary<CellPostion, double>
			{
				{ agent, 0 }
			};


			var root = agent;
			meta[root] = null;

			while (qeue.Count != 0)
			{
				var subTreeRoot = qeue.Dequeue();

				//Breaks if the goal has been found
				if (subTreeRoot.Equals(target))
				{
					return grid.Transform(ConsturctPath(subTreeRoot, meta));
				}

				foreach (CellPostion cellNeighbor in grid.GetNeighbors(subTreeRoot, matchPattern))
				{
					//If the node has been visted skip
					if (closedSet.Contains(cellNeighbor))
					{
						continue;
					}

					//if the node is a worse path skip
					double tentativeGScore = gScore[subTreeRoot] + grid.DistanceBetween(subTreeRoot, cellNeighbor);
					if (gScore.ContainsKey(cellNeighbor) && tentativeGScore >= gScore[cellNeighbor])
					{
						continue;
					}

					gScore[cellNeighbor] = tentativeGScore;

					if (!qeue.Contains(cellNeighbor))
					{
						meta[cellNeighbor] = subTreeRoot;
						//Adds the node to the qeue with it's fscore
						qeue.Enqueue(cellNeighbor, gScore[cellNeighbor] + grid.GetHeuristic(cellNeighbor, target));
					}

				}

				//adds that the node has been visted
				closedSet.Add(subTreeRoot);
			}

			return grid.Transform(ConsturctPath(agent, meta));
		}
	}
}
