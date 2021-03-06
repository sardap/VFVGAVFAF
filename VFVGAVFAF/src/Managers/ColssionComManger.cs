﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;
using VFVGAVFAF.src.Exception;
using VFVGAVFAF.src.Pathfinding;

namespace VFVGAVFAF.src.Managers
{
	class ColssionComManger : IManger
	{
		private IList<long> _toCheck = new List<long>();
		private QuadTree _quadTree;
		private Grid _grid;

		private ComponentManager _componentManager { get; set; }
		private EntityManager _entityManager { get; set; }

		public ColssionComManger(ComponentManager componentManager, EntityManager entityManager, Grid grid)
		{
			_componentManager = componentManager;
			_entityManager = entityManager;
			_grid = grid;
			_quadTree = new QuadTree(_componentManager, 0, new Microsoft.Xna.Framework.Rectangle(0, 0, 300, 600));
		}


		public void Check()
		{
			_grid.Clear();

			foreach(var id in _toCheck)
			{
				var com = _componentManager.GetComponent<ICollisionCom>(id);
				var ent = _entityManager.GetEntiy<GameObject>(com.EntID);
				_grid.Insert(com.HitBox, id, ent.Tags);
			}

			var colCount = 0;

			foreach (var id in _toCheck)
			{
				var com = _componentManager.GetComponent<ICollisionCom>(id);

				foreach (var otherID in _toCheck)
				{
					if(id != otherID)
					{
						var otherEntID = _componentManager.GetComponent<ICollisionCom>(otherID).EntID;
						if (com.Check(otherEntID, otherID))
						{
							//System.Diagnostics.Debug.WriteLine("COLLSION BETWEEN {0} and {1}", id, otherID);
							colCount++;
						}
					}
				}
			}

			//System.Diagnostics.Debug.WriteLine("COLLSIONS: {0}", colCount);
		}

		public void Regsiter(long comID)
		{
			if (_componentManager.GetComponent<ICollisionCom>(comID) != null)
			{
				_toCheck.Add(comID);
				return;
			}

			throw new MissmatchComponetException();
		}

		public void UnRegsiter(long id)
		{
			_toCheck.Remove(id);
		}
	}
}
