using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace WidgetLib
{
	public class Vector3Edit : StackLayout
	{
		#region Propereties

		private Vector3 _vect;
		public Vector3 Vect
		{
			get
			{
				return _vect;
			}
			private set
			{
				_vect = value;
			}
		}

		public event EventHandler<Vector3ChangeEventArgs> OnVector3Edited;

		#endregion //Propereties

		#region Methods

		public Vector3Edit(Vector3 vect, ContentManager content, FontSize fontSize = FontSize.Medium)
		{
			this.Alignment = StackAlignment.Left;
			Vect = vect;
			this.HasOutline = false;

			var red = AddEditbox("X", Vect.X, content, fontSize);
			red.OnNumberEdited += (obj, e) =>
			{
				_vect.X = Convert.ToSingle(e.Num);
				if (null != OnVector3Edited)
				{
					OnVector3Edited(this, new Vector3ChangeEventArgs(Vect));
				}
			};
			AddItem(new Shim(12, 0));

			var green = AddEditbox("Y", Vect.Y, content, fontSize);
			green.OnNumberEdited += (obj, e) =>
			{
				_vect.Y = Convert.ToSingle(e.Num);
				if (null != OnVector3Edited)
				{
					OnVector3Edited(this, new Vector3ChangeEventArgs(Vect));
				}
			};
			AddItem(new Shim(12, 0));

			var blue = AddEditbox("Z", Vect.Z, content, fontSize);
			blue.OnNumberEdited += (obj, e) =>
			{
				_vect.Z = Convert.ToSingle(e.Num);
				if (null != OnVector3Edited)
				{
					OnVector3Edited(this, new Vector3ChangeEventArgs(Vect));
				}
			};
			AddItem(new Shim(12, 0));
		}

		private NumEdit AddEditbox(string labelText, float defaultNum, ContentManager content, FontSize fontSize)
		{
			AddItem(new Label(labelText, content, fontSize)
			{
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
			});

			AddItem(new Shim(8f, 0));

			var editBox = new NumEdit(defaultNum, content, fontSize)
			{
				Min = float.MinValue,
				Max = float.MaxValue,
				AllowDecimal = true,
				AllowNegative = true,
				Size = new Vector2(80f, 32f),
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			AddItem(editBox);

			return editBox;
		}

		#endregion //Methods
	}
}
