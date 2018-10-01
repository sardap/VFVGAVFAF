using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;

namespace VFVGAVFAF.src
{
	/// <summary>
	/// AUTHOR : Steven Lambert
	/// URL : https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374
	/// Modified / Translated by : Paul Sarda
	/// </summary>
	class QuadTree
	{
		private const int QUAD_TREE_MAX_OBJECTS = 5;
		private const int QUAD_TREE_MAX_LEVELS = 100;

		private int _level;
		private ComponentManager _componentManager;
		private IList<long> _objects;
		private Rectangle _bounds;
		private QuadTree[] _nodes;

		public QuadTree(ComponentManager componentManager, int pLevel, Rectangle pBounds)
		{
			_componentManager = componentManager;
			_level = pLevel;
			_objects = new List<long>();
			_bounds = pBounds;
			_nodes = new QuadTree[4];
		}

		public void Process(IList<long> collisions)
		{
			Clear();
			Populate(collisions);
			CheckCollsions(collisions);
		}

		private void Populate(IList<long> collisions)
		{
			foreach (long i in collisions)
			{
				Insert(i);
			}
		}

		private void CheckCollsions(IList<long> collisions)
		{
			foreach(var id in collisions)
			{
				foreach (var otherID in Retrieve(id))
				{
					if(id != otherID)
					{
						var otherEntID = _componentManager.GetComponent<ICollisionCom>(otherID).EntID;
						_componentManager.GetComponent<ICollisionCom>(id).Check(otherEntID, otherID);
					}
				}
			}

			/*
			Parallel.ForEach(collisions, id => 
			{
				foreach (var otherID in Retrieve(id))
				{
					_componentManager.GetComponent<ICollisionCom>(id).Check(otherID);
				}
			});
			*/
		}

		private void Clear()
		{
			_objects.Clear();

			for (int i = 0; i < _nodes.Length; i++)
			{
				if (_nodes[i] != null)
				{
					_nodes[i].Clear();
					_nodes[i] = null;
				}
			}
		}

		private void Insert(long pRect)
		{
			if (_nodes[0] != null)
			{
				int i = GetIndex(pRect);

				if (i != -1)
				{
					_nodes[i].Insert(pRect);

					return;
				}
			}

			_objects.Add(pRect);

			if (_objects.Count > QUAD_TREE_MAX_OBJECTS && _level < QUAD_TREE_MAX_LEVELS)
			{
				if (_nodes[0] == null)
				{
					Split();
				}

				int j = 0;

				while (j < _objects.Count)
				{
					int index = GetIndex(_objects[j]);

					if (index != -1)
					{
						_nodes[index].Insert(_objects[j]);
						_objects.RemoveAt(j);
					}
					else
					{
						j++;
					}
				}
			}
		}

		private ConcurrentStack<long> Retrieve(HashSet<long> returnedObjs, long obj)
		{
			if (_nodes[0] != null)
			{
				var index = GetIndex(obj);
				if (index != -1)
				{
					_nodes[index].Retrieve(returnedObjs, obj);
				}
				else
				{
					for (int i = 0; i < _nodes.Length; i++)
					{
						_nodes[i].Retrieve(returnedObjs, obj);
					}
				}
			}

			ConcurrentStack<long> result = new ConcurrentStack<long>();

			foreach (long i in _objects)
			{
				result.Push(i);
			}

			return result;
		}

		private ConcurrentStack<long> Retrieve(long pRect)
		{
			return Retrieve(new HashSet<long>(), pRect);
		}

		private void Split()
		{
			int subWidth = Convert.ToInt32(_bounds.Width) >> 1;
			int subHeight = Convert.ToInt32(_bounds.Height) >> 1;
			int newX = Convert.ToInt32(_bounds.X);
			int newY = Convert.ToInt32(_bounds.Y);

			_nodes[0] = new QuadTree(_componentManager, _level + 1, new Rectangle(newX + subWidth, newY, subWidth, subHeight));
			_nodes[1] = new QuadTree(_componentManager, _level + 1, new Rectangle(newX, newY, subWidth, subHeight));
			_nodes[2] = new QuadTree(_componentManager, _level + 1, new Rectangle(newX, newY + subHeight, subWidth, subHeight));
			_nodes[3] = new QuadTree(_componentManager, _level + 1, new Rectangle(newX + subWidth, newY + subHeight, subWidth, subHeight));
		}

		private int GetIndex(long pRectID)
		{
			var pRect = _componentManager.GetComponent<ICollisionCom>(pRectID);

			int index = -1;
			double verticalMidpoint = _bounds.X + (_bounds.Width / 2);
			double horizontalMidpoint = _bounds.Y + (_bounds.Height / 2);

			// Object can completely fit within the top quadrants
			bool topQuadrant = (pRect.GetHitBox.Y < horizontalMidpoint && pRect.GetHitBox.Y + pRect.GetHitBox.Height < horizontalMidpoint);

			// Object can completely fit within the bottom quadrants
			bool bottomQuadrant = (pRect.GetHitBox.Y > horizontalMidpoint);

			// Object can completely fit within the left quadrants
			if (pRect.GetHitBox.X < verticalMidpoint && pRect.GetHitBox.X + pRect.GetHitBox.Width < verticalMidpoint)
			{
				if (topQuadrant)
				{
					index = 1;
				}
				else if (bottomQuadrant)
				{
					index = 2;
				}
			}
			// Object can completely fit within the right quadrants
			else if (pRect.GetHitBox.X > verticalMidpoint)
			{
				if (topQuadrant)
				{
					index = 0;
				}
				else if (bottomQuadrant)
				{
					index = 3;
				}
			}
			return index;
		}


	}
}
