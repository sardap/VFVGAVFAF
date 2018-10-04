using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class GoalReachedCom : IGameEventCom
	{
		public double TimeToComplete { get; set; }
		public double Cooldown { get; set; }
		public string Alias { get; set; }

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
