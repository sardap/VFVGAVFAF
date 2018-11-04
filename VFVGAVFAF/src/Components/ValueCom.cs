using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class ValueCom<T>: IValueCom, IValueCom<T>
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public virtual T Value { get; set; }

		public dynamic DValue { get { return Value; } set { Value = value; } }
	}
}
