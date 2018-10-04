using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class DamageCom : IColsionGameEventCom, INeedEnityManger
	{
		public EntityManager EntityManager { get; set; }
		private string HealthComAlais { get; set; }

		public long EntID { get; set; }
		public IColInfo ColliedWith { get; set; }
		public double TimeToComplete { get; set; }
		public double Cooldown { get; set; }
		public int Damage { get; set; }
		public string Alias { get; set; }

		public DamageCom()
		{
		}

		public void Action()
		{
			var otherEnt = EntityManager.GetEntiy<GameObject>(ColliedWith.EntID);

			if (otherEnt.HasComType<IHealthCom>())
			{
				var otherEntHPCom = otherEnt.GetFirstComponent<IHealthCom>();
				Console.WriteLine("CUR HP {0}, AFTER DAMAGE {1}", otherEntHPCom.HP, otherEntHPCom.HP + Damage);
				otherEntHPCom.HP += Damage;
			}
		}
	}
}
