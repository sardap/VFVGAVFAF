﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;

namespace VFVGAVFAF.src
{
	interface IGameEvenetPostMaster
	{
		void Add(long gameEvenetComID);

		void Add(long gameEvenetComID, long otherID);

		void UnregsiterCom(long id);

		void Step(double deltaTime);
	}
}
