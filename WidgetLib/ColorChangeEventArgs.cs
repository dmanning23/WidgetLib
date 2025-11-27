using Microsoft.Xna.Framework;

namespace WidgetLib
{
	public class ColorChangeEventArgs
	{
		public Color Color { get; set; }

		public ColorChangeEventArgs(Color color)
		{
			Color = color;
		}
	}
}
