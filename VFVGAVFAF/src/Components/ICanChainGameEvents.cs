using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface ICanChainGameEvents: IComponent, INeedPostMaster, INeedEnityManger
	{
		List<string> EventAlias { get; set; }
	}
}
