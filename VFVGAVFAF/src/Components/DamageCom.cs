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

		public long EntID { get; set; }
		public IColInfo ColliedWith { get; set; }
		public double TimeInbetweenRuns { get; set; }
		public int Damage { get; set; }

		public DamageCom(EntityManager entityManager, long healthComID)
		{
			_entityManager = entityManager;
			_healthComID = healthComID;
		}

		public void Action()
		{
			var otherEnt = _entityManager.GetEntiy<GameObject>(ColliedWith.EntID);

			if (otherEnt.HasComType<IHealthCom>())
			{
				var otherEntHPCom = otherEnt.GetFirstComponent<IHealthCom>();
				Console.WriteLine("CUR HP {0}, AFTER DAMAGE {1}", otherEntHPCom.HP, otherEntHPCom.HP + Damage);
				otherEntHPCom.HP += Damage;
			}
		}
	}
}
