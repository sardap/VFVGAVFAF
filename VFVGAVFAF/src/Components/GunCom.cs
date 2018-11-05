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

		public DirectionTypes ExitDirection { get; set; }

		public GameObjectFactory GameObjectFactory { get; set; }

		public BlueprintManger EntiyBlueprintManger { get; set; }

		public EntityManager EntityManager { get; set; }

		public ISenceManger SenceManger { get; set; }

		public GunCom()
		{
			ExitDirection = DirectionTypes.Left;
		}


		public void Action()
		{
			var postionCom = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IHaveHitBoxCom>(PostionAlais);

			var shooterHitbox = postionCom.HitBox;

			var nextEntry = EntiyBlueprintManger.Get(BulletBlueprint);
			var hitbox = nextEntry.GetbyAlais<RectPosCom>("pos").Paultangle;

			Postion2D nextPostion;

			switch(ExitDirection)
			{
				case DirectionTypes.Up:
					throw new NotImplementedException();
					break;
				case DirectionTypes.Down:
					throw new NotImplementedException();
					break;
				case DirectionTypes.Left:
					nextPostion = new Postion2D
					(
						shooterHitbox.Left - hitbox.Width - 2,
						(shooterHitbox.Y + shooterHitbox.Height / 2) - hitbox.Height / 2
					);
					break;
				case DirectionTypes.Right:
					nextPostion = new Postion2D
					(
						shooterHitbox.Right - hitbox.Width + 3,
						(shooterHitbox.Y + shooterHitbox.Height / 2) - hitbox.Height / 2
					);
					break;
				default:
					throw new NotImplementedException();
			}

			hitbox.Postion2D = nextPostion;

			if(nextEntry.HasAlias("move_direction_com"))
			{
				var moveDirectionCom = nextEntry.GetbyAlais<MoveDirectionCom>("move_direction_com");
				moveDirectionCom.Direction = ExitDirection;
			}

			SenceManger.AddProcessedToMain(GameObjectFactory.AddCreatedGameObject(nextEntry, new List<IPassValue>()));
		}
	}
}
