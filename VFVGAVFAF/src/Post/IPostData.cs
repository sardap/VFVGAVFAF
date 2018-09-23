using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	interface IPostData
	{
		double TimeToComplete { get; set; }

		long GameEventID { get; }
	}
}
