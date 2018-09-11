using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class ComponentManager
	{
		private Dictionary<long, IComponent> _comTable = new Dictionary<long, IComponent>();
		private Dictionary<long, List<IComponent>> _enityComponets = new Dictionary<long, List<IComponent>>();

		private Stack<long> _nextIDS = new Stack<long>();
		private long _lastOrderedID = 0;
		
		public ComponentManager()
		{
			_nextIDS.Push(_lastOrderedID);
		}

		public long CreateComponet<T>(long entiyID, T com) where T : IComponent
		{
			var nextID = _nextIDS.Pop();
			Console.WriteLine(nextID);

			if (nextID == _lastOrderedID)
			{
				_lastOrderedID++;
				_nextIDS.Push(_lastOrderedID);
			}

			_comTable.Add(nextID, com);

			List<IComponent> components;

			if (!_enityComponets.ContainsKey(entiyID))
			{
				_enityComponets.Add(entiyID, new List<IComponent>());
				components = _enityComponets[entiyID];
			}
			else
			{
				components = _enityComponets[entiyID];
			}

			components.Add(_comTable[nextID]);

			return nextID;
		}

		public T GetComponent<T>(long id) where T : IComponent
		{
			return (T)_comTable[id];
		}

		public bool RemoveComponent(long entiyID, long comID)
		{
			_enityComponets[entiyID].Remove(_comTable[comID]);

			return true;
		}

		public bool DeystroyComponent(long comID)
		{
			var com = _comTable[comID];

			_comTable.Remove(comID);

			_nextIDS.Push(comID);

			return true;
		}

	}
}
