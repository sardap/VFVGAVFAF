﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class HealthCom : IHealthCom, IStepCom, INeedPostMaster, INeedEnityManger
	{
		public enum HPOps
		{
			Equal,
			Greater,
			Less
		}

		public interface IHPTrigger
		{
			bool Resolve(HealthCom healthCom);
			IList<string> GameEvents { get; }

		}

		public class HPChangeTrigger : IHPTrigger
		{
			private int _oldVal;
			public IList<string> GameEvents { get; set; }

			public bool Resolve(HealthCom healthCom)
			{
				int oldVal = _oldVal;
				_oldVal = healthCom.HP;
				return oldVal != healthCom.HP;
			}
		}

		public class HPOpTrigger : IHPTrigger
		{
			public HPOps Opreator { get; set; }
			public int Value { get; set; }
			public IList<string> GameEvents { get; set; }

			public bool Resolve(HealthCom healthCom)
			{
				switch (Opreator)
				{
					case HPOps.Equal:
						return healthCom.HP == Value;
					case HPOps.Greater:
						return healthCom.HP > Value;
					case HPOps.Less:
						return healthCom.HP < Value;
				}

				throw new NotImplementedException();
			}
		}

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public long EntID { get; set; }
		public int HP { get; set; }
		public int MaxHP { get; set; }
		public int StartingHP { get; set; }

		public IList<IHPTrigger> Evenets { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Step(double deltaTime)
		{
			foreach(var hpTrigger in Evenets)
			{
				if(hpTrigger.Resolve(this))
				{
					foreach(var gameEventID in hpTrigger.GameEvents)
					{
						GameEvenetPostMaster.Add(EntityManager.GetEntiy<GameObject>(EntID).GetIdForAlais(gameEventID));
					}
				}
			}
		}
	}
}
