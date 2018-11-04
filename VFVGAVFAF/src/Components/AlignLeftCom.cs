using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class AlignLeftCom : IGameEventCom, INeedEnityManger
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public double TimeToComplete { get; set; }

		public double Cooldown { get; set; }

		[JsonRequired]
		public string PostionAlais { get; set; }

		[JsonRequired]
		public string ToAlignLeftOf { get; set; }

		public Margin Margin { get; set; }

		public EntityManager EntityManager { get; set; }

		public void Action()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var postionCom = ent.GetComponent<IPostionComponet>(PostionAlais);
			var bounds = ent.GetComponent<IHaveHitBoxCom>(ToAlignLeftOf).HitBox;

			var newPos = postionCom.GetPostion();

			newPos.X = bounds.Right;

			Margin.Apply(newPos);

			postionCom.SetPostion(newPos);
		}
	}
}
