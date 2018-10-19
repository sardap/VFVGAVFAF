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
		struct STuple<T1, T2>
		{
			public STuple(T1 one, T2 two)
			{
				One = one;
				Two = two;
			}

			public T1 One;
			public T2 Two;
		}

		private ComponentManager _componentManager;
		private Dictionary<long, IPostData> _posts = new Dictionary<long, IPostData>();
		private Dictionary<long, double> _cooldownTable = new Dictionary<long, double>();
		private HashSet<long> _removedLastStep = new HashSet<long>();

		public GameEvenetPostMaster(ComponentManager componentManager)
		{
			_componentManager = componentManager;
		}

		public void UnregsiterCom(long id)
		{
			_removedLastStep.Add(id);

			_posts.Remove(id);

			_cooldownTable.Remove(id);
		}

		public void Add(long id)
		{
			var gameEvenet = _componentManager.GetComponent<IGameEventCom>(id);

			if (!_cooldownTable.ContainsKey(id))
			{
				_cooldownTable.Add(id, 0);
			}

			if(_cooldownTable[id] <= 0)
			{
				Add(id, gameEvenet);
				_cooldownTable[id] = gameEvenet.Cooldown;
			}
		}

		public void Step(double deltaTime)
		{
			var nextPosts = new Dictionary<long, IPostData>();
			var toChange = new Stack<KeyValuePair<long, double>>();

			foreach (var entry in _cooldownTable)
			{
				var id = entry.Key;
				var cooldown = _cooldownTable[id];
				var com = _componentManager.GetComponent<IGameEventCom>(id);
				double nextValue = cooldown <= 0 ? com.Cooldown : _cooldownTable[id] - deltaTime;
				toChange.Push(new KeyValuePair<long, double>(id, nextValue));
			}

			while(toChange.Count > 0)
			{
				var top = toChange.Pop();

				_cooldownTable[top.Key] = top.Value;
			}

			foreach (var post in _posts.Values)
			{
				if(!_removedLastStep.Contains(post.GameEventID))
				{
					post.TimeToComplete -= deltaTime;
					Console.WriteLine("ID{0}\tTIME LEFT:{1}", post.GameEventID, post.TimeToComplete);

					if (post.TimeToComplete <= 0)
					{
						Console.WriteLine("RUNNING ID: {0}", post.GameEventID);
						_componentManager.GetComponent<IGameEventCom>(post.GameEventID).Action();
					}
					else
					{
						nextPosts.Add(post.GameEventID, post);
					}
				}
			}

			_posts = nextPosts;
			_removedLastStep.Clear();
		}

		private void Add(long id, IGameEventCom gameEvenet)
		{
			Console.WriteLine("ADDING POST ID{0} TTC:{1}", id, gameEvenet.TimeToComplete);
			_posts.Add(id, new PostData { TimeToComplete = gameEvenet.TimeToComplete, GameEventID = id });
		}
	}
}
