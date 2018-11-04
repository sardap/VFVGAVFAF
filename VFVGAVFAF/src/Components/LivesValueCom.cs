using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class LivesValueCom: ValueCom<int>, INeedMinigameManger
	{
		public override int Value
		{
			get
			{
				if(MinigameManger != null)
					return MinigameManger.FailuresAllowed - MinigameManger.Failures;

				return 80085;
			}

			set
			{
			}
		}

		public MinigameManger MinigameManger { get; set; }
	}
}
