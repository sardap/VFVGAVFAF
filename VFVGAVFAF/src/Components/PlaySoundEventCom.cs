using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class PlaySoundEventCom : ISoundEventCom
	{
		private SoundManager _soundManager;
		private string _song;

		public double TimeToComplete { get; set; }
		public double Cooldown { get; set; }

		public long EntID { get; set; }

		public PlaySoundEventCom(SoundManager soundManager, string song)
		{
			_song = song;
			_soundManager = soundManager;
		}

		public void Action()
		{
			MediaPlayer.Volume = 1.0f;
			_soundManager.GetSound(_song).Play();
		}
	}
}
