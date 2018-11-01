using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class PostionTrigerCom: IComponent, IStepCom, INeedEnityManger, INeedPostMaster
	{
		public long EntID { get; set; }

		public string HitBoxAlais { get; set; }

		public string ConstrantAlais { get; set; }

		public List<string> GameEvents = new List<string>();

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public void Step(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);

			var constrantCom = ent.GetComponent<IPostionConstrantCom>(ConstrantAlais);
			var hitbox = ent.GetComponent<IHaveHitBoxCom>(HitBoxAlais).HitBox;

			//Console.WriteLine("CONSTRANT {0}: {1}", ConstrantAlais, constrantCom.Check(hitbox));
			if (constrantCom.Check(hitbox))
			{
				GameEvents.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
			}
		}
	}
}
