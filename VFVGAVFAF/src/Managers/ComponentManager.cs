using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src
{
	class ComponentManager
	{
		private Dictionary<long, IComponent> _comTable = new Dictionary<long, IComponent>();
		private Dictionary<long, HashSet<long>> _enityComponets = new Dictionary<long, HashSet<long>>();
		public IGameEvenetPostMaster PostMaster { get; set; }

		private Stack<long> _nextIDS = new Stack<long>();
		private long _lastOrderedID = 0;
		
		public ComponentManager()
		{
			_nextIDS.Push(_lastOrderedID);
		}

		public bool ComExists(long id)
		{
			return _comTable.ContainsKey(id);
		}

		public bool ComExistsAndOfType<T>(long id) where T : IComponent
		{
			return ComExists(id) && _comTable[id] is T;
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
				_enityComponets.Add(entiyID, new HashSet<long>());
			}

			_enityComponets[entiyID].Add(nextID);

			return nextID;
		}

		public T GetComponent<T>(long id) where T : IComponent
		{
			return (T)_comTable[id];
		}

		public bool RemoveComponent(long entiyID, long comID)
		{
			_enityComponets[entiyID].Remove(comID);

			return true;
		}

		public bool RemoveEnt(long entID)
		{
			_enityComponets.Remove(entID);

			return true;
		}

		public bool DeystroyComponent(long entID, long comID)
		{
			var com = _comTable[comID];

			_comTable.Remove(comID);

			_enityComponets[entID].Remove(comID);

			//_nextIDS.Push(comID);

			PostMaster.UnregsiterCom(comID);

			return true;
		}

		public HashSet<long> GetAllComIDForEnt(long entID)
		{
			return _enityComponets[entID];
		}

	}
}
