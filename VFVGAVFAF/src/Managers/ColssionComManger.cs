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

		public ComponentManager ComponentManager { get; set; }

		public void Check()
		{
			foreach (var i in _toCheck)
			{
				foreach (var j in _toCheck)
				{
					if(i != j)
					{
						ComponentManager.GetComponent<ICollisionCom>(i).Check(j);
					}
				}
			}
		}

		public void Regsiter(long comID)
		{
			if (ComponentManager.GetComponent<ICollisionCom>(comID) != null)
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
