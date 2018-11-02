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

		private class Sence
		{
			private SenceManger _senceManger;

			private ISenceData _currentSence;
			private ISenceData _overlay;
			private bool _load = false;
			private bool _overlayLoad = false;

			public List<long> ProcessedGameObjects = new List<long>();
			public List<IPassValue> ToPass = new List<IPassValue>();
			public List<IPassValue> ToPassOverlay = new List<IPassValue>();

			public Sence(SenceManger senceManger)
			{
				_senceManger = senceManger;
			}

			public void Load(ISenceData senceData)
			{
				if (_currentSence != null)
				{
					UnloadSence();
				}

				_currentSence = senceData;
				_load = true;
			}

			public void Step()
			{
				if(_overlayLoad)
				{
					LoadSence(_overlay, ToPassOverlay);
					_overlayLoad = false;
				}

				if (_load)
				{
					LoadSence(_currentSence, ToPass);
					_load = false;
				}
			}

			public void UnloadCurrent()
			{
				UnloadSence();
			}

			public void ApplyOverlay(ISenceData overlay, List<IPassValue> toPass)
			{
				_overlay = overlay;
				_overlayLoad = true;
				ToPassOverlay = toPass;
			}

			private void UnloadSence()
			{
				foreach (var created in ProcessedGameObjects)
				{
					_senceManger._entityManager.RegsiterToDestory(created);
				}

				ProcessedGameObjects.Clear();
			}

			private void LoadSence(ISenceData senceData, List<IPassValue> passValues)
			{
				ProcessedGameObjects.AddRange(senceData.Load(_senceManger.GameObjectFactory, passValues));
			}
		}

		private EntityManager _entityManager;
		private Sence _main;
		private Sence _overlay;
		private string _fileNameOverlay = "";
		private Dictionary<string, Sence> _otherSences = new Dictionary<string, Sence>();
		private List<IPassValue> _toPassToOverlay = new List<IPassValue>();


		public GameObjectFactory GameObjectFactory { get; set; }

		public SenceManger(EntityManager entityManager)
		{
			_entityManager = entityManager;
			_main = new Sence(this);
			_overlay = new Sence(this);
		}

		public void LoadMain(ISenceData senceData)
		{
			_main.Load(senceData);
		}

		public void LoadMainFile(string fileName, List<IPassValue> passedValues)
		{
			LoadMain(GetJsonSenceFromFile(fileName));

			_main.ToPass = passedValues;
		}

		public void AddProcessedToMain(long id)
		{
			_main.ProcessedGameObjects.Add(id);
		}

		public void Step()
		{
			_main.Step();
			
			foreach(var entry in _otherSences)
			{
				entry.Value.Step();
			}
		}

		public void UnloadMain()
		{
			_main.UnloadCurrent();
			_overlay.UnloadCurrent();
		}

		public void OverlayFileMain(string fileName, List<IPassValue> passedValues)
		{
			_main.ApplyOverlay(GetJsonSenceFromFile(fileName), passedValues);
		}


		public void Load(string name, ISenceData senceData)
		{
			if(!_otherSences.ContainsKey(name))
			{
				_otherSences.Add(name, new Sence(this));
			}

			_otherSences[name].Load(senceData);
		}

		public void LoadFile(string name, string fileName, List<IPassValue> passedValues)
		{
			Load(name, GetJsonSenceFromFile(fileName));

			_otherSences[name].ToPass = passedValues;
		}

		public void AddProcessedTo(string name, long id)
		{
			_otherSences[name].ProcessedGameObjects.Add(id);
		}

		public void Overlay(string name, string fileName, List<IPassValue> passValues)
		{
			_otherSences[name].ApplyOverlay(GetJsonSenceFromFile(fileName), passValues);
		}

		private JsonSence GetJsonSenceFromFile(string fileName)
		{
			string jsonString;

			using (StreamReader streamReader = new StreamReader(fileName))
			{
				jsonString = streamReader.ReadToEnd();
			}

			JsonSence jsonSence = JsonConvert.DeserializeObject<JsonSence>(jsonString, _settings);

			return jsonSence;
		}

	}
}
