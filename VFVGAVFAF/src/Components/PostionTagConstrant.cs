using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class PostionTagConstrant: IPostionConstrantCom, INeedEnityManger, IHaveAlias
	{

		public long EntID { get; set; }

		public string Alias { get; set; }

		public string Tag { get; set; }

		public bool Inside { get; set; }

		public CheckType Type { get; set; }

		public EntityManager EntityManager { get; set; }

		public bool Check(Paultangle hitBox)
		{
			return EntityManager.GetAllEntsWithTag(Tag).All
			(
				i =>
					Utils.Check
					(
						hitBox, 
						EntityManager.GetEntiy<GameObject>(i).GetComponent<IHaveHitBoxCom>("pos").HitBox, 
						Type, 
						Inside
					)
			);
		}

	}
}
