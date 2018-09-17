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
		private Stack<long> _toDestroy = new Stack<long>();
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
			entity.SetID(newID);
			return newID;
		}

		public T GetEntiy<T>(long id) where T : IEntity
		{
			return (T)_entityTable[id];
		}

		public void RegsiterToDestory(long id)
		{
			_toDestroy.Push(id);
		}

		public void Step()
		{
			while(_toDestroy.Count > 0)
			{
				DestroyEntity(_toDestroy.Pop());
			}
		}

		private void DestroyEntity(long id)
		{
			_nextIDs.Push(id);
			IEntity entity = GetEntiy<IEntity>(id);
			_entityTable.Remove(id);
			entity.UnregstierComsFromMangers();
		}
	}
}
