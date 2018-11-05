using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class TriggerOtherEntCom : IGameEventCom, INeedPostMaster, INeedEnityManger
	{
		public long EntID { get; set; }

		[JsonRequired]
		public string Alias { get; set; }

		public double TimeToComplete { get; }

		public double Cooldown { get; }

		[JsonRequired]
		public string OtherEntAlias { get; set; }

		[JsonRequired]
		public List<string> EventAlais { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(OtherEntAlias);

			EventAlais.ForEach(i =>
				GameEvenetPostMaster.Add(ent.GetIdForAlais(i))
			);
		}
	}
}
