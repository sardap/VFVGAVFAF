using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Sence
{
	interface IFactorySenceData : ISenceData
	{
		IList<GameObjectFactory.GameObjects> ToCreate { get; }

		Task SaveFile(string fileName);
	}
}