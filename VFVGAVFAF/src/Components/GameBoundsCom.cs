using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class GameBoundsCom : IHaveAlias, IHaveHitBoxCom, IComponent, INeedGameInfo
	{
		public long EntID { get; set; }


		public string Alias { get; set; }

		public GameInfo GameInfo { get; set; }

		public Paultangle HitBox
		{
			get
			{
				return GameInfo.GetBounds();
			}
		}

	}
}
