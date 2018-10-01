﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Sence
{
	interface ISenceData
	{
		IList<GameObjectFactory.GameObjects> ToCreate { get; }
		IList<long> CreatedEntites { get; }

		Task SaveFile(string fileName);
	}
}
