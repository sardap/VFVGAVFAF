using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	class KeyboardInputValueDepdentCom<T> : IContolerCom, INeedEnityManger, INeedPostMaster, INeedKeyboardInputManger
	{
		public long EntID { get; set; }

		[JsonRequired]
		public string ValueAlias { get; set; }

		[JsonRequired]
		public Dictionary<T, IList<KeyboardAction>> Actions { get; set; }

		public EntityManager EntityManager { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public KeyboardInputManger KeyboardInputManger { get; set; }

		public void Update(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var valueCom = ent.GetComponent<IValueCom<T>>(ValueAlias);

			foreach (var entry in Actions[valueCom.Value])
			{
				if (entry.Resolve(KeyboardInputManger))
				{
					entry.GameEvents.ForEach(i => GameEvenetPostMaster.Add(ent.GetIdForAlais(i)));
				}
			}
		}
	}

	class KeyboardInputTextDepdentCom : KeyboardInputValueDepdentCom<string> { }
}