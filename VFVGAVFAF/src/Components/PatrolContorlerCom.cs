using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class PatrolContorlerCom: IContolerCom, INeedEnityManger, INeedPostMaster, IHaveAlias
	{
		private bool _movingTowardsEnd = true;
		private bool _firstTime = true;

		public long EntID { get; set; }

		public string Alias { get; set; }

		public List<string> TriggeredAtTarget { get; set; }

		[JsonIgnore]
		public Postion2D Start { get; set; }

		public Postion2D End { get; set; }

		public long Speed { get; set; }

		public double Radius { get; set; }

		public string PosAlais { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public PatrolContorlerCom()
		{
			TriggeredAtTarget = new List<string>();
		}

		public void Update(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var posCom = ent.GetComponent<IPostionComponet>(PosAlais);
			var curPos = posCom.GetPostion();
			var target = _movingTowardsEnd ? End : Start;
			var curInc = Utils.IncrmnetForPoint(curPos, target, deltaTime, Speed);

			if (_firstTime)
			{
				Start = curPos;
				_firstTime = false;
			}

			curPos += curInc;
			posCom.SetPostion(curPos);

			if (curPos.WithinRadius(target, Radius))
			{
				TriggeredAtTarget.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
				_movingTowardsEnd = !_movingTowardsEnd;
			}
		}
	}
}
