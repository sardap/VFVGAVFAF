using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using VFVGAVFAF.src.Managers;
using VFVGAVFAF.src.Sence;
using Newtonsoft.Json;
using VFVGAVFAF.src.Json;
using System.IO;

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
		private SoundManager _soundManager;
		private GameObjectFactory _gameObjectFactory;
		private ISenceManger _senceManger;
		private IStepManager _stepManager;
		private IGameEvenetPostMaster _gameEvenetPostMaster;

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
			_colssionManger = new ColssionComManger(_componentManager);
			_textureManager = new TextureManager
			{
				Content = Content
			};
			_soundManager = new SoundManager(Content);
			_senceManger = new SenceManger(_entityManager);
			_stepManager = new StepManager() { ComponentManager = _componentManager };
			_gameEvenetPostMaster = new GameEvenetPostMaster(_componentManager);

			_gameObjectFactory = new GameObjectFactory
			{
				ComponentManager = _componentManager,
				RenderManager = _renderManager,
				EntityManager = _entityManager,
				InputManger = _inputManger,
				ColssionManger = _colssionManger,
				StepManager = _stepManager,
				TextureManager = _textureManager,
				SoundManager = _soundManager,
				SpriteBatch = SpriteBatch,
				SenceManger = _senceManger,
				Content = Content,
				GameEvenetPostMaster = _gameEvenetPostMaster
			};

			_senceManger.GameObjectFactory = _gameObjectFactory;

			SetupPlayer();
		}

		public void Step(double deltaTime)
		{
			_gameEvenetPostMaster.Step(deltaTime);
			_inputManger.Update(deltaTime);
			_colssionManger.Check();
			_stepManager.Step(deltaTime);
			_entityManager.Step();
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
			ISenceData senceData = new SenceData();

			//senceData.ToCreate.Add(GameObjectFactory.GameObjects.TestEntiy);
			/*
			senceData.ToCreate.Add(GameObjectFactory.GameObjects.SqaurePlayer);

			for (int i = 0; i < 4; i++)
			{
				senceData.ToCreate.Add(GameObjectFactory.GameObjects.EnemeySqaure);
			}
			*/

			senceData.SaveFile("sence.json");

			JsonSerializerSettings _settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto
			};

			_senceManger.Load(Utils.LoadSenceDataFromFile("sence.json").GetAwaiter().GetResult());

			EnityToJson jsonGameobject;
			string jsonString;

			if (false)
			{
				jsonGameobject = new EnityToJson();
				jsonGameobject.PopluateFromEntiy(_entityManager.GetEntiy<GameObject>(0));

				jsonString = JsonConvert.SerializeObject(jsonGameobject, typeof(EnityToJson), _settings);

				using (StreamWriter streamWriter = new StreamWriter("entriy.json"))
				{
					streamWriter.Write(jsonString);
				}
			}
			else
			{
				using (StreamReader streamReader = new StreamReader("entriy.json"))
				{
					jsonString = streamReader.ReadToEnd();
				}
			}
			

			jsonGameobject = JsonConvert.DeserializeObject<EnityToJson>(jsonString, _settings);

			_gameObjectFactory.AddCreatedGameObject(jsonGameobject);

		}
	}
}
