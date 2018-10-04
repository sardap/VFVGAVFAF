using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	interface INeedTextureManager
	{
		[JsonIgnore]
		TextureManager TextureManager { get; set; }
	}
}
