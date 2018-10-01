using Microsoft.Xna.Framework;
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
		IColInfo ColliedWith { get; set; }
	}
}
