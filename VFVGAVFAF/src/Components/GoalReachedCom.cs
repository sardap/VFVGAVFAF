using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class GoalReachedCom : IGameEventCom
	{
		public double TimeInbetweenRuns { get; set; }

		public long EntID { get; set; }

		public GoalReachedCom(long entID)
		{
			EntID = entID;
		}

		public void Action()
		{
			Console.WriteLine("You won");
		}
	}
}
