using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;

namespace VFVGAVFAF.src
{
	interface IPassValue
	{
		string TargetAlais { get; set; }

		string TagMatchPattern { get; set; }

		[JsonIgnore]
		dynamic DValue { get; }
	}

	interface INeedFromCom
	{
		string Alias { get; set; }

		void SetValue(dynamic value);
	}

	class PassValue<T>: IPassValue
	{
		public string TargetAlais { get; set; }

		public string TagMatchPattern { get; set; }

		public virtual T Value { get; set; }

		public dynamic DValue { get { return Value; } }

		public PassValue()
		{
			TagMatchPattern = "";
		}
	}

	class PassDouble: PassValue<double> { }

	class PassBool: PassValue<bool> { }

	class PassValueFromCom<T>: PassValue<T>, INeedFromCom
	{
		public string Alias { get; set; }

		public void SetValue(dynamic value)
		{
			Value = value;
		}
	}

	class PassBoolFromCom: PassValueFromCom<bool>{ }
}
