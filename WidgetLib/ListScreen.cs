using InputHelper;
using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WidgetLib
{
	public abstract class ListScreen<T> : BaseTab
	{
		#region Properties

		protected List<T> Items { get; set; }

		public IButton AddItemButton { get; private set; }

		protected bool Closable { get; set; }

		private bool Removable { get; set; }
		private bool Addable { get; set; }

		private IStackLayout ScrollingStack { get; set; }
		private ScrollLayout Scroller { get; set; }

		#endregion //Properties

		#region Methods

		public ListScreen(string tabName, bool removable = true, bool addable = true) : base(tabName)
		{
			Removable = removable;
			Addable = addable;
			Closable = true;
		}

		public ListScreen(List<T> items, string tabName, bool removable = true, bool addable = true) : this(tabName, removable, addable)
		{
			Items = items;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			//Add the title of this page
			if (Closable)
			{
				AddTitle(ScreenName, false, ToolStack);
			}
			else
			{
				CreateLabel(ScreenName, ToolStack);
				AddShim(ToolStack);
			}

			//Create the scroll layout...
			Scroller = new ScrollLayout()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2(360f, Resolution.ScreenArea.Height - ToolStack.Rect.Bottom),
				Position =new Point(ToolStack.Rect.Left, ToolStack.Rect.Bottom)
			};

			//Create the scrolling stack and add to the scroller
			ScrollingStack = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};

			//add a button for each item in the list
			foreach (var item in Items)
			{
				CreateItemControl(item, false);
			}

			//add the "Add Quest" button
			if (Addable)
			{
				AddItemButton = CreateButton("Add Item", ScrollingStack);
				AddItemButton.OnClick += AddItem;
			}

			Scroller.AddItem(ScrollingStack);

			//add the scroller
			AddItem(Scroller);

			AddItem(ToolStack);
		}

		protected virtual ItemControl<T> CreateItemControl(T item, bool loadContent)
		{
			var button = new ItemControl<T>(item, this, Removable)
			{
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
			};

			if (null != AddItemButton)
			{
				ScrollingStack.InsertItemBefore(button, AddItemButton);
			}
			else
			{
				ScrollingStack.AddItem(button);
			}

			Scroller.UpdateMinMaxScroll();
			Scroller.UpdateScrollBars();

			//listen for state changes to update the control text
			Transition.OnStateChange += (obj1, e1) =>
			{
				if (button.ItemNameLabel.Text != item.ToString())
				{
					button.ItemNameLabel.Text = item.ToString();
				}
			};

			//navigate to the item's individual screen when it is clicked
			button.ItemNameButton.OnClick += (obj, e) =>
			{
				NavigateToItemScreen(item);
			};

			if (loadContent)
			{
				button.LoadContent(this);
			}

			return button;
		}

		public abstract void NavigateToItemScreen(T item);

		public abstract void AddItem(object obj, ClickEventArgs e);

		public virtual void RemoveItem(T item)
		{
			//remove from the item collection
			Items.Remove(item);

			//remove the widget
			foreach (var toolStackItem in ScrollingStack.Items)
			{
				var itemControl = toolStackItem as ItemControl<T>;
				if (itemControl != null && itemControl.Item.ToString() == item.ToString())
				{
					ScrollingStack.RemoveItem(toolStackItem);
					break;
				}
			}
		}

		#endregion //Methods
	}
}
