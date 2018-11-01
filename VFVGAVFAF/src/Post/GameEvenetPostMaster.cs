using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;

namespace VFVGAVFAF.src
{
	class GameEvenetPostMaster : IGameEvenetPostMaster
	{
		struct STuple
		{
			public STuple(long one, long two)
			{
				One = one;
				Two = two;
			}

			public long One;
			public long Two;

			public bool Ether(long other)
			{
				return One == other || Two == other;
			}
		}

		private ComponentManager _componentManager;
		private Dictionary<STuple, IPostData> _posts = new Dictionary<STuple, IPostData>();
		private Dictionary<STuple, double> _cooldownTable = new Dictionary<STuple, double>();
		private HashSet<long> _removedLastStep = new HashSet<long>();

		public GameEvenetPostMaster(ComponentManager componentManager)
		{
			_componentManager = componentManager;
		}

		public void UnregsiterCom(long id)
		{
			_removedLastStep.Add(id);

			List<STuple> toRemove = new List<STuple>();

			foreach(var post in _posts)
			{
				if(post.Key.Ether(id))
				{
					toRemove.Add(post.Key);
				}
			}

			Utils.RemoveAll(_posts, toRemove);

			toRemove = new List<STuple>();

			foreach (var entry in _cooldownTable)
			{
				if(entry.Key.Ether(id))
				{
					toRemove.Add(entry.Key);
				}
			}

			Utils.RemoveAll(_cooldownTable, toRemove);
		}

		public void Add(long id, long otherID)
		{
			var key = new STuple(id, otherID);

			var gameEvenet = _componentManager.GetComponent<IGameEventCom>(id);

			if (!_cooldownTable.ContainsKey(key))
			{
				_cooldownTable.Add(key, 0);
			}

			if (_cooldownTable[key] <= 0)
			{
				Add(key, gameEvenet);
				_cooldownTable[key] = gameEvenet.Cooldown;
			}
		}

		public void Add(long id)
		{
			Add(id, long.MinValue);
		}

		public void Step(double deltaTime)
		{
			var nextChanges = new Dictionary<STuple, IPostData>();
			var toChange = new Stack<KeyValuePair<STuple, double>>();

			foreach (var entry in _cooldownTable)
			{
				var key = entry.Key;
				var id = key.One;
				var cooldown = _cooldownTable[key];
				var com = _componentManager.GetComponent<IGameEventCom>(id);
				double nextValue = cooldown <= 0 ? com.Cooldown : _cooldownTable[key] - deltaTime;
				toChange.Push(new KeyValuePair<STuple, double>(new STuple(key.One, key.Two), nextValue));
			}

			while(toChange.Count > 0)
			{
				var top = toChange.Pop();

				_cooldownTable[top.Key] = top.Value;
			}

			Dictionary<STuple, IPostData> postsCopy = new Dictionary<STuple, IPostData>();

			postsCopy = _posts.ToList().ToDictionary(i => i.Key, i => i.Value);

			foreach (var entry in postsCopy)
			{

				var value = entry.Value;
				var key = entry.Key;

				value.TimeToComplete -= deltaTime;
				//Console.WriteLine("ID{0}\tTIME LEFT:{1}", value.GameEventID, value.TimeToComplete);

				if (value.TimeToComplete <= 0)
				{

					var com = _componentManager.GetComponent<IGameEventCom>(value.GameEventID);

					if (com is IColsionGameEventCom)
					{
						((IColsionGameEventCom)com).ActiveID = key.Two;
						Console.WriteLine("ACTIVE ID SET {0}", com);
					}

					Console.WriteLine("RUNNING ID: {0} COM TYPE {1}", value.GameEventID, com);

					com.Action();

					_posts.Remove(key);
				}
			}

			_removedLastStep.Clear();
		}

		private void Add(STuple key, IGameEventCom gameEvenet)
		{
			Console.WriteLine("ADDING POST TO POSTS ID:{0} TTC:{1}", key.One, gameEvenet.TimeToComplete);
			_posts.Add(key, new PostData { TimeToComplete = gameEvenet.TimeToComplete, GameEventID = key.One });
		}
	}
}
