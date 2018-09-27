using System;
using System.Collections.Concurrent;
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
		public ConcurrentStack<long> ColliedWith { get; set; }
		public double TimeInbetweenRuns { get; set; }
		public int Damage { get; set; }

		public DamageCom(ComponentManager componentManager, long healthComID)
		{
			CanBeTriggered = new List<long>();
			_healthComID = healthComID;
			_componentManager = componentManager;
			ColliedWith = new ConcurrentStack<long>();
		}

		public void Action()
		{
			if(ColliedWith.Any(i => CanBeTriggered.Contains(i)))
			{
				_componentManager.GetComponent<IHealthCom>(_healthComID).HP += Damage;
			}
		}

	}
}
