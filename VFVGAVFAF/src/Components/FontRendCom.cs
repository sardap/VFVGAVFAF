﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFVGAVFAF.src.Components;
using VFVGAVFAF.src.Managers;

namespace VFVGAVFAF.src.Components
{
	[Obsolete("Use font rend value com", false)]
	class FontRendCom : IRenderableComponent, INeedSpriteBatch, INeedEnityManger, INeedFontManager, IGetSizeCom, IHaveAlias, IHaveHitBoxCom
	{
		public long EntID { get; set; }

		private FontManger _fontManger;
		private SpriteFont _font;

		public string Alias { get; set; }

		public string TextAlais { get; set; }

		public string PostionAlais { get; set; }

		public string FontName { get; set; }

		public Color Color { get; set; }

		public float Scale { get; set; } 

		public FontManger FontManger
		{
			get
			{
				return _fontManger;
			}
			set
			{
				_fontManger = value;
				_font = _fontManger.GetFont(FontName);
			}
		}

		public SpriteBatch SpriteBatch { get; set; }

		public EntityManager EntityManager { get; set; }

		public Paultangle HitBox
		{
			get
			{
				var size = Size();
				var postion = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IPostionComponet>(PostionAlais).GetPostion();
				return new Paultangle(postion.X, postion.Y, size.X, size.Y);
			}
		}

		public Vector2 Size()
		{
			var text = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IValueCom<string>>(TextAlais).Value;
			return _font.MeasureString(text) * Scale;
		}


		public void Render(double deltaTime)
		{
			var postion = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IPostionComponet>(PostionAlais).GetPostion();
			var text = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IValueCom<string>>(TextAlais).Value;
			SpriteBatch.DrawString(_font, text, postion.ToVector(), Color, 0, new Vector2(), Scale, SpriteEffects.None, 0);
		}

	}
}
