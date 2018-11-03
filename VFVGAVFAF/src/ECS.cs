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
using VFVGAVFAF.src.Pathfinding;

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
		private MouseManger _mouseManger;
		private KeyboardInputManger _keyboardInputManger;
		private Grid _grid;

		public Settings Settings;

		public Matrix ScaleMatrix { get; set; }

		public SpriteBatch SpriteBatch { get; set; }

		public ContentManager Content { get; set; }

		public void InitiliseSettings()
		{
			var fileName = @"Minigames\Settings.json";

			if (!File.Exists(fileName))
			{
				using (StreamWriter writer = new StreamWriter(fileName))
				{
					writer.Write(JsonConvert.SerializeObject(new Settings(), typeof(Settings), Utils.JsonSettings));
				}
			}

			using (StreamReader reader = new StreamReader(fileName))
			{
				Settings = JsonConvert.DeserializeObject<Settings>(reader.ReadToEnd(), Utils.JsonSettings);
			}
		}

		public void Initialse(GraphicsDeviceManager graphics)
		{
			_mouseManger = new MouseManger();
			_keyboardInputManger = new KeyboardInputManger();
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
			_grid = new Grid();
			_colssionManger = new ColssionComManger(_componentManager, _entityManager, _grid);
			_textureManager = new TextureManager
			{
				Content = Content
			};
			_fontManger = new FontManger
			{
				Content = Content
			};
			_soundManager = new SoundManager(Content, Settings);
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
					/*
					@"Minigames\Game2.json",
					@"Minigames\Game3.json",
					@"Minigames\Game4.json",
					@"Minigames\Game5.json",
					@"Minigames\Game6.json",
					@"Minigames\Game7.json",
					@"Minigames\Game8.json",
					@"Minigames\Game9.json",
					*/
					@"Minigames\Game10.json"
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
				MouseManger = _mouseManger,
				KeyboardInputManger = _keyboardInputManger,
				Content = Content,
				GameEvenetPostMaster = _gameEvenetPostMaster,
				Grid = _grid,
				GameInfo = _gameInfo
			};

			_senceManger.GameObjectFactory = _gameObjectFactory;
			_componentManager.PostMaster = _gameEvenetPostMaster;

			SetupPlayer();
			SetupResoultion();
		}

		public void Step(double deltaTime)
		{
			_mouseManger.Step(ScaleMatrix);
			_keyboardInputManger.Step();
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
			_senceManger.LoadFile("background", @"Minigames\Background.json", new List<IPassValue>());
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

		private void SetupResoultion()
		{
			var scaleX = (float)Settings.Width / GameInfo.VIRTUAL_WIDTH;
			var scaleY = (float)Settings.Height / GameInfo.VIRTUAL_HEIGHT;
			ScaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

		}
	}
}
