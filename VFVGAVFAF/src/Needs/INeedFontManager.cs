using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;

namespace VFVGAVFAF.src
{
	interface INeedFontManager
	{
		[JsonIgnore]
		FontManger FontManger { get; set; }
	}
}
