using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	[Obsolete("Use Game event + trigger at start", false)]
	interface IRunXTimesCom: IComponent, IStepCom
	{
		long RunsRemaining { get; set; }
	}
}
