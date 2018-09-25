using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RespawnCom : IGameEventCom
	{
		private ComponentManager _componentManager;
		private long _posComID;
		private long _healthComID;

		public double TimeInbetweenRuns { get; set; }

		public RespawnCom(ComponentManager componentManager, long posComID, long healthComID)
		{
			_posComID = posComID;
			_healthComID = healthComID;
			_componentManager = componentManager;
		}

		public void Action()
		{
			_componentManager.GetComponent<IPostionComponet>(_posComID).ResetPostion();
			var healthCom = _componentManager.GetComponent<IHealthCom>(_healthComID);
			healthCom.HP = healthCom.StartingHP;
		}
	}
}
