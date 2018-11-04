using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class PlayNextMiniGameCom: IGameEventCom, INeedMinigameManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; }

		public double Cooldown { get; }

		public MinigameManger MinigameManger { get; set; }

		public bool GameResult { get; set; }


		public void Action()
		{
			MinigameManger.PlayNext(GameResult);
		}

	}
}
