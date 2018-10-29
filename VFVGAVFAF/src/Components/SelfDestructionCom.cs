using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class SelfDestructionCom: IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		[JsonIgnore]
		public double Cooldown { get { return 0; } }

		public EntityManager EntityManager { get; set; }

		public void Action()
		{
			EntityManager.RegsiterToDestory(EntID);
		}
	}
}
