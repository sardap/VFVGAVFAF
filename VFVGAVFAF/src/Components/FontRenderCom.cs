using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class FontRendValueCom<T>: IRenderableComponent, INeedSpriteBatch, INeedEnityManger, INeedFontManager, IGetSizeCom, IHaveAlias, IHaveHitBoxCom
	{
		public long EntID { get; set; }

		private FontManger _fontManger;
		private SpriteFont _font;

		public string Alias { get; set; }

		[JsonRequired]
		public string ValueAlais { get; set; }

		[JsonRequired]
		public string PostionAlais { get; set; }

		[JsonRequired]
		public string FontName { get; set; }

		[JsonRequired]
		public string ColorAlias { get; set; }

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
			var text = EntityManager.GetEntiy<GameObject>(EntID).GetComponent<IValueCom<T>>(ValueAlais).Value.ToString();
			return _font.MeasureString(text) * Scale;
		}


		public void Render(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var postion = ent.GetComponent<IPostionComponet>(PostionAlais).GetPostion();
			var text = ent.GetComponent<IValueCom<T>>(ValueAlais).Value.ToString();
			var color = ent.GetComponent<IValueCom<Color>>(ColorAlias).Value;
			SpriteBatch.DrawString(_font, text, postion.ToVector(), color, 0, new Vector2(), Scale, SpriteEffects.None, 0);
		}

		Vector2 IGetSizeCom.Size()
		{
			throw new NotImplementedException();
		}
	}

	class FontRendDoubleCom : FontRendValueCom<double> { }
}
