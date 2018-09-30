using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class ColInfo : IColInfo
	{
		public long EntID { get; set; }

		public ColInfo(long entID)
		{
			EntID = entID;
		}
	}
}
