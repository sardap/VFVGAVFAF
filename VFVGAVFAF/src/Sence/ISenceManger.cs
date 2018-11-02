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

		void Step();

		void LoadMain(ISenceData senceData);

		void LoadMainFile(string fileName, List<IPassValue> passedValues);

		void AddProcessedToMain(long id);

		void OverlayFileMain(string filename, List<IPassValue> passedValues);

		void Load(string name, ISenceData senceData);

		void LoadFile(string name, string fileName, List<IPassValue> passedValues);

		void AddProcessedTo(string name, long id);

		void Overlay(string name, string fileName, List<IPassValue> passValues);
	}
}
