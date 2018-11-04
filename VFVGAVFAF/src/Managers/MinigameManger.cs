using Newtonsoft.Json;
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
		private string _loaded = null;
		private int _failures;

		public List<string> MinigameFiles = new List<string>();

		public string FinalGame { get; set; }

		public string FinalScreen { get; set; }

		public string FailureScreen { get; set; }

		public int FailuresAllowed { get; set; }

		[JsonIgnore]
		public ISenceManger SenceManger { get; set; }

		public void PlayNext(bool lastGameResult)
		{
			if (lastGameResult && _loaded != null)
			{
				_played.Add(_loaded);
			}
			else if(_loaded != null)
			{
				_failures++;
			}

			if (_failures > FailuresAllowed)
			{
				PlayNext(FailureScreen);
				return;
			}

			if(_played.Count() == MinigameFiles.Count())
			{
				PlayNext(FinalGame);
				return;
			}

			if (_played.Count() > MinigameFiles.Count())
			{
				PlayNext(FinalScreen);
				return;
			}

			_active = Utils.RemoveAll(MinigameFiles, _played);

			var next = Utils.RandomElement(_active);

			PlayNext(next);
				
			_loaded = next;
		}

		public void PlayAgain()
		{
			SenceManger.LoadMainFile(_loaded, new List<IPassValue>());
		}
		
		private void PlayNext(string next)
		{
			SenceManger.LoadMainFile(next, new List<IPassValue>());
		}
	}
}
