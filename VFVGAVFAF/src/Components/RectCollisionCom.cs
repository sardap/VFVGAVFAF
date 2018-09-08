using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RectCollisionCom : ICollisionCom
	{
		private ComponentManager _componentManager;
		private long _rectPosComID;

		public Rectangle GetHitBox
		{
			get
			{
				return _componentManager.GetComponent<RectPosCom>(_rectPosComID).Rectangle;
			}
		}
		public List<long> GameEventComs { get; set; }

		public RectCollisionCom(ComponentManager componentManager, long rectPosComID)
		{
			_componentManager = componentManager;
			_rectPosComID = rectPosComID;
			GameEventComs = new List<long>();
		}

		public void Check(long otherID)
		{
			var hitBox = _componentManager.GetComponent<RectPosCom>(_rectPosComID);
			var otherHitBox = _componentManager.GetComponent<ICollisionCom>(otherID);
			if (hitBox.Rectangle.Intersects(otherHitBox.GetHitBox))
			{
				GameEventComs.ForEach(i => _componentManager.GetComponent<GoalReachedCom>(i).Action());
			}
		}
	}
}
