using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class MoveToPointCom : IContolerCom, INeedEnityManger, INeedPostMaster, IHaveAlias
	{
		private bool _targetHit = false;

		public long EntID { get; set; }

		public string Alias { get; set; }

		public string PostionAlias { get; set; }

		public List<string> TriggeredAtTarget { get; set; }

		public Postion2D Target { get; set; }

		public long Speed { get; set; }

		public double Radius { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public void Update(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var posCom = ent.GetComponent<IPostionComponet>(PostionAlias);
			var curPos = posCom.GetPostion();
			var target = Target;

			if (!curPos.WithinRadius(target, Radius))
			{
				var curInc = Utils.IncrmnetForPoint(curPos, target, deltaTime, Speed);

				curPos += curInc;
				posCom.SetPostion(curPos);
			}
			else if(!_targetHit)
			{
				_targetHit = true;
				TriggeredAtTarget.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
			}
		}
	}
}
