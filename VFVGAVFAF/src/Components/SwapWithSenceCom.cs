using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Components
{
	class SwapWithSenceCom: IGameEventCom,  INeedSenceManger, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		public string NextSence { get; set; }

		public List<IPassValue> ToPass { get; set; }

		public ISenceManger SenceManger { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);

			for (int i = 0; i < ToPass.Count; i++)
			{
				if(ToPass[i] is INeedFromCom)
				{
					var com = ent.GetComponent<IValueCom>(((INeedFromCom)ToPass[i]).Alias);

					((INeedFromCom)ToPass[i]).SetValue(com.DValue);
				}
			}

			SenceManger.LoadMainFile(NextSence, ToPass);
		}
	}
}
