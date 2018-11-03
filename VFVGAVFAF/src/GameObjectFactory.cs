﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;
using VFVGAVFAF.src.Json;
using VFVGAVFAF.src.Managers;
using VFVGAVFAF.src;
using VFVGAVFAF.src.Sence;
using Newtonsoft.Json;

namespace VFVGAVFAF.src
{
	class GameObjectFactory
	{
		public enum GameObjects
		{
			SqaurePlayer,
			SqaureGoal,
			EnemeySqaure,
			TestPlayer,
			TestEnemy
		}

		private delegate GameObject CreateGameObject();

		private Dictionary<GameObjects, CreateGameObject> _gameObjectCreators = new Dictionary<GameObjects, CreateGameObject>();

		public ComponentManager ComponentManager { get; set; }
		public EntityManager EntityManager { get; set; }
		public RenderManager RenderManager { get; set; }
		public InputManger InputManger { get; set; }
		public ColssionComManger ColssionManger { get; set; }
		public IStepManager StepManager { get; set; }
		public SoundManager SoundManager { get; set; }
		public TextureManager TextureManager { get; set; }
		public ContentManager Content { get; set; }
		public ISenceManger SenceManger { get; set; }
		public SpriteBatch SpriteBatch { get; set; }
		public FontManger FontManger { get; set; }
		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }
		public GameInfo GameInfo { get; set; }
		public MinigameManger MinigameManger { get; set; }
		public BlueprintManger BlueprintManger { get; set; }
		public MouseManger MouseManger { get; set; }
		public KeyboardInputManger KeyboardInputManger { get; set; }

		public GameObjectFactory()
		{
		}

		public GameObject CreateObjectOfType(GameObjects gameObjects)
		{
			return _gameObjectCreators[gameObjects]();
		}

		public long AddCreatedGameObject(GameObject gameObject)
		{
			var entID = EntityManager.CreateEntity(gameObject);
			AddressNeedsAndMangers(gameObject);
			return entID;
		}

		public bool VaildToAdd(EnityToJson jsonGameObject)
		{
			return jsonGameObject.InstanceLimt == null || 
				!EntityManager.InstanceCount.ContainsKey(jsonGameObject.Alias) ||
				EntityManager.InstanceCount.ContainsKey(jsonGameObject.Alias) && 
				EntityManager.InstanceCount[jsonGameObject.Alias] < jsonGameObject.InstanceLimt;
		}

		public long AddCreatedGameObject(EnityToJson jsonGameObject, List<IPassValue> passedValues)
		{
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto
			};

			var gameObject = new GameObject(ComponentManager);

			if (jsonGameObject.Alias != null)
			{
				gameObject.Alias = jsonGameObject.Alias;
			}
					
			var entID = EntityManager.CreateEntity(gameObject);
			gameObject = EntityManager.GetEntiy<GameObject>(entID);

			gameObject.Tags = jsonGameObject.Tags;

			jsonGameObject.Components.ForEach(i =>
			{
				var jsonString = JsonConvert.SerializeObject(i, typeof(IComponent), settings);
				var com = JsonConvert.DeserializeObject<IComponent>(jsonString, settings);

				gameObject.AddComponent(com);
			});

			AddressNeedsAndMangers(gameObject);

			var ent = EntityManager.GetEntiy<GameObject>(entID);

			foreach(var entry in passedValues)
			{
				dynamic dCom = ent.GetComponent<IComponent>(entry.TargetAlais);
				dCom.Value = entry.DValue;
			}


