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
		private IGameEvenetPostMaster _gameEvenetPostMaster;
		private ComponentManager _componentManager;
		private long _rectPosComID;

		public Paultangle GetHitBox
		{
			get
			{
				return _componentManager.GetComponent<RectPosCom>(_rectPosComID).Rectangle;
			}
		}
		public List<long> GameEventComs { get; set; }

		public RectCollisionCom(ComponentManager componentManager, IGameEvenetPostMaster gameEvenetPostMaster, long rectPosComID)
		{
			_componentManager = componentManager;
			_gameEvenetPostMaster = gameEvenetPostMaster;
			_rectPosComID = rectPosComID;
			GameEventComs = new List<long>();
		}

		public void Check(long otherID)
		{
			var hitBox = _componentManager.GetComponent<RectPosCom>(_rectPosComID); // Safe
			var otherCom = _componentManager.GetComponent<ICollisionCom>(otherID); // Safe
			var otherHitBox = otherCom.GetHitBox; // Not Safe
			if (hitBox.Rectangle.Intersects(otherHitBox)) // Note Safe
			{
				foreach(var gameEventID in GameEventComs)
				{
					if(_componentManager.GetComponent<IGameEventCom>(gameEventID) is IColsionGameEventCom)
					{
						_componentManager.GetComponent<IColsionGameEventCom>(gameEventID).ColliedWith.Push(otherID);
					}

					_gameEvenetPostMaster.Add(gameEventID);
				}
			}
		}
	}
}
