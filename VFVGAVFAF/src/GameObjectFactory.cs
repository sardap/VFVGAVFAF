using Microsoft.Xna.Framework;
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
using VFVGAVFAF.src.Pathfinding;
using System.Text.RegularExpressions;

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
		public Grid Grid { get; set; }

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

			foreach (var entry in passedValues)
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

			if (component is IStepCom)
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

			if (component is INeedSoundManger)
			{
				((INeedSoundManger)component).SoundManager = SoundManager;
			}

			if (component is INeedPostMaster)
			{
				((INeedPostMaster)component).GameEvenetPostMaster = GameEvenetPostMaster;
			}

			if (component is INeedSenceManger)
			{
				((INeedSenceManger)component).SenceManger = SenceManger;
			}

			if (component is INeedFontManager)
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

			if (component is INeedGrid)
			{
				((INeedGrid)component).Grid = Grid;
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
	}
}
