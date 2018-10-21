using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface IColsionGameEventCom : IGameEventCom
	{
		[JsonIgnore]
		long ActiveID { get; set; }

		[JsonIgnore]
		Dictionary<long, IColInfo> ColliedWithTable { get; set; }
	}
}
