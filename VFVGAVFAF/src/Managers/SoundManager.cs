using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Managers
{
	class SoundManager
	{
		private IDictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();
		private Song _song;
		private Settings _settings;

		private ContentManager _content { get; set; }

		public SoundManager(ContentManager content, Settings settings)
		{
			_content = content;
			_settings = settings;
		}

		public void PlaySong(string id)
		{
			MediaPlayer.Volume = _settings.MusicLevel;
			_song = _content.Load<Song>(id);
			MediaPlayer.Play(_song);
		}

		public void StopSong()
		{
			MediaPlayer.Stop();
		}

		public void PlaySound(string id)
		{
			GetSound(id).Play(_settings.VFXLevel, _settings.Pitch, 0);
		}

		private SoundEffect GetSound(string id)
		{
			if (!_soundEffects.ContainsKey(id))
			{
				var test = _content.Load<SoundEffect>(id);
				_soundEffects.Add(id, test);
			}

			return _soundEffects[id];
		}

	}
}
