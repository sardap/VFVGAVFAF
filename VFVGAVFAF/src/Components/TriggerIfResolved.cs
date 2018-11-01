using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class TriggerIfResolved: IStepCom, INeedEnityManger, INeedPostMaster
	{
		public long EntID { get; set; }

		public List<string> ToResolve { get; set; }

		public List<string> Events { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Step(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);

			if(ToResolve.All(i => ent.GetComponent<IResolveBool>(i).Resolve()))
			{
				Events.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
			}
		}
	}
}
