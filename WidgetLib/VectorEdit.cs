using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace WidgetLib
{
	public class VectorEdit : StackLayout
	{
		#region Propereties

		private Vector2 _vector;
		public Vector2 Vector
		{
			get
			{
				return _vector;
			}
			private set
			{
				_vector = value;
			}
		}

		public event EventHandler<VectorChangeEventArgs> OnVectorEdited;

		protected INumEdit XEdit { get; set; }

		protected INumEdit YEdit { get; set; }

		#endregion //Propereties

		#region Methods

		public VectorEdit(Vector2 vector, ContentManager content, FontSize fontSize = FontSize.Medium)
		{
			this.Alignment = StackAlignment.Left;
			Vector = vector;
			this.HasOutline = false;

			AddItem(new Label("X:", content, fontSize)
			{
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
			});

			AddItem(new Shim(12, 0));

			XEdit = new NumEdit(Vector.X, content, fontSize)
			{
				Size = new Vector2(120f, 32f),
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			AddItem(XEdit);

			AddItem(new Shim(16, 0));

			AddItem(new Label("Y:", content, fontSize)
			{
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
			});

			AddItem(new Shim(12, 0));

			YEdit = new NumEdit(Vector.Y, content, fontSize)
			{
				Size = new Vector2(120f, 32f),
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			AddItem(YEdit);

			XEdit.OnNumberEdited += (obj, e) =>
			{
				_vector.X = e.Num;
				if (null != OnVectorEdited)
				{
					OnVectorEdited(this, new VectorChangeEventArgs(Vector));
				}
			};

			YEdit.OnNumberEdited += (obj, e) =>
			{
				_vector.Y = e.Num;
				if (null != OnVectorEdited)
				{
					OnVectorEdited(this, new VectorChangeEventArgs(Vector));
				}
			};
		}

		#endregion //Methods
	}
}
