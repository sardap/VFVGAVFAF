using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Json;

namespace VFVGAVFAF.src.Sence
{
	class SenceManger : ISenceManger
	{
		private JsonSerializerSettings _settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto
		};

		private EntityManager _entityManager;
		private ISenceData _currentSence;
		private List<long> _processedGameObjects = new List<long>();
		private bool _load = false;
		private bool _overlay = false;
		private int _numReloads;
		private string _fileNameOverlay = "";
		private List<IPassValue> _toPass = new List<IPassValue>();
		private List<IPassValue> _toPassToOverlay = new List<IPassValue>();

		public GameObjectFactory GameObjectFactory { get; set; }

		public SenceManger(EntityManager entityManager)
		{
			_entityManager = entityManager;
		}

		public void Load(ISenceData senceData)
		{
			_numReloads++;
			Console.WriteLine("RELOAD {0}", _numReloads);

			if (_currentSence != null)
			{
				UnloadCurrent();
			}

			_currentSence = senceData;
			_load = true;
		}

		public void LoadFromFile(string fileName)
		{
			string jsonString;

			using (StreamReader streamReader = new StreamReader(fileName))
			{
				jsonString = streamReader.ReadToEnd();
			}

			JsonSence jsonGameobject = JsonConvert.DeserializeObject<JsonSence>(jsonString, _settings);

			Load(jsonGameobject);

			_toPass.Clear();
		}

		public void LoadFromFile(string fileName, List<IPassValue> passedValues)
		{
			LoadFromFile(fileName);

			_toPass = passedValues;
		}

		public void AddToProcessed(long id)
		{
			_processedGameObjects.Add(id);
		}

		public void Step()
		{
			if(_load)
			{
				LoadSence(_currentSence, _toPass);
				_load = false;
			}

			if(_overlay)
			{
				LoadSence(LoadFile(_fileNameOverlay), _toPassToOverlay);
				_overlay = false;
			}
		}

		public void UnloadCurrent()
		{
			UnloadSence(_currentSence);
		}

		public void OverlayFromFile(string fileName, List<IPassValue> passedValues)
		{
			_fileNameOverlay = fileName;
			_toPassToOverlay = passedValues;
			_overlay = true;
		}


		private void LoadSence(ISenceData senceData, List<IPassValue> passValues)
		{
			_processedGameObjects.AddRange(senceData.Load(GameObjectFactory, passValues));
		}

		private void UnloadSence(ISenceData senceData)
		{
			foreach(var created in _processedGameObjects)
			{
				_entityManager.RegsiterToDestory(created);
			}

			_processedGameObjects.Clear();
		}

		private JsonSence LoadFile(string fileName)
		{
			string jsonString;

			using (StreamReader streamReader = new StreamReader(fileName))
			{
				jsonString = streamReader.ReadToEnd();
			}

			var jsonSence = JsonConvert.DeserializeObject<JsonSence>(jsonString, _settings);

			return jsonSence;
		}


	}
}
