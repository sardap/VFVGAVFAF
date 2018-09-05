using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace VFVGAVFAF.src.Components
{
	class RectPosCom : IPostionComponet
	{
		public Vector2 Postion { get; set; }
		
		public Rectangle Rectangle { get { return new Rectangle((int)Postion.X, (int)Postion.Y, 100, 100); } }
	}
}
