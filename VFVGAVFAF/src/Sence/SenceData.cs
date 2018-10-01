using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Sence
{
	class SenceData : ISenceData
	{
		private List<GameObjectFactory.GameObjects> _toCreate = new List<GameObjectFactory.GameObjects>();
		private List<long> _entites = new List<long>();

		public IList<GameObjectFactory.GameObjects> ToCreate { get { return _toCreate; } }
		public IList<long> CreatedEntites { get { return _entites; } }

		public async Task SaveFile(string fileName)
		{
			using (StreamWriter streamWriter = new StreamWriter(fileName))
			{
				await streamWriter.WriteAsync(JsonConvert.SerializeObject(this));
			}
		}
	}
}
