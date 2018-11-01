using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Sence
{
	interface ISenceManger
	{
		GameObjectFactory GameObjectFactory { get; set; }

		void LoadMain(ISenceData senceData);

		void LoadMainFile(string fileName, List<IPassValue> passedValues);

		void Step();

		void AddProcessedToMain(long id);

		void OverlayFileMain(string filename, List<IPassValue> passedValues);
	}
}
