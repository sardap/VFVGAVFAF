using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RandomMovementContolerCom : IContolerCom
	{
		private EntityManager _entityManager;
		private long _posComID;

		public long EntID { get; set; }
		public double Speed { get; set; }

		public RandomMovementContolerCom(long entID, EntityManager entityManager, long posComID)
		{
			EntID = entID;
			_entityManager = entityManager;
			_posComID = posComID;
		}
		public void Update(double deltaTime)
		{
			var curSpeed = Speed * deltaTime;
			var ent = _entityManager.GetEntiy<GameObject>(EntID);
			Postion2D newPos = ent.GetComponent<IPostionComponet>(_posComID).GetPostion();
			newPos.X += GetSpeedIncrement(curSpeed);
			newPos.Y += GetSpeedIncrement(curSpeed);
			_entityManager.GetEntiy<GameObject>(EntID).GetComponent<IPostionComponet>(_posComID).SetPostion(newPos);
		}
		private double GetSpeedIncrement(double curSpeed)
		{
			var result = Utils.GetRandomDouble(-curSpeed, curSpeed);
			return result;
		}
	}
}
