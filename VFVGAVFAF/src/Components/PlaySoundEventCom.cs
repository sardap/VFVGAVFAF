using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	[Serializable]
	class PlaySoundEventCom : ISoundEventCom, INeedSoundManger, IHaveAlias
	{
		public string Song { get; set; }

		public long EntID { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public SoundManager SoundManager { get; set; }

		public string Alias { get; set; }

		public PlaySoundEventCom()
		{
		}

		public void Action()
		{
			SoundManager.PlaySound(Song);
		}
	}
}
