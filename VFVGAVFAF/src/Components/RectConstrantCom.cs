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
		private delegate bool CheckCon(Rectangle a, Rectangle B);

		public enum CheckType
		{
			Contains,
			Overlapping
		}

		private ComponentManager _componentManager;
		private long _constrantID;

		public bool Inside { get; set; }

		public CheckType Type { get; set; }

		public RectConstrantCom(ComponentManager componentManager, long constrantID)
		{
			_componentManager = componentManager;
			_constrantID = constrantID;
		}


		public bool Check(Rectangle hitBox)
		{
			//TODO this is a brain fart i don't know the right way
			var constrant = _componentManager.GetComponent<RectPosCom>(_constrantID).Rectangle;

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
