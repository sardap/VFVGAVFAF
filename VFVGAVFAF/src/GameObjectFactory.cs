using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;

namespace VFVGAVFAF.src
{
	class GameObjectFactory
	{
		public ComponentManager ComponentManager { get; set; }
		public EntityManager EntityManager { get; set; }
		public RenderManager RenderManager { get; set; }
		public InputManger InputManger { get; set; }
		public ContentManager Content { get; set; }

		public GameObject CreatePlayer(SpriteBatch spriteBatch)
		{
			var entID = EntityManager.CreateEntity(new GameObject());

			var rectPos = new RectPosCom();
			var rectPosID = ComponentManager.CreateComponet(entID, rectPos);

			var rectRend = new RectRendCom(ComponentManager, rectPosID)
			{
				Texture = Content.Load<Texture2D>("Images/player"),
				SpriteBatch = spriteBatch,
				Color = Color.Black
			};
			long rectRendID = ComponentManager.CreateComponet(entID, rectRend);
			RenderManager.RegsiterRendable<RectRendCom>(rectRendID);
			
			var inputCom = new KeyboardInputCom(ComponentManager, rectPosID);
			long inputComID = ComponentManager.CreateComponet(entID, inputCom);
			InputManger.RegsiterComponet(inputComID);

			return EntityManager.GetEntiy<GameObject>(entID);
		}
	}
}
