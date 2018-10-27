using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class Postion2DCom: IPostionComponet, IHaveAlias
	{
		public long EntID { get; set; }

		public Postion2D Postion2D { get; set; }

		public string Alias { get; set; }

		public void SetPostion(Postion2D postion2D)
		{
			Postion2D = postion2D;
		}

		public Postion2D GetPostion()
		{
			return Postion2D;
		}

		public void ResetPostion()
		{
			throw new NotImplementedException();
		}
	}
}
