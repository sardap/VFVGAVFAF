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
		private delegate bool CheckCon(Paultangle a, Paultangle B);

		public enum CheckType
		{
			Contains,
			Overlapping
		}

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
			var constrant = ent.GetComponent<IHaveHitBoxCom>(RectPosAlais).GetHitBox;

			CheckCon d;

			switch (Type)
			{
				case CheckType.Contains:
					d = new CheckCon(Utils.AInsideB);
					break;
				case CheckType.Overlapping:
					d = new CheckCon(Utils.AOverlapB);
					break;
				default:
					throw new NotImplementedException();
			}

			bool result;

			if (Inside)
				result = d(hitBox, constrant);
			else
				result = !d(hitBox, constrant);


			LastResult = result;

			return result;
		}
	}
}
