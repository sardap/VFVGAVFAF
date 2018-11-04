using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class AssignValueCom<S, T>: IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public string SourceAlais { get; set; }

		public string TargetAlais { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);

			var source = ent.GetComponent<IValueCom<S>>(SourceAlais);
			var target = ent.GetComponent<IValueCom<T>>(TargetAlais);

			dynamic newVaule = null;

			if(target.Value is string)
			{
				newVaule = source.Value.ToString();
			}
			else
			{
				throw new NotImplementedException();
			}

			target.Value = newVaule;
		}
	}

	class AssignDoubleToStringValueCom : AssignValueCom<double, string> { }

	class AssignIntToStringValueCom : AssignValueCom<int, string> { }

}
