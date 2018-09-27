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
				_posts.Push(new PostData { TimeToComplete = gameEvenet.TimeInbetweenRuns, GameEventID = id});
				_counts[id]++;
			}
		}

		public void Step(double deltaTime)
		{
			ConcurrentStack<IPostData> nextStack = new ConcurrentStack<IPostData>();

			foreach(var post in _posts)
			{
				post.TimeToComplete -= deltaTime;

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
