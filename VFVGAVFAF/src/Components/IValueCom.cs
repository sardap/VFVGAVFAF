﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface IValueCom<T>: IComponent
	{
		T Value { get; set; }
	}
}