using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RandomMovementContolerCom : IContolerCom
	{
		private ComponentManager _componentManager;
		private long _posComID;

		public double Speed { get; set; }

		public RandomMovementContolerCom(ComponentManager componentManager, long posComID)
		{
			_posComID = posComID;
			_componentManager = componentManager;
		}
		public void Update(double deltaTime)
		{
			var curSpeed = Speed * deltaTime;
			Postion2D newPos = _componentManager.GetComponent<IPostionComponet>(_posComID).GetPostion();
			newPos.X += GetSpeedIncrement(curSpeed);
			newPos.Y += GetSpeedIncrement(curSpeed);
			_componentManager.GetComponent<IPostionComponet>(_posComID).SetPostion(newPos);
		}
		private double GetSpeedIncrement(double curSpeed)
		{
			var result = Utils.GetRandomDouble(-curSpeed, curSpeed);
			Console.WriteLine(result);
			return result;
		}
	}
}
