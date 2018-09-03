using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class EntityManager
	{
		private Dictionary<long, IEntity> _entityTable = new Dictionary<long, IEntity>();

		private Stack<long> _nextIDs = new Stack<long>();
		private long _lastOrderedID = 0;

		public EntityManager()
		{
			_nextIDs.Push(_lastOrderedID);
		}

		public long CreateEntity(IEntity entity)
		{
			long newID = _nextIDs.Pop();
			if (newID == _lastOrderedID)
			{
				_lastOrderedID++;
				_nextIDs.Push(_lastOrderedID);
			}

			_entityTable.Add(newID, entity);
			return newID;
		}

		public T GetEntiy<T>(long id) where T : IEntity
		{
			return (T)_entityTable[id];
		}

		private void DestroyEntity(long id)
		{
			_nextIDs.Push(id);
			_entityTable.Remove(id);
		}
	}
}
