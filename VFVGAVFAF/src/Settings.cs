using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src
{
	class Settings
	{
		public float VFXLevel { get; set; }

		public float MusicLevel { get; set; }

		public float Pitch { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public Settings()
		{
			VFXLevel = 1f;
			MusicLevel = 1f;
			Pitch = 0f;
			Width = 800;
			Height = 600;
		}
	}
}
