using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFVGAVFAF.src.Components
{
	class ValueCom<T>: IValueCom, IValueCom<T>
	{
		public long EntID { get; set; }

		public string Alias { get; set; }

		public virtual T Value { get; set; }

		public dynamic DValue { get { return Value; } set { Value = value; } }
	}

	class DoubleValueCom : ValueCom<double> { }

	class ColorValueCom : ValueCom<Color> { }

	class TextCom : ValueCom<string>, INeedPostMaster, INeedEnityManger
	{
		public List<string> TextAlignAlias { get; set; }

		public IGameEvenetPostMaster GameEvenetPostMaster { get; set; }

		public EntityManager EntityManager { get; set; }

		public TextCom()
		{
			TextAlignAlias = new List<string>();
		}


		public override string Value
		{
			get
			{
				return base.Value;
			}

			set
			{
				base.Value = value;

				if(EntityManager != null)
				{
					var ent = EntityManager.GetEntiy<GameObject>(EntID);

					TextAlignAlias.ForEach(i => ent.GetComponent<ITextAlign>(i).Align());
				}
			}
		}
	}
}
