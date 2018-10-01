using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Components
{
	class LoadMiniGameCom : IGameEventCom
	{
		ISenceManger _senceManger;
		ISenceData _nextSence;

		public long EntID { get; set; }
		public double Cooldown { get; set; }
		public double TimeToComplete { get { return 0; } }

		public LoadMiniGameCom(long entID, ISenceManger senceManger, ISenceData senceData)
		{
			EntID = entID;
			_senceManger = senceManger;
			_nextSence = senceData;
		}

		public void Action()
		{
			_senceManger.Load(_nextSence);
		}

	}
}
