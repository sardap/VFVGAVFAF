using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class StopMusicCom: IGameEventCom, INeedSoundManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public SoundManager SoundManager { get; set; }

		public void Action()
		{
			SoundManager.StopSong();
		}
	}
}
