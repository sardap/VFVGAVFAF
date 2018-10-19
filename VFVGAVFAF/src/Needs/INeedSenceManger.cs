using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Sence;

namespace VFVGAVFAF.src
{
	interface INeedSenceManger
	{
		[JsonIgnore]
		ISenceManger SenceManger { get; set; }
	}
}
