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
		private ComponentManager _componentManager;
		private long _posComID;

		public bool BorderStop { get; set; }

		public KeyboardInputCom(ComponentManager componentManager, long posComID)
		{
			_posComID = posComID;
			_componentManager = componentManager;
		}

		public void Update(double deltaTime)
		{
			var position = _componentManager.GetComponent<IPostionComponet>(_posComID).Postion;

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

			_componentManager.GetComponent<IPostionComponet>(_posComID).Postion = position;
		}
	}
}
