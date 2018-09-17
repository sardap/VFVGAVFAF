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
			SqaureGoal
		}

		private delegate long CreateGameObject();

		private Dictionary<GameObjects, CreateGameObject> _gameObjectCreators = new Dictionary<GameObjects, CreateGameObject>();

		public ComponentManager ComponentManager { get; set; }
		public EntityManager EntityManager { get; set; }
		public RenderManager RenderManager { get; set; }
		public InputManger InputManger { get; set; }
		public ColssionComManger ColssionManger { get; set; }
		public TextureManager TextureManager { get; set; }
		public ContentManager Content { get; set; }
		public ISenceManger SenceManger { get; set; }
		public SpriteBatch SpriteBatch { get; set; }

		public GameObjectFactory()
		{
			_gameObjectCreators.Add(GameObjects.SqaurePlayer, new CreateGameObject(CreateSqaurePlayer));
			_gameObjectCreators.Add(GameObjects.SqaureGoal, new CreateGameObject(CreateSqaureGoal));
		}

		public long CreateObjectOfType(GameObjects gameObjects)
		{
			return _gameObjectCreators[gameObjects]();
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
					Color = Color.Red,
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
				Texture = texture,
				SpriteBatch = SpriteBatch,
				Color = Color.Red
			};
			long rectRendID = gameObject.AddComponent(rectRend);
			gameObject.RegsiterToManager(rectRendID, RenderManager);

			long gameEvenetID = gameObject.AddComponent(new GoalReachedCom());

			var colssionComID = gameObject.AddComponent(new RectCollisionCom(ComponentManager, rectPosID)
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

			var constrantBox = new RectConstrantCom(ComponentManager,
				gameObject.AddComponent(
					new RectPosCom(ComponentManager, new Rectangle(0, 0, 800, 600))
				)
			)
			{
				Inside = true
			};
			var constrantID = gameObject.AddComponent(constrantBox);

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

			var colssionComID = gameObject.AddComponent(new RectCollisionCom(ComponentManager, rectPosID));
			gameObject.RegsiterToManager(colssionComID, ColssionManger);

			return entID;
		}
	}
}
