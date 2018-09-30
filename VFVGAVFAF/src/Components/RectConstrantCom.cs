using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RectConstrantCom : IPostionConstrantCom
	{
		private delegate bool CheckCon(Paultangle a, Paultangle B);

		public enum CheckType
		{
			Contains,
			Overlapping
		}

		private EntityManager _entityManager;
		private long _constrantID;

		public bool Inside { get; set; }

		public CheckType Type { get; set; }

		public long EntID { get; set; }

		public RectConstrantCom(long entID, EntityManager entityManager, long constrantID)
		{
			EntID = entID;
			_entityManager = entityManager;
			_constrantID = constrantID;
		}


		public bool Check(Paultangle hitBox)
		{
			//TODO this is a brain fart i don't know the right way
			var constrant = _entityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(_constrantID).Rectangle;

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
