using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Exception
{
	class ComponetMissingException : System.Exception
	{
		public ComponetMissingException(long id) : base("Missing " + id.ToString())
		{
		}
	}
}
