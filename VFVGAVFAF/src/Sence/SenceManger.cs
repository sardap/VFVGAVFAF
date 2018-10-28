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
		private EntityManager _entityManager;
		private ISenceData _currentSence;
		private List<long> _processedGameObjects = new List<long>();
		private bool _load = false;
		private int _numReloads;

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
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto
			};

			string jsonString;

			using (StreamReader streamReader = new StreamReader(fileName))
			{
				jsonString = streamReader.ReadToEnd();
			}

			JsonSence jsonGameobject = JsonConvert.DeserializeObject<JsonSence>(jsonString, settings);

			Load(jsonGameobject);
		}

		public void AddToProcessed(long id)
		{
			_processedGameObjects.Add(id);
		}


		public void Step()
		{
			if(_load)
			{
				LoadSence(_currentSence);
				_load = false;
			}
		}

		public void UnloadCurrent()
		{
			UnloadSence(_currentSence);
		}

		private void LoadSence(ISenceData senceData)
		{
			_processedGameObjects.AddRange(senceData.Load(GameObjectFactory));
		}

		private void UnloadSence(ISenceData senceData)
		{
			foreach(var created in _processedGameObjects)
			{
				_entityManager.RegsiterToDestory(created);
			}

			_processedGameObjects.Clear();
		}
	}
}
