using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		public Dictionary<string, List<string>> GameEventComs { get; set; }

		[JsonIgnore]
		public Paultangle HitBox
		{
			get
			{
				return EntityManager.GetEntiy<GameObject>(EntID).GetComponent<RectPosCom>(RectPosAlais).Rectangle;
			}
		}

		public RectCollisionCom()
		{
			GameEventComs = new Dictionary<string, List<string>>();
		}

		public bool Check(long otherEntID, long otherID)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var hitBox = ent.GetComponent<RectPosCom>(RectPosAlais); // Safe
			var otherEnt = EntityManager.GetEntiy<GameObject>(otherEntID);
			var otherCom = otherEnt.GetComponent<ICollisionCom>(otherID); // Safe
			var otherHitBox = otherCom.HitBox; // Not Safe
			var collied = hitBox.Rectangle.Intersects(otherHitBox);
			if (collied) // Note Safe
			{
				foreach(var entry in GameEventComs)
				{
					if(otherEnt.Tags.Any(i => Regex.IsMatch(entry.Key, i)))
					{
						foreach(var id in entry.Value)
						{
							if (ent.GetComponent<IGameEventCom>(id) is IColsionGameEventCom)
							{
								ent.GetComponent<IColsionGameEventCom>(id).ColliedWithTable[otherCom.EntID] = new ColInfo(otherEntID);
							}

							GameEvenetPostMaster.Add(ent.GetIdForAlais(id), otherCom.EntID);
						}
						
					}
				}
			}

			return collied;
		}
	}
}