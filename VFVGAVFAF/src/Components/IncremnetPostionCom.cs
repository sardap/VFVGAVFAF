using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class IncremnetPostionCom: IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; }

		public double Cooldown { get; }

		public string PostionAlais { get; set; }

		public Postion2D IncrmenetChange { get; set; }

		public EntityManager EntityManager { get; set; }


		public void Action()
		{
			var posCom = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IPostionComponet>(PostionAlais);
			var nextPos = posCom.GetPostion();
			nextPos += IncrmenetChange;
			posCom.SetPostion(nextPos);
		}
	}
}
