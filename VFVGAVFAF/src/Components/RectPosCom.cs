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
		private Paultangle _rectangle = new Paultangle();
		private ComponentManager _componentManager;
		private Paultangle _startingPostion;
	
		public void SetPostion(Postion2D postion2D)
		{
			var oldPostion = new Postion2D(_rectangle.Postion2D);
			_rectangle.Postion2D.X = postion2D.X;
			_rectangle.Postion2D.Y = postion2D.Y;

			bool result = true;

			foreach(var i in PostionConstrantComs)
			{
				result = result && _componentManager.GetComponent<IPostionConstrantCom>(i).Check(Rectangle);
			}

			if(!result)
			{
				_rectangle.Postion2D.X = oldPostion.X;
				_rectangle.Postion2D.Y = oldPostion.Y;
			}
		}

		public Postion2D GetPostion()
		{
			return new Postion2D(_rectangle.Postion2D);
		}

		public void ResetPostion()
		{
			SetPostion(new Postion2D(100, 100));
		}
		
		public List<long> PostionConstrantComs { get; set; }

		public Rectangle Rectangle { get { return _rectangle.ToMonoGameRectangle(); } }

		public RectPosCom(ComponentManager componentManager, Rectangle rectangle)
		{
			_componentManager = componentManager;
			PostionConstrantComs = new List<long>();
			_rectangle = new Paultangle(rectangle);
			_startingPostion = new Paultangle(_rectangle);
		}

		public RectPosCom(ComponentManager componentManager, Postion2D postion2D) : this(componentManager, postion2D.ToRectangle(0, 0))
		{
		}
	}
}
