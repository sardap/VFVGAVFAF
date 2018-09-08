using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class Entity<T> : IEntity
	{
		private ComponentManager _componentManager;
		private Dictionary<long, IManger> _comMangers = new Dictionary<long, IManger>();

		private bool _alreadySet = false;
		private long _entiyID;
		private bool _active;

		public void SetID(long id)
		{
			if(_alreadySet)
			{
				throw new System.Exception("Already set ID");
			}

			_entiyID = id;
			_alreadySet = true;
		}

		public Entity(ComponentManager componentManager)
		{
			_componentManager = componentManager;
		}

		~Entity()
		{
			UnregstierComsFromMangers();
		}

		public long GetID
		{
			get { return _entiyID; }
		}

		public int TypeID
		{
			get { return typeof(T).GetHashCode(); }
		}

		public void RegsiterToManager(long comID, IManger manger)
		{
			manger.Regsiter(comID);
			_comMangers.Add(comID, manger);
		}

		public void UnregstierComsFromMangers()
		{
			foreach (var keyValue in _comMangers)
			{
				keyValue.Value.UnRegsiter(keyValue.Key);
			}
		}

		public T GetComponent<T>() where T : IComponent
		{
			return _componentManager.GetComponent<T>(_entiyID);
		}

		public long AddComponent<T>(T com) where T : IComponent
		{
			return _componentManager.CreateComponet(_entiyID, com);
		}

		public bool RemoveComponet<T>(long comID) where T : IComponent
		{
			return _componentManager.RemoveComponent(_entiyID, comID);
		}

		public bool Equals(Entity<T> other)
		{
			return _entiyID == other._entiyID;
		}
	}
}
