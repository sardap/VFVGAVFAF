using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RectConstrantCom : IPostionConstrantCom, INeedEnityManger, IHaveAlias
	{
		private delegate bool CheckCon(Paultangle a, Paultangle B);

		public enum CheckType
		{
			Contains,
			Overlapping
		}

		public long EntID { get; set; }

		public EntityManager EntityManager { get; set; }

		public string RectPosAlais { get; set; }

		public bool Inside { get; set; }

		public CheckType Type { get; set; }

		public string Alias { get; set; }


		public RectConstrantCom(string rectPosAlais)
		{
			RectPosAlais = rectPosAlais;
		}

		public RectConstrantCom()
		{
		}

		public bool Check(Paultangle hitBox)
		{
			//TODO this is a brain fart i don't know the right way
			var constrant = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(RectPosAlais).Rectangle;

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

			if (Inside)
				return d(hitBox, constrant);
			return !d(hitBox, constrant);
		}
	}
}
