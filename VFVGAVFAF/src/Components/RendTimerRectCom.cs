using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class RendTimerRectCom : IRenderableComponent, INeedEnityManger, INeedSpriteBatch
	{
		public long EntID { get; set; }

		[JsonRequired]
		public string RectPosAlais { get; set; }

		[JsonRequired]
		public string OutlineColorAlias { get; set; }

		[JsonRequired]
		public string BarColorAlias { get; set; }

		[JsonRequired]
		public string TimerValueAlias { get; set; }

		public double Thickness { get; set; }

		public EntityManager EntityManager { get; set; }

		public SpriteBatch SpriteBatch { get; set; }

		public RendTimerRectCom()
		{
			Thickness = 1;
		}

		public void Render(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var outlineColorCom = ent.GetComponent<ColorValueCom>(OutlineColorAlias);
			var barColorCom = ent.GetComponent<ColorValueCom>(BarColorAlias);
			var rectPosCom = ent.GetComponent<IHaveHitBoxCom>(RectPosAlais);
			var timerCom = ent.GetComponent<TimerCom>(TimerValueAlias);

			var timeRectangle = new Paultangle(rectPosCom.HitBox);
			timeRectangle.Width = (timerCom.Value / timerCom.EndTime) * timeRectangle.Width;

			ShapeExtensions.FillRectangle(SpriteBatch, timeRectangle.ToRectangleF(), barColorCom.Value);

			ShapeExtensions.DrawRectangle(SpriteBatch, rectPosCom.HitBox.ToRectangleF(), outlineColorCom.Value, (float)Thickness);
		}
	}
}
