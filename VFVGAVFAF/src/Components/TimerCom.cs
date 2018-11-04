using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class TimerCom: IStepCom, IHaveAlias, IValueCom<double>, IValueCom, INeedPostMaster, INeedEnityManger, IHaveFormatString
	{
		public interface IOpreator
		{
			void Initlise(TimerCom timerCom);

			void Calclate(TimerCom timerCom, double deltaTime);

			void Reset(TimerCom timerCom);

			bool Finshed(TimerCom timerCom);
		}

		public class CountUpOpreator: IOpreator
		{
			public void Initlise(TimerCom timerCom)
			{
				timerCom._currentTime = 0;
			}

			public void Calclate(TimerCom timerCom, double deltaTime)
			{
				timerCom._currentTime += deltaTime;
			}

			public void Reset(TimerCom timerCom)
			{
				Initlise(timerCom);
			}

			public bool Finshed(TimerCom timerCom)
			{
				return timerCom._currentTime > timerCom.EndTime;
			}
		}

		public class CountDownOpreator: IOpreator
		{
			public void Initlise(TimerCom timerCom)
			{
				timerCom._currentTime = timerCom.EndTime;
			}

			public void Calclate(TimerCom timerCom, double deltaTime)
			{
				timerCom._currentTime -= deltaTime;
			}

			public void Reset(TimerCom timerCom)
			{
				Initlise(timerCom);
			}

			public bool Finshed(TimerCom timerCom)
			{
				return timerCom._currentTime < 1;
			}

		}

		private double _currentTime;
		private bool _initslise = false;

		public long EntID { get; set; }

		public string Alias { get; set; }

		[JsonRequired]
		public string EndTimeAlias { get; set; }

		public List<string> GameEvents { get; set; }

		[JsonRequired]
		public IOpreator Opreator { get; set; }

		public string FormatString { get; set; }

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

		public TimerCom()
		{
			FormatString = "{0:0.00}";
		}

		public void Step(double deltaTime)
		{
			if(!_initslise)
			{
				_initslise = true;
				Reset();
			}

			Opreator.Calclate(this, deltaTime);

			if(Opreator.Finshed(this))
			{
				Opreator.Reset(this);
				ExcuteEvents();
			}
		}

		public void Reset()
		{
			Opreator.Reset(this);
		}

		private void ExcuteEvents()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			GameEvents.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
		}
	}
}
