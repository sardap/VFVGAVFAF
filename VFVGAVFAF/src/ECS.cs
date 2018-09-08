using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src
{
	class ECS
	{
		private EntityManager _entityManager;
		private ComponentManager _componentManager;
		private RenderManager _renderManager;
		private InputManger _inputManger;
		private ColssionComManger _colssionManger;
		private TextureManager _textureManager;
		private GameObjectFactory _gameObjectFactory;

		public SpriteBatch SpriteBatch { get; set; }
		public ContentManager Content { get; set; }

		public void Initialse()
		{
			_entityManager = new EntityManager();
			_componentManager = new ComponentManager();
			_renderManager = new RenderManager()
			{
				ComponentManager = _componentManager
			};
			_inputManger = new InputManger(_componentManager);
			_colssionManger = new ColssionComManger
			{
				ComponentManager = _componentManager
			};
			_textureManager = new TextureManager
			{
				Content = Content
			};

			_gameObjectFactory = new GameObjectFactory
			{
				ComponentManager = _componentManager,
				RenderManager = _renderManager,
				EntityManager = _entityManager,
				InputManger = _inputManger,
				ColssionManger = _colssionManger,
				TextureManager = _textureManager,
				Content = Content
			};

			SetupPlayer();
		}

		public void Step(double deltaTime)
		{
			_inputManger.Update(deltaTime);
			_colssionManger.Check();
		}

		public void Render(double deltaTime)
		{
			_renderManager.Render(deltaTime);
		}

		public void Terminate()
		{

		}

		private void SetupPlayer()
		{
			_gameObjectFactory.CreatePlayer(SpriteBatch);
			_gameObjectFactory.NormalEnemy(SpriteBatch);
		}
	}
}
