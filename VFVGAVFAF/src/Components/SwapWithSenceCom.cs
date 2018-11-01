using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Components
{
	class SwapWithSenceCom: IGameEventCom,  INeedSenceManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public string NextSence { get; set; }

		public List<IPassValue> ToPass { get; set; }

		public ISenceManger SenceManger { get; set; }

		public void Action()
		{
			SenceManger.LoadMainFile(NextSence, ToPass);
		}
	}
}
