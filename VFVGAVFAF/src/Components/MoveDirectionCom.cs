using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class MoveDirectionCom: IContolerCom, IComponent, INeedEnityManger
	{
		public enum DirectionTypes
		{
			Up = 0,
			Down = 1,
			Left = 2,
			Right = 3
		}

		public long EntID { get; set; }

		public string PostionAlais { get; set; }

		public DirectionTypes Direction { get; set; }

		public double Speed { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Update(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var posCom = ent.GetComponent<IPostionComponet>(PostionAlais);

			var nextPos = posCom.GetPostion();

			var curSpeed = Speed * deltaTime;

			switch(Direction)
			{
				case DirectionTypes.Up:
					nextPos.Y -= curSpeed;
					break;
				case DirectionTypes.Down:
					nextPos.Y += curSpeed;
					break;
				case DirectionTypes.Left:
					nextPos.X -= curSpeed;
					break;
				case DirectionTypes.Right:
					nextPos.X += curSpeed;
					break;
			}

			posCom.SetPostion(nextPos);

		}

	}
}
