using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	interface IPassValue
	{
		string TargetAlais { get; set; }

		string Tag { get; set; }

		[JsonIgnore]
		dynamic DValue { get; }

	}

	class PassValue<T>: IPassValue
	{
		public string TargetAlais { get; set; }

		public string Tag { get; set; }

		public T Value { get; set; }

		public dynamic DValue { get { return Value; } }
	}

	class PassDouble: PassValue<double> { }
}
