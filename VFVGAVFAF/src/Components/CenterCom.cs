using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class CenterCom: IRunXTimesCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public long RunsRemaining { get; set; }
		public string SizeGetterAlais { get; set; }
		public string PostionAlais { get; set; }
		public string BoundsAlais { get; set; }
		public List<ICenterOperation> CenterOperations { get; set; }

		public EntityManager EntityManager { get; set; }

		public CenterCom()
		{
		}

		public void Step(double deltaTime)
		{
			if(RunsRemaining > 0)
			{
				RunsRemaining--;

				var ent = EntityManager.GetEntiy<GameObject>(EntID);
				var size = ent.GetComponent<IGetSizeCom>(SizeGetterAlais).Size();
				var postionCom = ent.GetComponent<IPostionComponet>(PostionAlais);
				var bounds = ent.GetComponent<IHaveHitBoxCom>(BoundsAlais).HitBox;

				var newPos = postionCom.GetPostion();

				foreach (var entry in CenterOperations)
				{
					var result = entry.Calcaute(bounds, new Postion2D(size));

					if (result.X > 0)
					{
						newPos.X = result.X;
					}

					if(result.Y > 0)
					{
						newPos.Y = result.Y;
					}
				}

				postionCom.SetPostion(newPos);
			}
		}
	}
}
