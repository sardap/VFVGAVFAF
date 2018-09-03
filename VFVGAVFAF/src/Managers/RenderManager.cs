using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Exception;

namespace VFVGAVFAF.src
{
	class RenderManager
	{
		private List<long> _renderList = new List<long>();

		public ComponentManager ComponentManager { get; set;}

		public void Render(double deltaTime)
		{
			_renderList.ForEach(i => ComponentManager.GetComponent<IRenderableComponent>(i).Render(deltaTime));
		}

		public void RegsiterRendable<T>(long comID) where T : IRenderableComponent
		{
			if(ComponentManager.GetComponent<IRenderableComponent>(comID) != null)
			{
				_renderList.Add(comID);
				return;
			}

			throw new MissmatchComponetException();
		}
	}
}
