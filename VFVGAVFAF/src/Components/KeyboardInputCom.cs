using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class KeyboardInputCom : IContolerCom
	{
		private EntityManager _entityManager;
		private long _posComID;

		public long EntID { get; set; }

		public KeyboardInputCom(long entID, EntityManager entityManager, long posComID)
		{
			EntID = entID;
			_posComID = posComID;
			_entityManager = entityManager;
		}

		public void Update(double deltaTime)
		{
			var posCom = _entityManager.GetEntiy<GameObject>(EntID).GetComponent<IPostionComponet>(_posComID);
			var position = posCom.GetPostion();
			var oldPostion = new Postion2D(position.X, position.Y);

			var state = Keyboard.GetState();

			float speed = 500 * (float)deltaTime;

			if (state.IsKeyDown(Keys.Right))
				position.X += speed;
			if (state.IsKeyDown(Keys.Left))
				position.X -= speed;
			if (state.IsKeyDown(Keys.Up))
				position.Y -= speed;
			if (state.IsKeyDown(Keys.Down))
				position.Y += speed;

			if(position.X != oldPostion.X || position.Y != oldPostion.Y)
				posCom.SetPostion(position);
		}
	}
}
