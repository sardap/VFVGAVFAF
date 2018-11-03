using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class GetPathCom: IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public string StartPostionAlias { get; set; }

		public string EndPostionAlias { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var startPosCom = ent.GetComponent<IPostionComponet>(StartPostionAlias);
			var endPosCom = ent.GetComponent<IPostionComponet>(EndPostionAlias);


		}
	}
}
