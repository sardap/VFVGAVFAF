﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface INeedEnityManger
	{
		[JsonIgnore]
		EntityManager EntityManager { get; set; }
	}
}
