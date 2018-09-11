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
		private Rectangle _rectangle = new Rectangle();
		private ComponentManager _componentManager;

		public void SetPostion(Postion2D postion2D)
		{
			var oldPostion = new Postion2D(_rectangle.X, _rectangle.Y);
			_rectangle.X = (int)postion2D.X;
			_rectangle.Y = (int)postion2D.Y;

			bool result = true;

			foreach(var i in PostionConstrantComs)
			{
				result = result && _componentManager.GetComponent<IPostionConstrantCom>(i).Check(Rectangle);
			}

			if(!result)
			{
				_rectangle.X = (int)oldPostion.X;
				_rectangle.Y = (int)oldPostion.Y;
			}

			/*
			if (!PostionConstrantComs.TrueForAll(i => _componentManager.GetComponent<IPostionConstrantCom>(i).Check(Rectangle)))
			{
				_postion = oldPostion;
			}
			*/
		}

		public Postion2D GetPostion()
		{
			return new Postion2D(_rectangle);
		}
		
		public List<long> PostionConstrantComs { get; set; }

		public Rectangle Rectangle { get { return _rectangle; } }

		public RectPosCom(ComponentManager componentManager, Rectangle rectangle)
		{
			_componentManager = componentManager;
			PostionConstrantComs = new List<long>();
			_rectangle = rectangle;
		}

		public RectPosCom(ComponentManager componentManager, Postion2D postion2D) : this(componentManager, postion2D.ToRectangle(0, 0))
		{
		}
	}
}
