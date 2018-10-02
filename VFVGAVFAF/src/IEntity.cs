﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	interface IEntity
	{
		int TypeID { get; }
		long GetID { get; }

		List<IComponent> GetComponents { get; }

		void SetID(long id);

		void UnregstierComsFromMangers();
	}
}
