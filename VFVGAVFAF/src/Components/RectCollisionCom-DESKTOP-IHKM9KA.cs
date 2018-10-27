using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class RectCollisionCom : ICollisionCom, INeedEnityManger, INeedPostMaster
	{
		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }
		public EntityManager EntityManager { get; set; }

		public string RectPosAlais { get; set; }
		public long EntID { get; set; }
		public List<string> GameEventComs { get; set; }

		[JsonIgnore]
		public Paultangle GetHitBox
		{
			get
			{
				return EntityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(RectPosAlais).Rectangle;
			}
		}

		public RectCollisionCom()
		{
			GameEventComs = new List<string>();
		}

		public bool Check(long otherEntID, long otherID)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var hitBox = ent.GetComponent<RectPosCom>(RectPosAlais); // Safe
			var otherEnt = EntityManager.GetEntiy<GameObject>(otherEntID);
			var otherCom = otherEnt.GetComponent<ICollisionCom>(otherID); // Safe
			var otherHitBox = otherCom.GetHitBox; // Not Safe
			var collied = hitBox.Rectangle.Intersects(otherHitBox);
			if (collied) // Note Safe
			{
				foreach(var gameEventAlais in GameEventComs)
				{
					if(ent.GetComponent<IGameEventCom>(gameEventAlais) is IColsionGameEventCom)
					{
						ent.GetComponent<IColsionGameEventCom>(gameEventAlais).ColliedWith = new ColInfo(otherEntID);
					}

					GameEvenetPostMaster.Add(ent.GetIdForAlais(gameEventAlais), otherID);
				}
			}

			return collied;
		}
	}
}