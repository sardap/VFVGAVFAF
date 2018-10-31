using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class DoubleValueCom : IValueCom<double>
	{
		public long EntID { get; set;}

		public double Value { get; set; }

		public string Alias { get; set; }
	}
}
