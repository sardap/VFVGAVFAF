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
using VFVGAVFAF.src.Components;

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
		private FontManger _fontManger;
		private SoundManager _soundManager;
		private GameObjectFactory _gameObjectFactory;
		private ISenceManger _senceManger;
		private IStepManager _stepManager;
		private IGameEvenetPostMaster _gameEvenetPostMaster;
		private GameInfo _gameInfo;
		private MinigameManger _minigameManger;
		private BlueprintManger _entiyBlueprintManger;

		public SpriteBatch SpriteBatch { get; set; }
		public ContentManager Content { get; set; }

		public void Initialse(GraphicsDeviceManager graphics)
		{
			_componentManager = new ComponentManager();
			_entityManager = new EntityManager()
			{
				ComponentManager = _componentManager
			};
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
			_fontManger = new FontManger
			{
				Content = Content
			};
			_soundManager = new SoundManager(Content);
			_senceManger = new SenceManger(_entityManager);
			_stepManager = new StepManager() { ComponentManager = _componentManager };
			_gameEvenetPostMaster = new GameEvenetPostMaster(_componentManager);
			_gameInfo = new GameInfo(graphics);
			_minigameManger = new MinigameManger()
			{
				SenceManger = _senceManger,
				MinigameFiles = new List<string>()
				{
					//@"Minigames\Game1.json"
					@"Minigames\Game2.json",
					@"Minigames\Game3.json",
					@"Minigames\Game4.json",
					@"Minigames\Game5.json"
				}
			};

			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto
			};

			_entiyBlueprintManger =  JsonConvert.DeserializeObject<BlueprintManger>
			(
				Utils.ReadEntireFile(@"Minigames\Blueprints.json"),
				settings
			);

			_gameObjectFactory = new GameObjectFactory
			{
				ComponentManager = _componentManager,
				RenderManager = _renderManager,
				EntityManager = _entityManager,
				InputManger = _inputManger,
				ColssionManger = _colssionManger,
				StepManager = _stepManager,
				TextureManager = _textureManager,
				FontManger = _fontManger,
				SoundManager = _soundManager,
				MinigameManger = _minigameManger,
				SpriteBatch = SpriteBatch,
				SenceManger = _senceManger,
				BlueprintManger = _entiyBlueprintManger,
				Content = Content,
				GameEvenetPostMaster = _gameEvenetPostMaster,
				GameInfo = _gameInfo
			};

			_senceManger.GameObjectFactory = _gameObjectFactory;
			_componentManager.PostMaster = _gameEvenetPostMaster;

			SetupPlayer();
		}

		public void Step(double deltaTime)
		{
			_entityManager.Step();
			_senceManger.Step();
			_gameEvenetPostMaster.Step(deltaTime);
			_inputManger.Update(deltaTime);
			_colssionManger.Check();
			_stepManager.Step(deltaTime);
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
			_minigameManger.PlayNext();

			/*
			JsonSerializerSettings _settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto
			};

			JsonSence jsonGameobject;
			string jsonString;

			if (false)
			{
				JsonSence enityToJsons = new JsonSence();

				jsonString = JsonConvert.SerializeObject(enityToJsons, typeof(List<EnityToJson>), _settings);

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
			
			jsonGameobject = JsonConvert.DeserializeObject<JsonSence>(jsonString, _settings);

			_senceManger.Load(jsonGameobject);
			//_senceManger.UnloadCurrent();
			*/
		}
	}
}
