using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class GrowRectCom : IStepCom, INeedEnityManger
	{
		public long EntID { get; set; }
		public EntityManager EntityManager { get; set; }

		public double Rate { get; set; }
		public string RectPosAlais { get; set; }

		public void Step(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var rectCom = ent.GetComponent<RectPosCom>(RectPosAlais);

			var stepSize = Rate * deltaTime;

			rectCom.Paultangle.Width += stepSize;
			rectCom.Paultangle.Height += stepSize;
		}
	}
}
