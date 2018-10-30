using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class IntervalTriggerCom : IStepCom, INeedEnityManger, INeedPostMaster
	{
		private double _timeRemaing = 0d;

		public long EntID { get; set; }

		public double Cooldown { get; set; }

		public List<string> EventAlais { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }


		public void Step(double deltaTime)
		{
			_timeRemaing -= deltaTime;

			if(_timeRemaing < 0)
			{
				var ent = EntityManager.GetEntiy<GameObject>(EntID);

				EventAlais.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));

				_timeRemaing = Cooldown;
			}
		}

	}
}
