using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class KeyboardInputCom: IContolerCom, INeedEnityManger, INeedPostMaster, INeedKeyboardInputManger
	{
		public class KeyboardTrigger
		{
			public Keys Key { get; set; }

			public KeyStates State { get; set; }

			public List<string> GameEvents { get; set; }

			public KeyboardTrigger()
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

		public long EntID { get; set; }

		public string RectPosAlais { get; set; }

		public int Speed { get; set; }

		public IList<KeyboardTrigger> Actions { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public KeyboardInputManger KeyboardInputManger { get; set; }

		public KeyboardInputCom()
		{
		}

		public void Update(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var posCom = ent.GetComponent<IPostionComponet>(RectPosAlais);
			var position = posCom.GetPostion();
			var oldPostion = new Postion2D(position.X, position.Y);

			var state = Keyboard.GetState();

			float curSpeed = Speed * (float)deltaTime;

			if (state.IsKeyDown(Keys.Right))
				position.X += curSpeed;
			if (state.IsKeyDown(Keys.Left))
				position.X -= curSpeed;
			if (state.IsKeyDown(Keys.Up))
				position.Y -= curSpeed;
			if (state.IsKeyDown(Keys.Down))
				position.Y += curSpeed;

			if(position.X != oldPostion.X || position.Y != oldPostion.Y)
				posCom.SetPostion(position);

			foreach(var entry in Actions)
			{
				if (entry.Resolve(KeyboardInputManger))
				{
					entry.GameEvents.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
				}
			}
		}
	}
}
