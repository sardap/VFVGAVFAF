﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface IGameEventCom : IComponent
	{
		double TimeInbetweenRuns { get; }

		void Action();
	}
}
