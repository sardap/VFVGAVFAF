using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface ICollisionCom : IComponent
	{
		Paultangle GetHitBox { get; }

		void Check(long otherEntID, long otherID);
	}
}
