using InputHelper;
using MenuBuddy;
using Microsoft.Xna.Framework;

namespace WidgetLib
{
	public class ItemControl<T> : StackLayout
	{
		public T Item { get; set; }
		private ListScreen<T> ListScreen { get; set; }
		public IButton ItemNameButton { get; set; }
		public ILabel ItemNameLabel { get; set; }

		public ItemControl(T item, ListScreen<T> listScreen, bool removable = true) : base(StackAlignment.Top)
		{
			Item = item;
			ListScreen = listScreen;

			Horizontal = HorizontalAlignment.Left;
			Vertical = VerticalAlignment.Top;

			var flockButtons = new StackLayout(StackAlignment.Left)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};

			int sizeDelta = 360;

			//add a button with the flock name
			ItemNameButton = new RelativeLayoutButton()
			{
				Size = new Vector2(removable ? 256f : sizeDelta, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			ItemNameLabel = new Label(item.ToString(), listScreen.Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				TextColor = Color.White
			};
			ItemNameButton.AddItem(ItemNameLabel);
			flockButtons.AddItem(ItemNameButton);
			sizeDelta -= ItemNameButton.Rect.Width;

			if (removable)
			{
				//add a shim
				var shim = new Shim()
				{
					Size = new Vector2(16f, 16f),
					Horizontal = HorizontalAlignment.Left,
					Vertical = VerticalAlignment.Top,
				};
				flockButtons.AddItem(shim);
				sizeDelta -= shim.Rect.Width;

				//add a button to delete the flock
				var removeButton = new RelativeLayoutButton()
				{
					Size = new Vector2(sizeDelta, 32f),
					Horizontal = HorizontalAlignment.Left,
					Vertical = VerticalAlignment.Top,
					TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
					HasOutline = true
				};
				removeButton.AddItem(new Label("X", listScreen.Content, FontSize.Small)
				{
					Horizontal = HorizontalAlignment.Center,
					Vertical = VerticalAlignment.Center,
					TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight)
				});
				flockButtons.AddItem(removeButton);
				sizeDelta -= removeButton.Rect.Width;

				//delete this flock and control when this button clicked
				removeButton.OnClick += (obj, e) =>
				{
					OnDelete(obj, e);
				};
			}

			AddItem(flockButtons);
			AddItem(new Shim()
			{
				Size = new Vector2(16f, 16f)
			});
		}

		public void OnDelete(object obj, ClickEventArgs e)
		{
			var msgBox = new MessageBoxScreen("Are you sure you want to delete the item?");
			msgBox.OnSelect += (obj2, e2) =>
			{
				ListScreen.RemoveItem(Item);
			};

			ListScreen.ScreenManager.AddScreen(msgBox);
		}
	}
}
