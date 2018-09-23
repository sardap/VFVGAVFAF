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
		private Dictionary<long, Type> _ownedComs = new Dictionary<long, Type>();

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

		public IList<long> GetComponent<ComT>() where ComT : IComponent
		{
			var result = new List<long>();
			var foundComs = _ownedComs.ToList().FindAll(i => i.Value == typeof(ComT));
			foundComs.ForEach(i => result.Add(i.Key));
			return result;
		}

		public long AddComponent<ComT>(ComT com) where ComT : IComponent
		{
			var id = _componentManager.CreateComponet(_entiyID, com);
			_ownedComs.Add(id, typeof(ComT));
			return id;
		}

		public bool RemoveComponet<ComT>(long comID) where ComT : IComponent
		{
			_ownedComs.Remove(comID);
			return _componentManager.RemoveComponent(_entiyID, comID);
		}

		public bool Equals(Entity<T> other)
		{
			return _entiyID == other._entiyID;
		}
	}
}
