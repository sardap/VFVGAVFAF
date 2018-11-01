using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Managers
{
	public enum MouseButtons
	{
		Left = 0,
		Middle = 1,
		Right = 2
	}

	public enum MouseStates
	{
		Up = 0,
		Down = 1,
		Clicked = 2
	}

	class MouseManger
	{
		private Dictionary<MouseButtons, MouseStates> _lastStates = new Dictionary<MouseButtons, MouseStates>();

		private Postion2D _postion2D;

		public Dictionary<MouseButtons, MouseStates> ButtonStates { get; set; }

		public Dictionary<MouseButtons, bool> ButtonClicked { get; set; }

		public MouseManger()
		{
			ButtonStates = new Dictionary<MouseButtons, MouseStates>()
			{
				{ MouseButtons.Left, MouseStates.Up },
				{ MouseButtons.Right, MouseStates.Up },
				{ MouseButtons.Middle, MouseStates.Up }
			};

			ButtonClicked = new Dictionary<MouseButtons, bool>()
			{
				{ MouseButtons.Left, false },
				{ MouseButtons.Right, false },
				{ MouseButtons.Middle, false }
			};

			_lastStates = new Dictionary<MouseButtons, MouseStates>()
			{
				{ MouseButtons.Left, MouseStates.Up },
				{ MouseButtons.Right, MouseStates.Up },
				{ MouseButtons.Middle, MouseStates.Up }
			};

		}

		public Postion2D Postion { get { return _postion2D; } }

		public void Step()
		{
			var mouseState = Mouse.GetState();

			_postion2D = new Postion2D(mouseState.Position.X, mouseState.Position.Y);

			foreach (var key in ButtonStates.Keys.ToArray())
			{
				MouseStates status;

				switch (key)
				{
					case MouseButtons.Left:
						status = (MouseStates)mouseState.LeftButton;
						break;
					case MouseButtons.Middle:
						status = (MouseStates)mouseState.MiddleButton;
						break;
					case MouseButtons.Right:
						status = (MouseStates)mouseState.RightButton;
						break;
					default:
						throw new NotImplementedException();
				}

				ButtonStates[key] = status;
			}

			foreach(var key in ButtonClicked.Keys.ToArray())
			{
				ButtonClicked[key] = _lastStates[key] == MouseStates.Up && ButtonStates[key] == MouseStates.Down;
				_lastStates[key] = ButtonStates[key];
			}
		}
	}
}
