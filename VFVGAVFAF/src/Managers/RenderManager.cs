using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Exception;

namespace VFVGAVFAF.src
{
	class RenderManager : IManger
	{
		private ISet<long> _renderList = new HashSet<long>();

		public ComponentManager ComponentManager { get; set;}

		public void Render(double deltaTime)
		{
			foreach (var renderable in _renderList)
			{
				ComponentManager.GetComponent<IRenderableComponent>(renderable).Render(deltaTime);
			}

		}

		public void Regsiter(long comID)
		{
			if (ComponentManager.GetComponent<IRenderableComponent>(comID) != null)
			{
				_renderList.Add(comID);
				return;
			}

			throw new MissmatchComponetException();
		}

		public void UnRegsiter(long id)
		{
			_renderList.Remove(id);
		}
	}
}
