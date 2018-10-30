using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class DoubleValueUpdateCom: IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public double IncmrenetChange { get; set; }

		public string ValueAlais { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var com = ent.GetComponent<DoubleValueCom>(ValueAlais);

			com.Value += IncmrenetChange;

		}
	}
}
