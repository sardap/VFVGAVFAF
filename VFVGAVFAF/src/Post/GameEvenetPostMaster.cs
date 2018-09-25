using System;
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
		private IList<IPostData> _posts = new List<IPostData>();
		private IDictionary<long, int> _counts = new Dictionary<long, int>();

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
				_counts.Add(id, 0);
			}

			if(_counts[id] < numberOfInstances)
			{
				_posts.Add(new PostData { TimeToComplete = gameEvenet.TimeInbetweenRuns, GameEventID = id});
				_counts[id]++;
			}
		}

		public void Step(double deltaTime)
		{
			Stack<IPostData> toRemove = new Stack<IPostData>();

			foreach(var post in _posts)
			{
				post.TimeToComplete -= deltaTime;

				if(post.TimeToComplete < 0)
				{
					Console.WriteLine("RUNNING ID: {0}", post.GameEventID);
					_componentManager.GetComponent<IGameEventCom>(post.GameEventID).Action();
					toRemove.Push(post);
				}
			}

			while(toRemove.Count > 0)
			{
				_counts[toRemove.Peek().GameEventID]--;
				_posts.Remove(toRemove.Pop());
			}
		}
	}
}
