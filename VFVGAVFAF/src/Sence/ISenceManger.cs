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

		void Load(ISenceData senceData);

		void LoadFromFile(string fileName);

		void LoadFromFile(string fileName, List<IPassValue> passedValues);

		void Step();

		void AddToProcessed(long id);

		void UnloadCurrent();
	}
}
