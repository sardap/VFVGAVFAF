using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class KeyboardInputCom: IContolerCom, INeedEnityManger, INeedPostMaster
	{
		public long EntID { get; set; }

		public string RectPosAlais { get; set; }

		public EntityManager EntityManager { get; set; }

		public int Speed { get; set; }

		public Dictionary<Keys, List<string>> Actions { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }


		public KeyboardInputCom(long entID, EntityManager entityManager)
		{
			EntID = entID;
			EntityManager = entityManager;
		}

		public KeyboardInputCom()
		{
			Actions = new Dictionary<Keys, List<string>>();
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
				if (state.IsKeyDown(entry.Key))
				{
					entry.Value.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
				}
			}
		}
	}
}
