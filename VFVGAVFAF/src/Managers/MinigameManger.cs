using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Managers
{
	class MinigameManger
	{
		private List<string> _played = new List<string>();
		private List<string> _active = new List<string>();

		public List<string> MinigameFiles = new List<string>();

		public ISenceManger SenceManger { get; set; }

		public void PlayNext()
		{
			_active = Utils.RemoveAll(MinigameFiles, _played);

			var next = Utils.RandomEntry(_active);

			SenceManger.LoadFromFile(next);

			_played.Add(next);

			if(_played.Count >= MinigameFiles.Count)
			{
				_played.Clear();
			}
		}
	}
}