			return entID;
		}

		private void RegsiterToMangers(GameObject gameObject, IComponent component, long comID)
		{
			if (component is IRenderableComponent)
			{
				gameObject.RegsiterToManager(comID, RenderManager);
			}

			if (component is IContolerCom)
			{
				gameObject.RegsiterToManager(comID, InputManger);
			}

			if (component is ICollisionCom)
			{
				gameObject.RegsiterToManager(comID, ColssionManger);
			}

			if(component is IStepCom)
			{
				gameObject.RegsiterToManager(comID, StepManager);
			}
		}

		private T AddressNeeds<T>(T component) where T : IComponent
		{
			if (component is INeedEnityManger)
			{
				((INeedEnityManger)component).EntityManager = EntityManager;
			}

			if (component is INeedSpriteBatch)
			{
				((INeedSpriteBatch)component).SpriteBatch = SpriteBatch;
			}

			if (component is INeedTextureManager)
			{
				((INeedTextureManager)component).TextureManager = TextureManager;
			}

			if(component is INeedSoundManger)
			{
				((INeedSoundManger)component).SoundManager = SoundManager;
			}

			if(component is INeedPostMaster)
			{
				((INeedPostMaster)component).GameEvenetPostMaster = GameEvenetPostMaster;
			}

			if (component is INeedSenceManger)
			{
				((INeedSenceManger)component).SenceManger = SenceManger;
			}

			if(component is INeedFontManager)
			{
				((INeedFontManager)component).FontManger = FontManger;
			}

			if (component is INeedGameInfo)
			{
				((INeedGameInfo)component).GameInfo = GameInfo;
			}

			if (component is INeedMinigameManger)
			{
				((INeedMinigameManger)component).MinigameManger = MinigameManger;
			}

			if (component is INeedBlueprintManger)
			{
				((INeedBlueprintManger)component).EntiyBlueprintManger = BlueprintManger;
			}

			if (component is INeedGamesObjectFactory)
			{
				((INeedGamesObjectFactory)component).GameObjectFactory = this;
			}

			if (component is INeedMouseManger)
			{
				((INeedMouseManger)component).MouseManger = MouseManger;
			}

			if (component is INeedKeyboardInputManger)
			{
				((INeedKeyboardInputManger)component).KeyboardInputManger = KeyboardInputManger;
			}


			return component;
		}

		private void AddressNeedsAndMangers(GameObject gameObject)
		{
			gameObject.GetComponents.ForEach(i => 
			{
				AddressNeeds(i.Item2);
				RegsiterToMangers(gameObject, i.Item2, i.Item1);
				i.Item2.EntID = gameObject.GetID;
			});
		}

		public List<IComponent> CreateMusicEnt()
		{
			var result = new List<IComponent>();

			result.Add(new PlayMusicCom()
			{
				Alias = "music",
				MusicName = Music.FirstSong
			});

			result.Add(new TriggerEvenetCom()
			{
				EventAlais = new List<string>() { "music" }
			});

			return result;
		}

		public List<IComponent> CreateTestSqaure()
		{
			var result = new List<IComponent>();

			Rectangle rectangle = new Rectangle(0, 0, 100, 100);

			result.Add(new RectPosCom(rectangle)
			{
				Alias = "pos",
				PostionConstrantComs = new List<string> { "gameBoundsConstrant" }
			});

			result.Add(new RectRendCom()
			{
				TextureName = Textures.BLOCK,
				Color = Color.Black,
				RectPosAlais = "pos",
			});

			result.Add(new KeyboardInputCom()
			{
				RectPosAlais = "pos",
				Speed = 500
			});

			result.Add(new RectPosCom(new Rectangle(0, 0, 800, 600))
			{
				Alias = "gameBounds"
			});

			result.Add(new RectConstrantCom()
			{
				Alias = "gameBoundsConstrant",
				RectPosAlais = "gameBounds",
				Inside = true
			});

			result.Add(new PlaySoundEventCom()
			{
				Alias = "damageSound",
				Song = Sounds.DAMAGE_TAKEN
			});

			result.Add(new PlaySoundEventCom()
			{
				Alias = "respawnSound",
				Song = Sounds.PLAYER_RESPAWN
			});

			result.Add(new HealthCom()
			{
				HP = 100,
				StartingHP = 100,
				MaxHP = 100,
				Evenets = new List<HealthCom.IHPTrigger>()
				{
					new HealthCom.HPChangeTrigger
					{
						GameEvents = new List<string> { "damageSound" }
					},
					new HealthCom.HPOpTrigger
					{
						Opreator = HealthCom.HPOps.Less,
						Value = 0,
						GameEvents = new List<string> { "respawn", "respawnSound" }
					}
				}
			});

			result.Add(new RespawnCom()
			{
				Alias = "respawn",
				PosComAlais = "pos"
			});

			result.Add(new RectCollisionCom()
			{
				RectPosAlais = "pos",
			});

			return result;
		}

		public List<IComponent> CreateTestEnemy()
		{
			var result = new List<IComponent>();

			Rectangle rectangle = new Rectangle(50, 50, 150, 50);

			result.Add(new RectPosCom(rectangle)
			{
				Alias = "pos",
				PostionConstrantComs = new List<string> { "gameBoundsConstrant" },
				RandomStartPos = true
			});

			result.Add(new RectRendCom()
			{
				TextureName = Textures.BLOCK,
				Color = Color.Aqua,
				RectPosAlais = "pos",
			});

			result.Add(new PatrolContorlerCom()
			{
				PosAlais = "pos",
				Alias = "directionCom",
				Radius = 30,
				Speed = 100,
				Start = new Postion2D(200, 0),
				End = new Postion2D(200, 500)
			});

			result.Add(new RectPosCom(new Rectangle(0, 0, 800, 600))
			{
				Alias = "gameBounds"
			});

			result.Add(new RectConstrantCom()
			{
				Alias = "gameBoundsConstrant",
				RectPosAlais = "gameBounds",
				Inside = true
			});

			result.Add(new DamageCom()
			{
				Alias = "damage",
				Damage = -10,
				TimeToComplete = 0,
				Cooldown = 0.5
			});

			result.Add(new RectCollisionCom()
			{
				RectPosAlais = "pos",
				GameEventComs = new Dictionary<string, List<string>>() { { "Fuckyou", new List<string>() { "damage" } } }
			});

			return result;
		}

		private long _healthComID;
		private long _playerColsionCom;
		private long _speed = 50;
		private int _damage = -10;
		private Rectangle _nextRectSize;
		private Rectangle _bounds = new Rectangle(0, 0, 800, 600);

		private long CreateEnemeySqaure()
		{
			/*
			_nextRectSize = new Paultangle(Utils.RandomPostionInBounds(_bounds), 25, 25).ToMonoGameRectangle();

			var entID = CreateSqaure(_nextRectSize);
			var gameObject = EntityManager.GetEntiy<GameObject>(entID);

			var gameBoundsComID = gameObject.AddComponent(new RectPosCom(entID, EntityManager, _bounds));

			var constrantID = gameObject.AddComponent(
				new RectConstrantCom(entID, EntityManager, gameBoundsComID)
				{
					Inside = true
				}
			);

			var posComID = gameObject.GetFirstComponentID<RectPosCom>();

			var posCom = ComponentManager.GetComponent<RectPosCom>(posComID);
			posCom.PostionConstrantComs.Add(constrantID);

			var col = new RectCollisionCom(entID, EntityManager, GameEvenetPostMaster, posComID)
			{
				GameEventComs = new List<long>()
				{
					gameObject.AddComponent
					(
						new DamageCom(EntityManager, _healthComID)
						{
							Damage = _damage,
							TimeToComplete = 0,
							Cooldown = 0.5
						}
					)
				}
			};
			var colID = gameObject.AddComponent(col);
			gameObject.RegsiterToManager(colID, ColssionManger);

			var contID = gameObject.AddComponent
			(
				new RandomMovementContolerCom(entID, EntityManager, posComID)
				{
					Speed = _speed
				}
			);

			gameObject.RegsiterToManager(contID, InputManger);

			return entID;
			*/
			throw new NotImplementedException();
		}

		private long CreateSqaureGoal()
		{
			/*
			var entID = EntityManager.CreateEntity(new GameObject(ComponentManager));
			var gameObject = EntityManager.GetEntiy<GameObject>(entID);

			var rectConstBoxID = gameObject.AddComponent(
				new RectPosCom(entID, EntityManager, new Rectangle(0, 0, 300, 600))
			);

			var screenBoundsID = gameObject.AddComponent(
				new RectPosCom(entID, EntityManager, new Rectangle(0, 0, 400, 600))
			);

			var rectRendConstrant = gameObject.AddComponent(
				new RectOutlineRendCom(entID, EntityManager, rectConstBoxID)
				{
					Texture = TextureManager.GetTexture(Textures.BLOCK, Color.Green),
					SpriteBatch = SpriteBatch,
					Color = Color.Green,
					LineWidth = 1
				}
			);
			gameObject.RegsiterToManager(rectRendConstrant, RenderManager);

			var rectPos = new RectPosCom(entID, EntityManager, new Rectangle(300, 100, 20, 20));
			rectPos.PostionConstrantComs.Add(gameObject.AddComponent(
				new RectConstrantCom(entID, EntityManager, rectConstBoxID)
				{
					Inside = false,
					Type = RectConstrantCom.CheckType.Overlapping
				}
			));
			rectPos.PostionConstrantComs.Add(gameObject.AddComponent(
				new RectConstrantCom(entID, EntityManager, screenBoundsID)
				{
					Inside = true
				}
			));
			var rectPosID = gameObject.AddComponent(rectPos);

			var rectRend = new RectRendCom(EntityManager, rectPosID)
			{
				TextureName = Textures.BLOCK,
				Color = Color.Red,
				TextureManager = TextureManager,
				SpriteBatch = SpriteBatch
			};
			long rectRendID = gameObject.AddComponent(rectRend);
			gameObject.RegsiterToManager(rectRendID, RenderManager);

			long gameEvenetID = gameObject.AddComponent(new GoalReachedCom(entID));

			var colssionComID = gameObject.AddComponent(new RectCollisionCom(entID, EntityManager, GameEvenetPostMaster, rectPosID)
			{
				GameEventComs = new List<long>() { gameEvenetID, gameObject.AddComponent(new LoadMiniGameCom(entID, SenceManger, new SenceData())) }
			});
			gameObject.RegsiterToManager(colssionComID, ColssionManger);

			var goalContComID = gameObject.AddComponent
			(
				new RandomMovementContolerCom(entID, EntityManager, rectPosID)
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

			var gameRectPosID = gameObject.AddComponent(new RectPosCom(entID, EntityManager, new Rectangle(0, 0, 800, 600)));

			var constrantID = gameObject.AddComponent(
			new RectConstrantCom(entID, EntityManager, gameRectPosID)
			{
				Inside = true
			}
			);

			var rectPos = new RectPosCom(entID, EntityManager, new Rectangle(100, 100, 100, 100));

			rectPos.PostionConstrantComs.Add(constrantID);
			var rectPosID = gameObject.AddComponent(rectPos);

			var rectRend = new RectRendCom(EntityManager, rectPosID)
			{
				TextureName = Textures.BLOCK,
				Color = Color.Black,
				TextureManager = TextureManager,
				SpriteBatch = SpriteBatch
			};
			long rectRendID = gameObject.AddComponent(rectRend);
			gameObject.RegsiterToManager(rectRendID, RenderManager);

			var inputCom = new KeyboardInputCom(entID, EntityManager);
			long inputComID = gameObject.AddComponent(inputCom);
			gameObject.RegsiterToManager(inputComID, InputManger);

			var colssionComID = gameObject.AddComponent(new RectCollisionCom(entID, EntityManager, GameEvenetPostMaster, rectPosID));
			gameObject.RegsiterToManager(colssionComID, ColssionManger);
			_playerColsionCom = colssionComID;

			var playerHPCom = gameObject.AddComponent
			(
				new HealthCom(entID, GameEvenetPostMaster, 100, 100, 100)
			);
			gameObject.RegsiterToManager(playerHPCom, StepManager);
			_healthComID = playerHPCom;

			var respwanComID = gameObject.AddComponent(new RespawnCom(entID, EntityManager, rectPosID));
			var respawnSoundComID = gameObject.AddComponent(new PlaySoundEventCom(SoundManager, Songs.PLAYER_RESPAWN));

			ComponentManager.GetComponent<HealthCom>(playerHPCom).Evenets.Add(
				new HealthCom.HPOpTrigger()
				{
					Opreator = HealthCom.HPOps.Less,
					Value = 0,
					GameEvents = new List<long>() { respwanComID, respawnSoundComID }
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
			*/
			throw new NotImplementedException();
		}
	}
}
