using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Json;
using VFVGAVFAF.src.Components;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Components
{
	class GunCom: IGameEventCom, INeedGamesObjectFactory, INeedBlueprintManger, INeedEnityManger, INeedSenceManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public string BulletBlueprint { get; set; }

		public string PostionAlais { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public GameObjectFactory GameObjectFactory { get; set; }

		public EntiyBlueprintManger EntiyBlueprintManger { get; set; }

		public EntityManager EntityManager { get; set; }

		public ISenceManger SenceManger { get; set; }


		public void Action()
		{
			var postionCom = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IPostionComponet>(PostionAlais);

			var nextEntry = EntiyBlueprintManger.Get(BulletBlueprint);
			nextEntry.GetbyAlais<RectPosCom>("pos").Paultangle.Postion2D = new Postion2D(postionCom.GetPostion());

			SenceManger.AddToProcessed(GameObjectFactory.AddCreatedGameObject(nextEntry));
		}
	}
}
