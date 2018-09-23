using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Managers
{
	class StepManager : IStepManager
	{
		private ISet<long> _renderList = new HashSet<long>();

		public ComponentManager ComponentManager { get; set; }


		public void Regsiter(long id)
		{
			_renderList.Add(id);
		}

		public void UnRegsiter(long id)
		{
			_renderList.Remove(id);
		}

		public void Step(double deltaTime)
		{
			foreach(var id in _renderList)
			{
				ComponentManager.GetComponent<IStepCom>(id).Step(deltaTime);
			}
		}
	}
}
