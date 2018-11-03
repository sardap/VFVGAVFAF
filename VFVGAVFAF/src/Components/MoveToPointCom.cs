using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Pathfinding;

namespace VFVGAVFAF.src.Components
{
	class MoveToPointCom : IContolerCom, IHaveAlias, INeedEnityManger, INeedPostMaster, INeedGrid
	{
		private Queue<Postion2D> _path = null;
		private Postion2D _subTarget;
		private double _lastUpdate = double.MaxValue;

		public long EntID { get; set; }

		public string Alias { get; set; }

		public string PostionAlias { get; set; }

		public List<string> TriggeredAtTarget { get; set; }

		public Postion2D Target { get; set; }

		public long Speed { get; set; }

		public string ObstaclePattern { get; set; }

		public double PathUpdateInterval { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public Grid Grid { get; set; }

		public void Update(double deltaTime)
		{
			_lastUpdate += deltaTime;

			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var posCom = ent.GetComponent<IPostionComponet>(PostionAlias);
			var curPos = posCom.GetPostion();

			if (_lastUpdate > PathUpdateInterval)
			{
				_path = new Queue<Postion2D>(new AStarSerach().FindPath(Grid, curPos, Target, ObstaclePattern));
				_subTarget = curPos;
				_lastUpdate = 0;
			}

			if(curPos.WithinRadius(_subTarget, 3))
			{
				if(_path.Count > 0)
				{
					_subTarget = _path.Dequeue();
				}
				else
				{
					TriggeredAtTarget.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
				}
			}
			
			var target = _subTarget;

			//System.Diagnostics.Debug.WriteLine("CUR POS {0} TARGET {1}", curPos, target);
			if (!curPos.WithinRadius(target, 3))
			{
				var curInc = Utils.IncrmnetForPoint(curPos, target, deltaTime, Speed);

				curPos += curInc;
				posCom.SetPostion(curPos);
			}
		}
	}
}
