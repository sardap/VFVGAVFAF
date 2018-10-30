using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class ValueChangeListnerCom<T> : IStepCom, INeedEnityManger, INeedPostMaster where T : IComparable<T>, IComparable
	{
		public enum Ops
		{
			Equal = 0,
			Greater = 1,
			Less = 2
		}

		public interface ITrigger
		{
			bool Resolve(T value);
			IList<string> GameEvents { get; }

		}

		public class ChangeTrigger : ITrigger
		{
			private T _oldVal;
			public IList<string> GameEvents { get; set; }

			public bool Resolve(T value)
			{
				T oldVal = _oldVal;
				_oldVal = value;
				return oldVal.CompareTo(value) == 0;
			}
		}

		public class OpTrigger : ITrigger
		{
			public Ops Opreator { get; set; }
			public T Value { get; set; }
			public IList<string> GameEvents { get; set; }

			public bool Resolve(T value)
			{
				int result = value.CompareTo(Value);

				switch (Opreator)
				{
					case Ops.Equal:
						return result == 0;
					case Ops.Greater:
						return result > 0;
					case Ops.Less:
						return result < 0;
				}

				throw new NotImplementedException();
			}
		}

		public long EntID { get; set; }

		public string ToWatchAlais { get; set; }

		public IList<ITrigger> Triggers { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public ValueChangeListnerCom()
		{
			Triggers = new List<ITrigger>();
		}

		public void Step(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var valCom = ent.GetComponent<IValueCom<T>>(ToWatchAlais);

			foreach (var trigger in Triggers)
			{
				if (trigger.Resolve(valCom.Value))
				{
					foreach (var gameEventID in trigger.GameEvents)
					{
						GameEvenetPostMaster.Add(EntityManager.GetEntiy<GameObject>(EntID).GetIdForAlais(gameEventID));
					}
				}
			}

		}
	}
}
