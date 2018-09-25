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

		private ContentManager _content { get; set; }

		public SoundManager(ContentManager content)
		{
			_content = content;
		}

		public SoundEffect GetSound(string id)
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
