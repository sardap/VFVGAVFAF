using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Sence
{
	class SenceData : IFactorySenceData
	{
		private List<GameObjectFactory.GameObjects> _toCreate = new List<GameObjectFactory.GameObjects>();
		private List<GameObject> _createdGameObjects = new List<GameObject>();
		private List<long> _entites = new List<long>();

		public IList<GameObjectFactory.GameObjects> ToCreate { get { return _toCreate; } }
		public List<GameObject> CreatedGameObjects { get { return _createdGameObjects; } }

		public void Create(GameObjectFactory gameObjectFactory)
		{

		}

		public void CreateGameObjects(GameObjectFactory gameObjectFactory)
		{
			foreach(var method in _toCreate)
			{
				_createdGameObjects.Add(gameObjectFactory.CreateObjectOfType(method));
			}
		}

		public List<long> Load(GameObjectFactory gameObjectFactory)
		{
			var result = new List<long>();

			_toCreate.ForEach(i => result.Add(gameObjectFactory.AddCreatedGameObject(gameObjectFactory.CreateObjectOfType(i))));

			return result;
		}

		public async Task SaveFile(string fileName)
		{
			using (StreamWriter streamWriter = new StreamWriter(fileName))
			{
				await streamWriter.WriteAsync(JsonConvert.SerializeObject(this));
			}
		}
	}
}
