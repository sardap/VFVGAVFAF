using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src
{
	class KeyboardAction
	{
		public Keys Key { get; set; }

		public KeyStates State { get; set; }

		public List<string> GameEvents { get; set; }

		public KeyboardAction()
		{
		}

		public bool Resolve(KeyboardInputManger keyboardInputManger)
		{
			switch (State)
			{
				case KeyStates.Up:
					return !keyboardInputManger.KeyPressed[Key];
				case KeyStates.Down:
					return keyboardInputManger.KeyPressed[Key];
				case KeyStates.Clicked:
					return keyboardInputManger.KeyClicked[Key];
			}

			throw new NotImplementedException();
		}
	}
}
