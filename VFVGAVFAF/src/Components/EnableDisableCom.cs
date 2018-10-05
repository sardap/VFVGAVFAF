using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class EnableDisableCom : IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }
		public EntityManager EntityManager { get; set; }
		public double TimeToComplete { get; set; }
		public double Cooldown { get; set; }
		public string Alias { get; set; }
		public List<string> ComsToDisable { get; set; }
		public bool NewState { get; set; }

		public EnableDisableCom()
		{
			ComsToDisable = new List<string>();
		}

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);

			ComsToDisable.ForEach(i => ent.ChangeEnableStateCom(EntID, NewState));
		}
	}
}
