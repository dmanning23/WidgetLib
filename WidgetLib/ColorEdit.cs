using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace WidgetLib
{
	public class ColorEdit : StackLayout
	{
		#region Propereties

		private Color _color;
		public Color Color
		{
			get
			{
				return _color;
			}
			private set
			{
				_color = value;
			}
		}

		public event EventHandler<ColorChangeEventArgs> OnColorEdited;

		#endregion //Propereties

		#region Methods

		public ColorEdit(Color color, ContentManager content, FontSize fontSize = FontSize.Medium)
		{
			this.Alignment = StackAlignment.Left;
			Color = color;
			this.HasOutline = false;

			var red = AddEditbox("R", Color.R, content, fontSize);
			red.OnNumberEdited += (obj, e) =>
			{
				_color.R = Convert.ToByte(e.Num);
				if (null != OnColorEdited)
				{
					OnColorEdited(this, new ColorChangeEventArgs(Color));
				}
			};
			AddItem(new Shim(12, 0));

			var green = AddEditbox("G", Color.G, content, fontSize);
			green.OnNumberEdited += (obj, e) =>
			{
				_color.G = Convert.ToByte(e.Num);
				if (null != OnColorEdited)
				{
					OnColorEdited(this, new ColorChangeEventArgs(Color));
				}
			};
			AddItem(new Shim(12, 0));

			var blue = AddEditbox("B", Color.B, content, fontSize);
			blue.OnNumberEdited += (obj, e) =>
			{
				_color.B = Convert.ToByte(e.Num);
				if (null != OnColorEdited)
				{
					OnColorEdited(this, new ColorChangeEventArgs(Color));
				}
			};
			AddItem(new Shim(12, 0));

			var alpha = AddEditbox("A", Color.A, content, fontSize);
			alpha.OnNumberEdited += (obj, e) =>
			{
				_color.A = Convert.ToByte(e.Num);
				if (null != OnColorEdited)
				{
					OnColorEdited(this, new ColorChangeEventArgs(Color));
				}
			};
		}

		private NumEdit AddEditbox(string labelText, byte defaultNum, ContentManager content, FontSize fontSize)
		{
			AddItem(new Label(labelText, content, fontSize)
			{
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
			});

			AddItem(new Shim(8f, 0));

			var editBox = new NumEdit(defaultNum, content, fontSize)
			{
				Min = 0,
				Max = 255,
				AllowDecimal = false,
				AllowNegative = false,
				Size = new Vector2(64f, 32f),
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			AddItem(editBox);

			return editBox;
		}

		#endregion //Methods
	}
}
