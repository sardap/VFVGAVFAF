using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class MoveDirectionCom: IContolerCom, IComponent, INeedEnityManger
	{
		public long EntID { get; set; }

		public string PostionAlais { get; set; }

		public Postion2D Direction { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Update(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var posCom = ent.GetComponent<IPostionComponet>(PostionAlais);

			var nextPos = posCom.GetPostion();
			nextPos += Direction * deltaTime;

			posCom.SetPostion(nextPos);

		}

	}
}
