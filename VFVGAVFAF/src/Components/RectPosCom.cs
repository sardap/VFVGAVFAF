﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace VFVGAVFAF.src.Components
{
	class RectPosCom : IPostionComponet, INeedEnityManger, IHaveAlias
	{
		private Paultangle _startingPostion;
		public EntityManager EntityManager { get; set; }

		public Paultangle Paultangle { get; set; }
		public long EntID { get; set; }
		public string Alias { get; set; }
		public List<string> PostionConstrantComs { get; set; }


		[JsonIgnore]
		public Paultangle Rectangle { get { return Paultangle; } }

		public RectPosCom()
		{
			Paultangle = new Paultangle();
			_startingPostion = new Paultangle(Paultangle);
		}

		public RectPosCom(Rectangle rectangle)
		{
			PostionConstrantComs = new List<string>();
			Paultangle = new Paultangle(rectangle);
			_startingPostion = new Paultangle(Paultangle);
		}

		public RectPosCom(Postion2D postion2D) : this(postion2D.ToRectangle(0, 0))
		{
		}

		public void SetPostion(Postion2D postion2D)
		{
			var oldPostion = new Postion2D(Paultangle.Postion2D);
			Paultangle.Postion2D.X = postion2D.X;
			Paultangle.Postion2D.Y = postion2D.Y;

			bool result = true;

			var ent = EntityManager.GetEntiy<GameObject>(EntID);

			foreach(var i in PostionConstrantComs)
			{
				result = result && ent.GetComponent<IPostionConstrantCom>(i).Check(Paultangle);
			}

			if(!result)
			{
				Paultangle.Postion2D.X = oldPostion.X;
				Paultangle.Postion2D.Y = oldPostion.Y;
			}
		}

		public Postion2D GetPostion()
		{
			return new Postion2D(Paultangle.Postion2D);
		}

		public void ResetPostion()
		{
			SetPostion(new Postion2D(100, 100));
		}
	}
}
