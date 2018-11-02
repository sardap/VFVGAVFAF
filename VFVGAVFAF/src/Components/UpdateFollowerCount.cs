using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class UpdateFollowerCount: DoubleValueUpdateCom, IColsionGameEventCom
	{
		private HashSet<long> _colliedWith = new HashSet<long>();

		public long ActiveID { get; set; }

		public Dictionary<long, IColInfo> ColliedWithTable { get; set; }

		public UpdateFollowerCount(): base()
		{
			ColliedWithTable = new Dictionary<long, IColInfo>();
		}

		public override void Action()
		{
			if(!_colliedWith.Contains(ColliedWithTable[ActiveID].EntID))
			{
				_colliedWith.Add(ColliedWithTable[ActiveID].EntID);
				base.Action();
			}
			else
			{
				Console.Write("Already Added to followers");
			}
		}
	}
}
