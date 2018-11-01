﻿using System;
using System.Collections.Concurrent;
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
		private Dictionary<long, IManger> _disalbedComMangers = new Dictionary<long, IManger>();

		private Dictionary<long, Type> _ownedComs = new Dictionary<long, Type>();
		private ConcurrentDictionary<Type, IList<long>> _ownedComsTypes = new ConcurrentDictionary<Type, IList<long>>();

		private ConcurrentDictionary<string, long> _alaisTable = new ConcurrentDictionary<string, long>();
		private Dictionary<long, bool> _enabledComs = new Dictionary<long, bool>();

		private bool _alreadySet = false;
		private long _entiyID;

		public List<string> Tags { get; set; }

		public long GetID
		{
			get { return _entiyID; }
		}

		public int TypeID
		{
			get { return typeof(T).GetHashCode(); }
		}

		public string Alias { get; set; }

		public Entity(ComponentManager componentManager)
		{
			_componentManager = componentManager;
		}

		~Entity()
		{
			UnregstierComsFromMangers();
		}

		public void SetID(long id)
		{
			if (_alreadySet)
			{
				throw new System.Exception("Already set ID");
			}

			_entiyID = id;
			_alreadySet = true;
		}

		public void KillYourself()
		{
			foreach(var entry in _ownedComs)
			{
				_componentManager.DeystroyComponent(GetID, entry.Key);
			}

			_componentManager.RemoveEnt(_entiyID);
		}

		public List<Tuple<long, IComponent>> GetComponents
		{
			get
			{
				var result = new List<Tuple<long, IComponent>>();

				foreach(var entry in _ownedComs)
				{
					result.Add(new Tuple<long, IComponent>(entry.Key, GetComponent<IComponent>(entry.Key)));
				}

				return result;
			}
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

		public long GetIdForAlais(string alais)
		{
			return _alaisTable[alais];
		}

		public bool ComEnabled(long id)
		{
			return true;
		}

		public void EnableCom(long id)
		{
			if(_disalbedComMangers.ContainsKey(id))
			{
				_comMangers.Add(id, _disalbedComMangers[id]);
				_comMangers[id].Regsiter(id);
				_disalbedComMangers.Remove(id);
			}
		}

		public void DisableCom(long id)
		{
			if(_comMangers.ContainsKey(id))
			{
				_disalbedComMangers.Add(id, _comMangers[id]);
				_comMangers[id].UnRegsiter(id);
				_comMangers.Remove(id);
			}
		}

		public void DisableCom(long id, List<MangersEnum> types)
		{
			if (_comMangers.ContainsKey(id) && types.Any(i => i == Utils.MangerToMangerEnum(_comMangers[id])))
			{
				DisableCom(id);
			}
		}

		public bool ToggleCom(long id)
		{
			_enabledComs[id] = !_enabledComs[id];

			if(!_enabledComs[id])
			{
				DisableCom(id);
			}
			else
			{
				EnableCom(id);
			}

			return _enabledComs[id];
		}

		public void Freeze(List<MangersEnum> types)
		{
			var allComs = new List<long>();

			foreach (var entry in _componentManager.GetAllComIDForEnt(_entiyID))
			{
				DisableCom(entry, types);
			}

		}

		public void Defrost()
		{

		}

		public ComT GetComponent<ComT>(long id) where ComT : IComponent
		{
			if(!_ownedComs.ContainsKey(id))
			{
				throw new KeyNotFoundException("Entiy does not own this com");
			}

			if(!_enabledComs[id])
			{
				throw new System.Exception("Cannot access disabled Com");
			}

			return _componentManager.GetComponent<ComT>(id);
		}

		public long GetFirstComponentID<ComT>() where ComT : IComponent
		{
			return _ownedComsTypes[typeof(ComT)][0];
		}

		public ComT GetFirstComponent<ComT>() where ComT : IComponent
		{
			return GetComponent<ComT>().First();
		}

		public ComT GetComponent<ComT>(string alais) where ComT : IComponent
		{
			return GetComponent<ComT>(_alaisTable[alais]);
		}

		public IList<ComT> GetComponent<ComT>() where ComT : IComponent
		{
			var result = new List<ComT>();

			List<ComT> foundComs = new List<ComT>();
			foreach (var enty in _ownedComs)
			{
				var toFindType = typeof(ComT);
				var com = GetComponent<IComponent>(enty.Key);
				if (com is ComT && ComEnabled(enty.Key))
				{
					foundComs.Add(_componentManager.GetComponent<ComT>(enty.Key));
				}
			}

			return foundComs;
		}

		public bool HasComType<ComT>() where ComT : IComponent
		{
			return GetComponent<ComT>().Count > 0;
		}

		public long AddComponent<ComT>(ComT com) where ComT : IComponent
		{
			var id = _componentManager.CreateComponet(_entiyID, com);
			_ownedComs.Add(id, typeof(ComT));
			com.EntID = _entiyID;

			if(com is IHaveAlias)
			{
				if(((IHaveAlias)com).Alias != null)
					_alaisTable.TryAdd(((IHaveAlias)com).Alias, id);
			}

			if (!_ownedComsTypes.ContainsKey(typeof(ComT)))
			{
				_ownedComsTypes.TryAdd(typeof(ComT), new List<long>());
			}

			_ownedComsTypes[typeof(ComT)].Add(id);
			_enabledComs.Add(id, true);

			return id;
		}

		public bool RemoveComponet<ComT>(long comID) where ComT : IComponent
		{
			_ownedComsTypes[typeof(ComT)].Remove(comID);
			_ownedComs.Remove(comID);
			_enabledComs.Remove(comID);
			return _componentManager.RemoveComponent(_entiyID, comID);
		}

		public bool Equals(Entity<T> other)
		{
			return _entiyID == other._entiyID;
		}
	}
}
