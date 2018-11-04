﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface IValueCom: IComponent, IHaveAlias
	{
		dynamic DValue { get; set; }
	}

	interface IValueCom<T>: IComponent, IHaveAlias
	{
		T Value { get; set; }
	}
}
