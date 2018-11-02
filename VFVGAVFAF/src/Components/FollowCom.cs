using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class FollowCom: IContolerCom, INeedEnityManger, IHaveAlias, IColsionGameEventCom
	{
		private long? _followingEntID;

		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; }

		public double Cooldown { get; }

		public string PostionAlias { get; set; }

		public long Speed { get; set; }

		public long ActiveID { get; set; }

		public Dictionary<long, IColInfo> ColliedWithTable { get; set; }

		public EntityManager EntityManager { get; set; }

		public FollowCom()
		{
			ColliedWithTable = new Dictionary<long, IColInfo>();
		}

		public void Action()
		{
			var otherEnt = EntityManager.GetEntiy<GameObject>(ColliedWithTable[ActiveID].EntID);

			_followingEntID = ColliedWithTable[ActiveID].EntID;
		}


		public void Update(double deltaTime)
		{
			if(_followingEntID != null)
			{
				var ent = EntityManager.GetEntiy<GameObject>(EntID);
				var posCom = ent.GetComponent<IPostionComponet>(PostionAlias);
				var curPos = posCom.GetPostion();

				var otherEnt = EntityManager.GetEntiy<GameObject>((long)_followingEntID);
				var otherPosCom = otherEnt.GetComponent<IPostionComponet>("pos");
				var target = otherPosCom.GetPostion();

				var curInc = Utils.IncrmnetForPoint(curPos, target, deltaTime, Speed);

				curPos += curInc;
				posCom.SetPostion(curPos);
			}
		}

	}
}
