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
			foreach(var toCreate in senceData.ToCreate)
			{
				senceData.CreatedEntites.Add(GameObjectFactory.CreateObjectOfType(toCreate));
			}
		}

		private void UnloadSence(ISenceData senceData)
		{
			foreach(var created in senceData.CreatedEntites)
			{
				_entityManager.RegsiterToDestory(created);
			}
		}
	}
}
