using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Components
{
	class ChangeSenceCom : IGameEventCom, INeedSenceManger
	{
		public long EntID { get; set; }
		public double TimeToComplete { get; set; }
		public double Cooldown { get; set; }
		public string Alias { get; set; }
		public ISenceManger SenceManger { get; set; }
		public string FileName { get; set; }

		public void Action()
		{
			SenceManger.LoadFromFile(FileName);
		}
	}
}
