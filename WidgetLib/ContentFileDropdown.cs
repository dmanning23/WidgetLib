using FilenameBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WidgetLib
{
	/// <summary>
	/// This is a combobox that lists all the bones in the model.
	/// </summary>
	public class ContentFileDropdown : Dropdown<Filename>
	{
		public ContentFileDropdown(IScreen screen, string folder, string fileExtension, Filename initialSelectedFile) : base(screen)
		{
			OnClick += CreateDropdownList;

			//get a list of all the files of the correct type
			var files = Filename.ContentFiles(folder, fileExtension);

			CreateDropdownItem(null, screen.Content);

			foreach (var file in files)
			{
				CreateDropdownItem(file, screen.Content);

				//check if this is the initial selected file
				if ((null != initialSelectedFile) && (initialSelectedFile.File == file.File))
				{
					SelectedItem = file;
				}
			}
		}

		private void CreateDropdownItem(Filename file, ContentManager content)
		{
			//add this dude
			var dropitem = new DropdownItem<Filename>(file, this)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350f, 48f),
				Clickable = false
			};

			if (null != file)
			{
				var label = new Label(file.GetRelFilename(), content, FontSize.Small)
				{
					Vertical = VerticalAlignment.Center,
					Horizontal = HorizontalAlignment.Center
				};
				dropitem.AddItem(label);
			}

			AddDropdownItem(dropitem);
		}
	}
}
