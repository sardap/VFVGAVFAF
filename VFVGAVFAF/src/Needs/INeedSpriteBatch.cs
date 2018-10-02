using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Needs
{
	interface INeedSpriteBatch
	{
		[JsonIgnore]
		SpriteBatch SpriteBatch { get; set; }
	}
}
