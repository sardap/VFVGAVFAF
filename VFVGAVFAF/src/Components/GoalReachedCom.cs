﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class GoalReachedCom : IGameEventCom
	{
		public double TimeToComeplte { get; set; }

		public void Action()
		{
			Console.WriteLine("You won");
		}
	}
}
