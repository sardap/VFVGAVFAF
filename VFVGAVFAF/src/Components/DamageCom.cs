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
		private EntityManager _entityManager;
		private long _healthComID;

		public long EntID { get; }
		public ConcurrentStack<IColInfo> ColliedWith { get; set; }
		public double TimeInbetweenRuns { get; set; }
		public int Damage { get; set; }

		public DamageCom(long entID, EntityManager entityManager, long healthComID)
		{
			_entityManager = entityManager;
			_healthComID = healthComID;
			ColliedWith = new ConcurrentStack<IColInfo>();
		}

		public void Action()
		{
			while(ColliedWith.Count > 0)
			{
				ColliedWith.TryPop(out IColInfo next);
				var otherEnt = _entityManager.GetEntiy<GameObject>(next.EntID);

				if (otherEnt.HasComType<IHealthCom>())
				{
					otherEnt.GetFirstComponent<IHealthCom>().HP += Damage;
				}
			}
		}
	}
}
