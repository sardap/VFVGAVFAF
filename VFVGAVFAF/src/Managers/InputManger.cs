using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;

namespace VFVGAVFAF.src
{
	class InputManger
	{
		private ComponentManager _componentManager;

		private List<long> _regssiteredIDs = new List<long>();

		public InputManger(ComponentManager componentManager)
		{
			_componentManager = componentManager;
		}

		public void Update(double deltaTime)
		{
			_regssiteredIDs.ForEach(i => _componentManager.GetComponent<IContolerCom>(i).Update(deltaTime));
		}

		public void RegsiterComponet(long comID)
		{
			_regssiteredIDs.Add(comID);
		}
	}
}
