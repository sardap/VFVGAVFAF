using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RandomMovementContolerCom : IContolerCom, INeedEnityManger
	{

		public long EntID { get; set; }
		public EntityManager EntityManager { get; set; }
		public string RectPosAlais { get; set; }
		public double Speed { get; set; }

		public RandomMovementContolerCom()
		{
		}

		public void Update(double deltaTime)
		{
			var curSpeed = Speed * deltaTime;
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			Postion2D newPos = ent.GetComponent<IPostionComponet>(RectPosAlais).GetPostion();
			newPos.X += GetSpeedIncrement(curSpeed);
			newPos.Y += GetSpeedIncrement(curSpeed);
			EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IPostionComponet>(RectPosAlais).SetPostion(newPos);
		}

		private double GetSpeedIncrement(double curSpeed)
		{
			var result = Utils.GetRandomDouble(-curSpeed, curSpeed);
			return result;
		}
	}
}
