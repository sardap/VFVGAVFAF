using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class PatrolContorlerCom : IContolerCom, INeedEnityManger, INeedPostMaster, IHaveAlias
	{
		private bool _movingTowardsEnd = true;

		public long EntID { get; set; }
		public EntityManager EntityManager { get; set; }
		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }
		public string Alias { get; set; }
		public List<string> TriggeredAtTarget { get; set; }
		public Postion2D Start { get; set; }
		public Postion2D End { get; set; }
		public long Speed { get; set; }
		public double Radius { get; set; }
		public string PosAlais { get; set; }

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
			var curInc = IncrmnetForPoint(curPos, target, deltaTime);

			curPos += curInc;
			posCom.SetPostion(curPos);


			if (curPos.WithinRadius(target, Radius))
			{
				TriggeredAtTarget.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
				_movingTowardsEnd = !_movingTowardsEnd;
			}
		}

		private Postion2D IncrmnetForPoint(Postion2D curPos, Postion2D target, double deltaTime)
		{
			var distance = Math.Sqrt(Math.Pow(target.X - curPos.X, 2) + Math.Pow(target.Y - curPos.Y, 2));
			var directionX = (target.X - curPos.X) / distance;
			var directionY = (target.Y - curPos.Y) / distance;

			var result = new Postion2D(directionX * Speed * deltaTime, directionY * Speed * deltaTime);

			return result;
		}
	}
}
