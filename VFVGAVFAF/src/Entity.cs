using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class Entity<T> : IEntity
	{
		ComponentManager _componentManager;

		private long _entiyID;
		private bool _active;

		public long GetID
		{
			get { return _entiyID; }
		}

		public int TypeID
		{
			get { return typeof(T).GetHashCode(); }
		}

		public T GetComponent<T>() where T : IComponent
		{
			return _componentManager.GetComponent<T>(_entiyID);
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
