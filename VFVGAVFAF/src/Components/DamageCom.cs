using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class DamageCom : IColsionGameEventCom
	{
		private ComponentManager _componentManager;
		private long _healthComID;

		public IList<long> CanBeTriggered { get; set; }
		public long ColliedWith { get; set; }
		public double TimeToComeplte { get; set; }
		public int Damage { get; set; }

		public DamageCom(ComponentManager componentManager, long healthComID)
		{
			CanBeTriggered = new List<long>();
			_healthComID = healthComID;
			_componentManager = componentManager;
		}

		public void Action()
		{
			if(CanBeTriggered.Contains(ColliedWith))
			{
				_componentManager.GetComponent<IHealthCom>(_healthComID).HP += Damage;
			}
		}

	}
}
