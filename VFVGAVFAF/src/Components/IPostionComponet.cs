using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	interface IPostionComponet : IComponent
	{
		// Not using Get and set prop becuase i was running into debuging issues
		void SetPostion(Postion2D postion2D);
		Postion2D GetPostion();
		void ResetPostion();
	}
}
