﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface IValueCom: IComponent, IHaveAlias
	{
		[JsonIgnore]
		dynamic DValue { get; set; }
	}

	interface IValueCom<T>: IValueCom
	{
		T Value { get; set; }
	}
}
