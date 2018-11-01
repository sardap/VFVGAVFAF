using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Components
{
	class LoadComBlueprintCom: IGameEventCom, INeedGamesObjectFactory, INeedBlueprintManger, INeedSenceManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public string BlueprintName { get; set; }

		public ISenceManger SenceManger { get; set; }

		public BlueprintManger EntiyBlueprintManger { get; set; }

		public GameObjectFactory GameObjectFactory { get; set; }

		public void Action()
		{
			var nextEntry = EntiyBlueprintManger.Get(BlueprintName);

			SenceManger.AddToProcessed(GameObjectFactory.AddCreatedGameObject(nextEntry, new List<IPassValue>()));
		}
	}
}
