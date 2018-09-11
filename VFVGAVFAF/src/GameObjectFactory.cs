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

namespace VFVGAVFAF.src
{
	class GameObjectFactory
	{
		public ComponentManager ComponentManager { get; set; }
		public EntityManager EntityManager { get; set; }
		public RenderManager RenderManager { get; set; }
		public InputManger InputManger { get; set; }
		public ColssionComManger ColssionManger { get; set; }
		public TextureManager TextureManager { get; set; }
		public ContentManager Content { get; set; }

		public GameObject CreatePlayer(SpriteBatch spriteBatch)
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

			var border2 = gameObject.AddComponent(new RectPosCom(ComponentManager, new Rectangle(400, 0, 400, 600)));
			var constrantID2 = gameObject.AddComponent(new RectConstrantCom(ComponentManager,  border2)
			{
				Inside = false,
				Type = RectConstrantCom.CheckType.Overlapping
			});
			
			gameObject.RegsiterToManager(gameObject.AddComponent(new RectRendCom(ComponentManager, border2)
			{
				Texture = TextureManager.GetTexture(Textures.BLOCK),
				SpriteBatch = spriteBatch,
				Color = Color.Red
			}), RenderManager);

			var rectPos = new RectPosCom(ComponentManager, new Rectangle(100, 100, 100, 100));

			rectPos.PostionConstrantComs.Add(constrantID);
			rectPos.PostionConstrantComs.Add(constrantID2);
			var rectPosID = gameObject.AddComponent(rectPos);

			var rectRend = new RectRendCom(ComponentManager, rectPosID)
			{
				Texture =  TextureManager.GetTexture(Textures.BLOCK),
				SpriteBatch = spriteBatch,
				Color = Color.Black
			};
			long rectRendID = gameObject.AddComponent(rectRend);
			gameObject.RegsiterToManager(rectRendID, RenderManager);
			
			var inputCom = new KeyboardInputCom(ComponentManager, rectPosID);
			long inputComID = gameObject.AddComponent(inputCom);
			gameObject.RegsiterToManager(inputComID, InputManger);

			var colssionComID = gameObject.AddComponent(new RectCollisionCom(ComponentManager, rectPosID));
			gameObject.RegsiterToManager(colssionComID, ColssionManger); 

			return EntityManager.GetEntiy<GameObject>(entID);
		}

		public GameObject NormalEnemy(SpriteBatch spriteBatch)
		{
			var entID = EntityManager.CreateEntity(new GameObject(ComponentManager));
			var gameObject = EntityManager.GetEntiy<GameObject>(entID);

			var rectPos = new RectPosCom(ComponentManager, new Rectangle(300, 100, 20, 20));
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
				SpriteBatch = spriteBatch,
				Color = Color.Red
			};
			long rectRendID = gameObject.AddComponent(rectRend);
			gameObject.RegsiterToManager(rectRendID, RenderManager);

			long gameEvenetID = gameObject.AddComponent(new GoalReachedCom());

			var colssionComID = gameObject.AddComponent(new RectCollisionCom(ComponentManager, rectPosID)
			{
				GameEventComs = new List<long>() { gameEvenetID }
			});
			gameObject.RegsiterToManager(colssionComID, ColssionManger);

			return gameObject;
		}
	}
}
