using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class DamageCom : IColsionGameEventCom, INeedEnityManger, IRegsterForEntDest
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public long ActiveID { get; set; }

		public Dictionary<long, IColInfo> ColliedWithTable { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public int Damage { get; set; }

		public EntityManager EntityManager { get; set; }


		public DamageCom()
		{
			ColliedWithTable = new Dictionary<long, IColInfo>();
		}

		public void Notfiy(long id)
		{
			long? toRemove = null;

			foreach(var i  in ColliedWithTable)
			{
				if(i.Value.EntID == id)
				{
					toRemove = i.Key;
				}
			}

			if(toRemove != null)
				ColliedWithTable.Remove((long)toRemove);
		}


		public void Action()
		{
			var otherEnt = EntityManager.GetEntiy<GameObject>(ColliedWithTable[ActiveID].EntID);

			if (otherEnt.HasComType<IHealthCom>())
			{
				var otherEntHPCom = otherEnt.GetFirstComponent<IHealthCom>();
				Console.WriteLine("CUR HP {0}, AFTER DAMAGE {1}", otherEntHPCom.HP, otherEntHPCom.HP + Damage);
				otherEntHPCom.HP += Damage;
			}
			else
			{
				Console.WriteLine("ENT: {0} NO HEALTH COM", ColliedWithTable[ActiveID].EntID);
			}
		}
	}
}
