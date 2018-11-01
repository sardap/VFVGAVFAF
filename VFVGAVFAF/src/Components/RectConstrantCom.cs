using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class RectConstrantCom : IPostionConstrantCom, INeedEnityManger, IHaveAlias
	{
		public long EntID { get; set; }

		public bool LastResult { get; set; }

		public string RectPosAlais { get; set; }

		public bool Inside { get; set; }

		public CheckType Type { get; set; }

		public string Alias { get; set; }

		public EntityManager EntityManager { get; set; }

		public RectConstrantCom(string rectPosAlais)
		{
			RectPosAlais = rectPosAlais;
			LastResult = false;
		}

		public RectConstrantCom()
		{
			LastResult = false;
		}

		public bool Check(Paultangle hitBox)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			//TODO this is a brain fart i don't know the right way
			var constrant = ent.GetComponent<IHaveHitBoxCom>(RectPosAlais).HitBox;

			var result = Utils.Check(hitBox, constrant, Type, Inside);

			LastResult = result;

			return result;
		}
	}
}
