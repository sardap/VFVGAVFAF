using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;
using VFVGAVFAF.src.Managers;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src
{
	class GameObjectFactory
	{
		public enum GameObjects
		{
			SqaurePlayer,
			SqaureGoal,
			EnemeySqaure
		}

		private delegate long CreateGameObject();

		private Dictionary<GameObjects, CreateGameObject> _gameObjectCreators = new Dictionary<GameObjects, CreateGameObject>();

		public ComponentManager ComponentManager { get; set; }
		public EntityManager EntityManager { get; set; }
		public RenderManager RenderManager { get; set; }
		public InputManger InputManger { get; set; }
		public ColssionComManger ColssionManger { get; set; }
		public IStepManager StepManager { get; set; }
		public TextureManager TextureManager { get; set; }
		public SoundManager SoundManager { get; set; }
		public ContentManager Content { get; set; }
		public ISenceManger SenceManger { get; set; }
		public SpriteBatch SpriteBatch { get; set; }
		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public GameObjectFactory()
		{
			_gameObjectCreators.Add(GameObjects.SqaurePlayer, new CreateGameObject(CreateSqaurePlayer));
			_gameObjectCreators.Add(GameObjects.SqaureGoal, new CreateGameObject(CreateSqaureGoal));
			_gameObjectCreators.Add(GameObjects.EnemeySqaure, new CreateGameObject(CreateEnemeySqaure));
		}

		public long CreateObjectOfType(GameObjects gameObjects)
		{
			return _gameObjectCreators[gameObjects]();
		}

		private long CreateSqaure(Rectangle rectangle)
		{
			var entID = EntityManager.CreateEntity(new GameObject(ComponentManager));
			var gameObject = EntityManager.GetEntiy<GameObject>(entID);

			var rectPos = gameObject.AddComponent(new RectPosCom(ComponentManager, rectangle));
			var rectRendCom = gameObject.AddComponent(new RectRendCom(ComponentManager, rectPos)
			{
				Texture = TextureManager.GetTexture(Textures.BLOCK),
				SpriteBatch = SpriteBatch,
				Color = Color.Red
			});
			gameObject.RegsiterToManager(rectRendCom, RenderManager);

			return entID;
		}

		private long _healthComID;
		private long _playerColsionCom;
		private long _speed = 50;
		private int _damage = -10;
		private Rectangle _nextRectSize = new Rectangle(300, 300, 5, 5);
		private Rectangle _bounds = new Rectangle(0, 0, 800, 600);

		private long CreateEnemeySqaure()
		{
			_nextRectSize = new Paultangle(Utils.RandomPostionInBounds(_bounds), 5, 5).ToMonoGameRectangle();

			var entID = CreateSqaure(_nextRectSize);
			var gameObject = EntityManager.GetEntiy<GameObject>(entID);

			var gameBoundsComID = gameObject.AddComponent(new RectPosCom(ComponentManager, _bounds));

			var constrantID = gameObject.AddComponent(
				new RectConstrantCom(ComponentManager, gameBoundsComID)
				{
					Inside = true
				}
			);

			var posComID = gameObject.GetComponent<RectPosCom>().First();

			var posCom = ComponentManager.GetComponent<RectPosCom>(posComID);
			posCom.PostionConstrantComs.Add(constrantID);

			var col = new RectCollisionCom(ComponentManager, GameEvenetPostMaster, posComID)
			{
				GameEventComs = new List<long>()
				{
					gameObject.AddComponent
					(
						new DamageCom(ComponentManager, _healthComID)
						{
							CanBeTriggered = new List<long> { _playerColsionCom },
							Damage = _damage,
							TimeInbetweenRuns = 1
						}
					)
				}
			};
			var colID = gameObject.AddComponent(col);
			gameObject.RegsiterToManager(colID, ColssionManger);

			var contID = gameObject.AddComponent
			(
				new RandomMovementContolerCom(ComponentManager, posComID)
				{
					Speed = _speed
				}
			);

			gameObject.RegsiterToManager(contID, InputManger);

			return entID;
		}

		private long CreateSqaureGoal()
		{
			var entID = EntityManager.CreateEntity(new GameObject(ComponentManager));
			var gameObject = EntityManager.GetEntiy<GameObject>(entID);

			var rectConstBoxID = gameObject.AddComponent(
				new RectPosCom(ComponentManager, new Rectangle(0, 0, 300, 600))
			);

			var screenBoundsID = gameObject.AddComponent(
				new RectPosCom(ComponentManager, new Rectangle(0, 0, 400, 600))
			);

			var rectRendConstrant = gameObject.AddComponent(
				new RectOutlineRendCom(ComponentManager, rectConstBoxID)
				{
					Texture = TextureManager.GetTexture(Textures.BLOCK),
					SpriteBatch = SpriteBatch,
					Color = Color.Green,
					LineWidth = 1
				}
			);
			gameObject.RegsiterToManager(rectRendConstrant, RenderManager);

			var rectPos = new RectPosCom(ComponentManager, new Rectangle(300, 100, 20, 20));
			rectPos.PostionConstrantComs.Add(gameObject.AddComponent(
				new RectConstrantCom(ComponentManager, rectConstBoxID)
				{
					Inside = false,
					Type = RectConstrantCom.CheckType.Overlapping
				}
			));
			rectPos.PostionConstrantComs.Add(gameObject.AddComponent(
				new RectConstrantCom(ComponentManager, screenBoundsID)
				{
					Inside = true
				}
			));
			var rectPosID = gameObject.AddComponent(rectPos);

			var texture = TextureManager.GetTexture(Textures.BLOCK);
			Color[] tcolor = new Color[texture.Width * texture.Height];
			texture.GetData(tcolor);

			for (int i = 0; i < tcolor.Length; i++)
			{
				tcolor[i].R = 255;
			}

			texture.SetData(tcolor);

			var rectRend = new RectRendCom(ComponentManager, rectPosID)
			{
				Texture = TextureManager.GetTexture(Textures.BLOCK),
				SpriteBatch = SpriteBatch,
				Color = Color.Red
			};
			long rectRendID = gameObject.AddComponent(rectRend);
			gameObject.RegsiterToManager(rectRendID, RenderManager);

			long gameEvenetID = gameObject.AddComponent(new GoalReachedCom());

			var colssionComID = gameObject.AddComponent(new RectCollisionCom(ComponentManager, GameEvenetPostMaster, rectPosID)
			{
				GameEventComs = new List<long>() { gameEvenetID, gameObject.AddComponent(new LoadMiniGameCom(SenceManger, new SenceData())) }
			});
			gameObject.RegsiterToManager(colssionComID, ColssionManger);

			var goalContComID = gameObject.AddComponent
			(
				new RandomMovementContolerCom(ComponentManager, rectPosID)
				{
					Speed = 1000
				}
			);
			gameObject.RegsiterToManager(goalContComID, InputManger);

			return entID;
		}

		private long CreateSqaurePlayer()
		{
			var entID = EntityManager.CreateEntity(new GameObject(ComponentManager));
			var gameObject = EntityManager.GetEntiy<GameObject>(entID);

			var gameRectPosID = gameObject.AddComponent(new RectPosCom(ComponentManager, new Rectangle(0, 0, 800, 600)));

			var constrantID = gameObject.AddComponent(
			new RectConstrantCom(ComponentManager, gameRectPosID)
			{
				Inside = true
			}
			);

			var rectPos = new RectPosCom(ComponentManager, new Rectangle(100, 100, 100, 100));

			rectPos.PostionConstrantComs.Add(constrantID);
			var rectPosID = gameObject.AddComponent(rectPos);

			var rectRend = new RectRendCom(ComponentManager, rectPosID)
			{
				Texture = TextureManager.GetTexture(Textures.BLOCK),
				SpriteBatch = SpriteBatch,
				Color = Color.Black
			};
			long rectRendID = gameObject.AddComponent(rectRend);
			gameObject.RegsiterToManager(rectRendID, RenderManager);

			var inputCom = new KeyboardInputCom(ComponentManager, rectPosID);
			long inputComID = gameObject.AddComponent(inputCom);
			gameObject.RegsiterToManager(inputComID, InputManger);

			var colssionComID = gameObject.AddComponent(new RectCollisionCom(ComponentManager, GameEvenetPostMaster, rectPosID));
			gameObject.RegsiterToManager(colssionComID, ColssionManger);
			_playerColsionCom = colssionComID;

			var playerHPCom = gameObject.AddComponent(new HealthCom(ComponentManager, GameEvenetPostMaster, 100, 100, 100));
			gameObject.RegsiterToManager(playerHPCom, StepManager);
			_healthComID = playerHPCom;

			var respwanComID = gameObject.AddComponent(new RespawnCom(ComponentManager, rectPosID, playerHPCom));

			ComponentManager.GetComponent<HealthCom>(playerHPCom).Evenets.Add(
				new HealthCom.HPOpTrigger()
				{
					Opreator = HealthCom.HPOps.Less,
					Value = 0,
					GameEvents = new List<long>() { respwanComID }
				}
			);

			var soundCom = gameObject.AddComponent(new PlaySoundEventCom(SoundManager, Songs.DAMAGE_TAKEN));

			ComponentManager.GetComponent<HealthCom>(playerHPCom).Evenets.Add(
				new HealthCom.HPChangeTrigger()
				{
					GameEvents = new List<long>() { soundCom }
				}
			);


			return entID;
		}
	}
}
