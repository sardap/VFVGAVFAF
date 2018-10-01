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
		private ComponentManager _componentManager;
		private ConcurrentStack<IPostData> _posts = new ConcurrentStack<IPostData>();
		private ConcurrentDictionary<long, int> _counts = new ConcurrentDictionary<long, int>();

		public GameEvenetPostMaster(ComponentManager componentManager)
		{
			_componentManager = componentManager;
		}

		public void Add(long id)
		{
			int numberOfInstances = 1;
			var gameEvenet = _componentManager.GetComponent<IGameEventCom>(id);

			if(!_counts.ContainsKey(id))
			{
				_counts.TryAdd(id, 0);
			}

			if(_counts[id] < numberOfInstances)
			{
				Console.WriteLine("ADDING POST ID{0} COUNTS:{1} TTC:{2}", id, _counts[id], gameEvenet.TimeInbetweenRuns);
				_posts.Push(new PostData { TimeToComplete = gameEvenet.TimeInbetweenRuns, GameEventID = id});
				_counts.TryUpdate(id, _counts[id] + 1, _counts[id]);
			}
		}

		public void Step(double deltaTime)
		{
			ConcurrentStack<IPostData> nextStack = new ConcurrentStack<IPostData>();

			while(_posts.Count > 0)
			{
				_posts.TryPop(out IPostData post);
				post.TimeToComplete -= deltaTime;
				Console.WriteLine("ID{0}\tTIME LEFT:{1}", post.GameEventID, post.TimeToComplete);

				if(post.TimeToComplete < 0)
				{
					Console.WriteLine("RUNNING ID: {0}", post.GameEventID);
					_componentManager.GetComponent<IGameEventCom>(post.GameEventID).Action();
					_counts[post.GameEventID]--;
				}
				else
				{
					nextStack.Push(post);
				}
			}

			_posts = nextStack;
		}
	}
}
