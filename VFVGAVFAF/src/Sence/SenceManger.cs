using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Sence
{
	class SenceManger : ISenceManger
	{
		private EntityManager _entityManager;
		private ISenceData _currentSence;
		private List<long> _processedGameObjects = new List<long>();

		public GameObjectFactory GameObjectFactory { get; set; }

		public SenceManger(EntityManager entityManager)
		{
			_entityManager = entityManager;
		}

		public void Load(ISenceData senceData)
		{
			if (_currentSence != null)
			{
				UnloadCurrent();
			}

			_currentSence = senceData;
			LoadSence(_currentSence);
		}

		public void UnloadCurrent()
		{
			UnloadSence(_currentSence);
		}

		private void LoadSence(ISenceData senceData)
		{

			foreach(var toCreate in senceData.CreatedGameObjects)
			{
				_processedGameObjects.Add(GameObjectFactory.AddCreatedGameObject(toCreate));
			}
		}

		private void UnloadSence(ISenceData senceData)
		{
			foreach(var created in _processedGameObjects)
			{
				_entityManager.RegsiterToDestory(created);
			}
		}
	}
}
