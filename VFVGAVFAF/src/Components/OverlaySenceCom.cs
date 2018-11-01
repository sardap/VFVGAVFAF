using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src.Components
{
	class OverlaySenceCom: IGameEventCom, ICanChainGameEvents, INeedSenceManger
	{
		//TODO MAKE THIS NOT SHIT
		private bool _ran = false;

		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; }

		public double Cooldown { get; set; }

		public List<string> EventAlias { get; set; }

		public string FileName { get; set; }

		public List<IPassValue> PassValues { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public EntityManager EntityManager { get; set; }

		public ISenceManger SenceManger { get; set; }

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);

			EventAlias.ForEach(i =>
				GameEvenetPostMaster.Add(EntityManager.GetEntiy<GameObject>(EntID).GetIdForAlais(i))
			);

			SenceManger.OverlayFromFile(FileName, PassValues);
			_ran = true;
		}
	}
}
