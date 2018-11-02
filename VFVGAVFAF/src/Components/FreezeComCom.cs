using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class FreezeComCom: IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public List<string> ToFreezeAlias { get; set; }

		public List<MangersEnum> Mangers { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			ToFreezeAlias.ForEach(i => ent.DisableCom(ent.GetIdForAlais(i), Mangers));
		}
	}
}
