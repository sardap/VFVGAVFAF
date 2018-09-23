using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Managers
{
	interface IStepManager : IManger
	{
		void Step(double deltaTime);
	}
}
