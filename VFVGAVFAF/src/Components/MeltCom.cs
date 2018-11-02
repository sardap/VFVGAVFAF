using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class MeltCom : IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public string MatchPattern { get; set; }

		public List<MangersEnum> Mangers { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Action()
		{
			EntityManager.Melt(MatchPattern, Mangers);
		}
	}
}
