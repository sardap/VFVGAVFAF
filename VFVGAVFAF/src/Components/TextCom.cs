using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class TextCom: IHaveAlias, IComponent
	{
		public long EntID { get; set; }
		public string Alias { get; set; }
		public string Text { get; set; }
	}
}
