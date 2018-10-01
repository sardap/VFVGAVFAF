using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RespawnCom : IGameEventCom
	{
		private EntityManager _entityManger;
		private long _posComID;

		public long EntID { get; set; }
		public double TimeToComplete { get; set; }
		public double Cooldown { get; set; }

		public RespawnCom(long entID, EntityManager entityManger, long posComID)
		{
			EntID = entID;
			_posComID = posComID;
			_entityManger = entityManger;
		}

		public void Action()
		{
			var ent = _entityManger.GetEntiy<GameObject>(EntID);
			ent.GetComponent<IPostionComponet>(_posComID).ResetPostion();
			var healthCom = ent.GetFirstComponent<IHealthCom>();
			healthCom.HP = healthCom.StartingHP;
		}
	}
}
