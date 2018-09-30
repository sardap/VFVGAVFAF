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
		private EntityManager _entityManager;
		private long _rectPosComID;

		public long EntID { get; set; }
		public Paultangle GetHitBox
		{
			get
			{
				return _entityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(_rectPosComID).Rectangle;
			}
		}
		public List<long> GameEventComs { get; set; }

		public RectCollisionCom(long entID, EntityManager entityManager, IGameEvenetPostMaster gameEvenetPostMaster, long rectPosComID)
		{
			EntID = entID;
			_entityManager = entityManager;
			_gameEvenetPostMaster = gameEvenetPostMaster;
			_rectPosComID = rectPosComID;
			GameEventComs = new List<long>();
		}

		public void Check(long otherEntID, long otherID)
		{
			var ent = _entityManager.GetEntiy<GameObject>(EntID);
			var hitBox = ent.GetComponent<RectPosCom>(_rectPosComID); // Safe
			var otherEnt = _entityManager.GetEntiy<GameObject>(otherEntID);
			var otherCom = otherEnt.GetComponent<ICollisionCom>(otherID); // Safe
			var otherHitBox = otherCom.GetHitBox; // Not Safe
			if (hitBox.Rectangle.Intersects(otherHitBox)) // Note Safe
			{
				foreach(var gameEventID in GameEventComs)
				{
					if(ent.GetComponent<IGameEventCom>(gameEventID) is IColsionGameEventCom)
					{
						ent.GetComponent<IColsionGameEventCom>(gameEventID).ColliedWith.Push(new ColInfo(otherEntID));
					}

					_gameEvenetPostMaster.Add(gameEventID);
				}
			}
		}
	}
}
