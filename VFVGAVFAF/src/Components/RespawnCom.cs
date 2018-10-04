using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class RespawnCom : IGameEventCom, INeedEnityManger
	{
		public EntityManager EntityManager { get; set; }
		public string PosComAlais { get; set; }

		public long EntID { get; set; }
		public double TimeToComplete { get; set; }
		public double Cooldown { get; set; }
		public string Alias { get; set; }

		public RespawnCom()
		{
		}

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			ent.GetComponent<IPostionComponet>(PosComAlais).ResetPostion();
			var healthCom = ent.GetFirstComponent<IHealthCom>();
			healthCom.HP = healthCom.StartingHP;
		}
	}
}
