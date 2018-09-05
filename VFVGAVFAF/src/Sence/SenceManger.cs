using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Sence
{
	class SenceManger : ISenceManger
	{
		private ISenceData _currentSence;

		public void LoadSence(ISenceData senceData)
		{
			if(_currentSence != null)
			{
				UnloadSence(_currentSence);
			}


		}

		private void UnloadSence(ISenceData senceData)
		{

		}
	}
}
