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

			var rectPos = new RectPosCom
			{
				Postion = new Postion2D(100, 100)
			};
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

			var rectPos = new RectPosCom();
			rectPos.Postion.X = 300;
			rectPos.Postion.Y = 100;
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
