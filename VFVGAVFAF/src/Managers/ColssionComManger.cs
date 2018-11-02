using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;
using VFVGAVFAF.src.Exception;

namespace VFVGAVFAF.src.Managers
{
	class ColssionComManger : IManger
	{
		private IList<long> _toCheck = new List<long>();
		private QuadTree _quadTree;
		private ComponentManager _componentManager { get; set; }

		public ColssionComManger(ComponentManager componentManager)
		{
			_componentManager = componentManager;
			_quadTree = new QuadTree(_componentManager, 0, new Microsoft.Xna.Framework.Rectangle(0, 0, 300, 600));
		}


		public void Check()
		{
			var colCount = 0;

			//_quadTree.Process(_toCheck);

			foreach (var id in _toCheck)
			{
				foreach(var otherID in _toCheck)
				{
					if(id != otherID)
					{
						var otherEntID = _componentManager.GetComponent<ICollisionCom>(otherID).EntID;
						if (_componentManager.GetComponent<ICollisionCom>(id).Check(otherEntID, otherID))
						{
							System.Diagnostics.Debug.WriteLine("COLLSION BETWEEN {0} and {1}", id, otherID);
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
