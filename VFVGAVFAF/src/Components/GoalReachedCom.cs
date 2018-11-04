using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class GoalReachedCom : IGameEventCom, INeedMinigameManger, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public string GoalResultAlias { get; set; }

		public MinigameManger MinigameManger { get; set; }

		public EntityManager EntityManager { get; set; }


		public GoalReachedCom(long entID)
		{
			EntID = entID;
		}

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var goalResultCom = ent.GetComponent<BoolValueCom>(GoalResultAlias);

			MinigameManger.PlayNext(goalResultCom.Value);
		}
	}
}
