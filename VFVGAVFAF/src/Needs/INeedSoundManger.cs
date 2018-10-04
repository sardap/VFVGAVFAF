using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src
{
	interface INeedSoundManger
	{
		[JsonIgnore]
		SoundManager SoundManager { get; set; }
	}
}
