using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class TriggredIntervalTriggerCom : IntervalTriggerCom, IHaveAlias, IGameEventCom
	{
		private bool _active = false;

		public string Alias { get; set; }

		public double TimeToComplete { get; }

		public void Action()
		{
			_active = true;
		}

		public override void Step(double deltaTime)
		{
			if(_active)
			{
				base.Step(deltaTime);
			}
		}

	}
}
