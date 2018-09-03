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
		public Vector2 Postion
		{
			get
			{
				return new Vector2 { X = Rectangle.X, Y = Rectangle.Y };
			}
			set
			{
				var newRect = Rectangle;

				newRect.X = (int)value.X;
				newRect.Y = (int)value.Y;

				Rectangle = newRect;
			}
		}

		public Rectangle Rectangle { get; set; }
	}
}
