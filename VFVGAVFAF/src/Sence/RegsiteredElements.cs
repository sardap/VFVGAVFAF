﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Sence
{
	interface IRegsiteredElements
	{
		List<long> RegisteredElements { get; }

		IManger Manger { get; }
	}
}
