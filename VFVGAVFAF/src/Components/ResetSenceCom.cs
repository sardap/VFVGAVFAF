using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Components
{
	class ResetSenceCom : IGameEventCom, INeedMinigameManger
	{
		public long EntID { get; set; }
		public double TimeToComplete { get; set; }
		public double Cooldown { get; set; }
		public string Alias { get; set; }
		public MinigameManger MinigameManger { get; set; }

		public void Action()
		{
			MinigameManger.PlayAgain();
		}
	}
}
