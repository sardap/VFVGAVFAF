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

			if (state.IsKeyDown(Keys.Right))
				position.X += 10;
			if (state.IsKeyDown(Keys.Left))
				position.X -= 10;
			if (state.IsKeyDown(Keys.Up))
				position.Y -= 10;
			if (state.IsKeyDown(Keys.Down))
				position.Y += 10;

			_componentManager.GetComponent<IPostionComponet>(_posComID).Postion = position;
		}
	}
}
