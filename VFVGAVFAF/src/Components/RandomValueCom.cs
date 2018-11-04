using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RandomValueCom<T>: IValueCom<T>, IComponent, IHaveAlias, IGameEventCom
	{
		protected T _currentRandom;

		public long EntID { get; set; }

		[JsonRequired]
		public string Alias { get; set; }

		public double TimeToComplete { get; }

		public double Cooldown { get; }

		[JsonRequired]
		public List<T> Values { get; set; }

		[JsonIgnore]
		public T Value
		{
			get
			{
				if(_currentRandom == null)
				{
					_currentRandom = GetRandomValue();
				}

				return  _currentRandom;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public dynamic DValue
		{
			get
			{
				return Value;
			}

			set
			{
				Value = value;
			}
		}

		public virtual T GetRandomValue()
		{
			if(_currentRandom != null)
			{
				T resuslt;

				do
				{
					resuslt = Utils.RandomElement(Values);
				} while (resuslt.GetHashCode() == _currentRandom.GetHashCode());

				return resuslt;
			}

			return Utils.RandomElement(Values);
		}

		public void Action()
		{
			_currentRandom = GetRandomValue();
		}
	}

	class RandomTextCom : RandomValueCom<string>, INeedPostMaster, INeedEnityManger
	{
		public List<string> TextAlignAlias { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public EntityManager EntityManager { get; set; }

		public override string GetRandomValue()
		{
			_currentRandom = base.GetRandomValue();

			var ent = EntityManager.GetEntiy<GameObject>(EntID);

			TextAlignAlias.ForEach(i => ent.GetComponent<ITextAlign>(i).Align());

			return _currentRandom;
		}
	}
}
