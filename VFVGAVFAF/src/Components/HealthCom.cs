using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class HealthCom : IHealthCom, IStepCom
	{
		public enum HPOps
		{
			Equal,
			Greater,
			Less
		}

		public struct HPTrigger
		{
			public HPOps Opreator { get; set; }
			public int Value { get; set; }
			public IList<long> GameEvents { get; set; }
		}

		private ComponentManager _componentManager;

		public int HP { get; set; }
		public int MaxHP { get; set; }
		public int StartingHP { get; set; }

		public IList<HPTrigger> Evenets { get; set; }

		public HealthCom(ComponentManager componentManager, int hp, int maxHP, int startingHP)
		{
			_componentManager = componentManager;
			HP = HP;
			MaxHP = maxHP;
			StartingHP = startingHP;
			Evenets = new List<HPTrigger>();
		}

		public void Step(double deltaTime)
		{
			foreach(var hpTrigger in Evenets)
			{
				if(ResolveOp(hpTrigger.Opreator, hpTrigger.Value))
				{
					foreach(var gameEventID in hpTrigger.GameEvents)
					{
						_componentManager.GetComponent<IGameEventCom>(gameEventID).Action();
					}
				}
			}
		}

		private bool ResolveOp(HPOps op, int value)
		{
			switch(op)
			{
				case HPOps.Equal:
					return HP == value;
				case HPOps.Greater:
					return HP > value;
				case HPOps.Less:
					return HP < value;
			}

			throw new NotImplementedException();
		}
	}
}
