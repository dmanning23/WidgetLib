using FilenameBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace WidgetLib
{
	public abstract class BaseTab : WidgetScreen
	{
		#region Properties

		protected StackLayout ToolStack { get; private set; }

		protected IButton CloseButton { get; private set; }

		#endregion //Properties

		#region Methods

		public BaseTab(string tabName) : base(tabName)
		{
			CoveredByOtherScreens = true;
			CoverOtherScreens = true;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			ToolStack = new StackLayout()
			{
				Position = new Point(910, 36),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Alignment = StackAlignment.Top,
			};
		}

		public override void ExitScreen()
		{
			base.ExitScreen();

			var editorScreen = this as IEditorScreen;
			if (null != editorScreen)
			{
				editorScreen.Save();
			}
		}

		protected void AddTitle(string title, bool editableTitle, IStackLayout layout)
		{
			var titleLayout = new StackLayout(StackAlignment.Left)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};

			int sizeDelta = 360;

			//add a button with the quest name
			var button = new RelativeLayout()
			{
				Size = new Vector2(256f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Center,
				HasOutline = false
			};

			if (editableTitle)
			{
				//add a button with the quest name
				var titleTextEdit = new TextEdit(title, Content, FontSize.Small)
				{
					Size = new Vector2(256f, 32f),
					Horizontal = HorizontalAlignment.Left,
					Vertical = VerticalAlignment.Top,
					TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
					TextColor = Color.White,
					HasOutline = true,
				};
				titleTextEdit.OnTextEdited += (obj, e) =>
				{
					OnTitleTextEdited(titleTextEdit.Text);
				};
				button.AddItem(titleTextEdit);
			}
			else
			{
				var titleText = new Label(title, Content, FontSize.Small)
				{
					Horizontal = HorizontalAlignment.Center,
					Vertical = VerticalAlignment.Center,
					TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
					TextColor = Color.White,
					HasOutline = false,
					Highlightable = false
				};
				button.AddItem(titleText);
			}
			titleLayout.AddItem(button);
			sizeDelta -= button.Rect.Width;

			//add a shim
			var shim = new Shim()
			{
				Size = new Vector2(16f, 16f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};
			titleLayout.AddItem(shim);
			sizeDelta -= shim.Rect.Width;

			//add a button to exit this window
			CloseButton = new RelativeLayoutButton()
			{
				Size = new Vector2(sizeDelta, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true,
				Highlightable = true
			};
			CloseButton.AddItem(new Label("X", Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				TextColor = Color.Red
			});
			CloseButton.OnClick += (obj, e) =>
			{
				ExitScreen();
			};
			titleLayout.AddItem(CloseButton);
			sizeDelta -= CloseButton.Rect.Width;

			layout.AddItem(titleLayout);
			AddShim(layout);
		}

		protected ContentFileDropdown AddContentFileDropdown(string labelText, string folder, string extension, Filename initial, IStackLayout layout)
		{
			layout.AddItem(new Label(labelText, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			var boneTypes = new ContentFileDropdown(this, folder, extension, initial)
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			layout.AddItem(boneTypes);
			AddShim(layout);

			return boneTypes;
		}

		protected VectorEdit AddVectorEdit(string labelText, Vector2 initial, IStackLayout layout)
		{
			layout.AddItem(new Label(labelText, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			var boneTypes = new VectorEdit(initial, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};
			layout.AddItem(boneTypes);
			AddShim(layout);

			return boneTypes;
		}

		protected Vector3Edit AddVector3Edit(string labelText, Vector3 initial, IStackLayout layout)
		{
			layout.AddItem(new Label(labelText, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			var vectEdit = new Vector3Edit(initial, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};
			layout.AddItem(vectEdit);
			AddShim(layout);

			return vectEdit;
		}

		protected ColorEdit AddColorEdit(string labelText, Color initial, IStackLayout layout)
		{
			layout.AddItem(new Label(labelText, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			var boneTypes = new ColorEdit(initial, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};
			layout.AddItem(boneTypes);
			AddShim(layout);

			return boneTypes;
		}

		protected virtual void OnTitleTextEdited(string titleText)
		{
		}

		protected ILabel CreateLabel(string labelText, IStackLayout layout)
		{
			var button = new RelativeLayout()
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false
			};
			var label = new Label(labelText, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			};
			button.AddItem(label);
			layout.AddItem(button);
			return label;
		}

		protected ITextEdit CreateEditBox(string text, IStackLayout layout)
		{
			var button = new RelativeLayout()
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false
			};
			var label = new TextEdit(text, Content, FontSize.Small)
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				TextColor = Color.White,
				HasOutline = true,
			};
			button.AddItem(label);
			layout.AddItem(button);
			AddShim(layout);
			return label;
		}

		protected INumEdit CreateNumEditBox(float text, IStackLayout layout)
		{
			var button = new RelativeLayout()
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false
			};
			var label = new NumEdit(text, Content, FontSize.Small)
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				TextColor = Color.White,
				HasOutline = true,
			};
			button.AddItem(label);
			layout.AddItem(button);
			AddShim(layout);
			return label;
		}

		protected Vector3Edit CreateVector3EditBox(Vector3 vect, IStackLayout layout)
		{
			var button = new RelativeLayout()
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false
			};
			var colorEdit = new Vector3Edit(vect, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = false,
			};
			button.AddItem(colorEdit);
			layout.AddItem(button);
			AddShim(layout);
			return colorEdit;
		}

		protected ColorEdit CreateColorEditBox(Color color, IStackLayout layout)
		{
			var button = new RelativeLayout()
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false
			};
			var colorEdit = new ColorEdit(color, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = false,
			};
			button.AddItem(colorEdit);
			layout.AddItem(button);
			AddShim(layout);
			return colorEdit;
		}

		protected Shim AddShim(IStackLayout layout, IScreenItem nextItem = null)
		{
			var shim = new Shim()
			{
				Size = new Vector2(16f, 16f)
			};

			if (null != nextItem)
			{
				layout.InsertItemBefore(shim, nextItem);
			}
			else
			{
				layout.AddItem(shim);
			}
			return shim;
		}

		protected RelativeLayoutButton CreateButton(string labelText, IStackLayout layout)
		{
			var button = new RelativeLayoutButton()
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			button.AddItem(new Label(labelText, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			});

			layout.AddItem(button);
			AddShim(layout);
			return button;
		}

		protected ScrollLayout AddScrollWindow()
		{
			//create the scroller and add the abs layout
			var scroll = new ScrollLayout()
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Size = new Vector2(720, 720),
				Position = new Point(Resolution.ScreenArea.Center.X - 128, Resolution.ScreenArea.Center.Y)
			};

			AddItem(scroll);
			return scroll;
		}

		#endregion //Methods
	}
}
