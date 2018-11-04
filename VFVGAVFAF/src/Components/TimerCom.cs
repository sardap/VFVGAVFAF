using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class TimerCom: IStepCom, IHaveAlias, IValueCom<double>, IValueCom, INeedPostMaster, INeedEnityManger
	{
		private double _currentTime;

		public long EntID { get; set; }

		public string Alias { get; set; }

		[JsonRequired]
		public string EndTimeAlias { get; set; }

		public List<string> GameEvents { get; set; }

		[JsonIgnore]
		public double EndTime
		{
			get
			{
				return EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IValueCom<double>>(EndTimeAlias).Value;
			}
		}

		[JsonIgnore]
		public double Value { get { return _currentTime; } set { throw new NotSupportedException(); } }

		public dynamic DValue { get { return Value; } set { Value = value; } }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Step(double deltaTime)
		{
			_currentTime += deltaTime;

			if(_currentTime > EndTime)
			{
				_currentTime = 0;
				var ent = EntityManager.GetEntiy<GameObject>(EntID);
				GameEvents.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
			}
		}

		public void Reset()
		{
			_currentTime = 0;
		}
	}
}
