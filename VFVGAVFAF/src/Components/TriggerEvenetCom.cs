using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class TriggerEvenetCom: IComponent, INeedPostMaster, IStepCom, INeedEnityManger, IRunXTimesCom
	{
		private bool _ran = false;

		public long EntID { get; set; }

		public long RunsRemaining { get { return 0; } set { } }

		public List<string> EventAlais { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public TriggerEvenetCom()
		{
			EventAlais = new List<string>();
		}

		public void Step(double deltaTime)
		{
			if(!_ran)
			{
				EventAlais.ForEach(i => 
					GameEvenetPostMaster.Add(EntityManager.GetEntiy<GameObject>(EntID).GetIdForAlais(i))
				);
				_ran = true;
			}
		}

	}
}
