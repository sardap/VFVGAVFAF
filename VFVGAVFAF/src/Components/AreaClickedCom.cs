using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class AreaClickedCom: IContolerCom, INeedEnityManger, INeedPostMaster, INeedMouseManger
	{
		public long EntID { get; set; }

		public string HitBoxAlias { get; set; }

		public List<string> Events { get; set; }

		public MouseStates MouseState { get; set; }

		public MouseButtons MouseButton { get; set; }

		public EntityManager EntityManager { get; set; }

		public MouseManger MouseManger { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public void Update(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var hitBoxCom = ent.GetComponent<IHaveHitBoxCom>(HitBoxAlias);

			bool result;

			if(MouseState == MouseStates.Up || MouseState == MouseStates.Down)
			{
				result = MouseManger.ButtonStates[MouseButton] == MouseState;
			}
			else if(MouseState == MouseStates.Clicked)
			{
				result = MouseManger.ButtonClicked[MouseButton];
			}
			else
			{
				throw new NotImplementedException();
			}

			if(result)
			{
				if (hitBoxCom.HitBox.Conatins(MouseManger.Postion))
				{
					Events.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
				}
			}
		}
	}
}
