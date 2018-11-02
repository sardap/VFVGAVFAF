using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class PlayRandomSoundEvenetCom: ISoundEventCom, INeedSoundManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; }

		public double Cooldown { get; }

		public List<string> Sounds { get; set; }

		public SoundManager SoundManager { get; set; }

		public void Action()
		{
			SoundManager.GetSound(Utils.RandomElement(Sounds)).Play();
		}
	}
}
