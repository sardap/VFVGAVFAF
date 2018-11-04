using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface IValueUpdateCom<T>: IComponent, IGameEventCom
	{
		[JsonRequired]
		T IncmrenetChange { get; set; }

		[JsonRequired]
		string ValueAlais { get; set; }
	}

	class ValueUpdateCom<T>: IValueUpdateCom<T>, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public T IncmrenetChange { get; set; }

		public string ValueAlais { get; set; }

		public EntityManager EntityManager { get; set; }

		public virtual void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var com = ent.GetComponent<IValueCom<T>>(ValueAlais);

			com.DValue += IncmrenetChange;
		}
	}

	class DoubleValueUpdateCom : ValueUpdateCom<double> { }
}
