using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class CirclePostionCom: IPostionComponet, IHaveAlias, IGetSizeCom
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public Postion2D Postion { get; set; }

		public double Radius { get; set; }

		public CircleF ToMonogameCircle()
		{
			return new CircleF(Postion.ToPoint2(), (float)Radius);
		}

		public Vector2 Size()
		{
			return new Postion2D(Radius, Radius).ToVector();
		}

		public void SetPostion(Postion2D nextPostion)
		{
			Postion = nextPostion;
		}

		public Postion2D GetPostion()
		{
			return Postion;
		}

		public void ResetPostion()
		{
			throw new NotImplementedException();
		}
	}
}
