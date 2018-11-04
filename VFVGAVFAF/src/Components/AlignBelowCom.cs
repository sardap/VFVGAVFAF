using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class AlignBelowCom : ITextAlign, INeedEnityManger
	{
		public long EntID { get; set; }

		[JsonRequired]
		public string Alias { get; set; }

		[JsonRequired]
		public string BelowHitboxAlias { get; set; }

		[JsonRequired]
		public string PostionAlais { get; set; }

		Margin Margin { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Align()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var postionCom = ent.GetComponent<IPostionComponet>(PostionAlais);
			var bellowPosCom = ent.GetComponent<IHaveHitBoxCom>(BelowHitboxAlias).HitBox;

			var newPos = postionCom.GetPostion();

			newPos.Y = bellowPosCom.Bottom;

			newPos = Margin.Apply(newPos);

			postionCom.SetPostion(newPos);
		}
	}
}
