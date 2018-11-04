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

		public List<string> TextAlignAlias { get; set; }


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

		public FontRendValueCom()
		{
			TextAlignAlias = new List<string>();
		}

		public Vector2 Size()
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			var text = GetValueAsString(ent);
			return _font.MeasureString(text) * Scale;
		}

		public void Render(double deltaTime)
		{
			var ent = EntityManager.GetEntiy<GameObject>(EntID);
			TextAlignAlias.ForEach(i => ent.GetComponent<ITextAlign>(i).Align());

			string text = GetValueAsString(ent);

			var postion = ent.GetComponent<IPostionComponet>(PostionAlais).GetPostion();

			var color = ent.GetComponent<IValueCom<Color>>(ColorAlias).Value;
			SpriteBatch.DrawString(_font, text, postion.ToVector(), color, 0, new Vector2(), Scale, SpriteEffects.None, 0);
		}

		private string GetValueAsString(GameObject ent)
		{
			string text;
			var valueCom = ent.GetComponent<IValueCom<T>>(ValueAlais);

			if (valueCom is IHaveFormatString)
			{
				text = string.Format(((IHaveFormatString)valueCom).FormatString, valueCom.Value);
			}
			else
			{
				text = valueCom.Value.ToString();
			}

			return text;

		}
	}

	class FontRendDoubleCom : FontRendValueCom<double> { }
}
