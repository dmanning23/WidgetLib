using Microsoft.Xna.Framework;

namespace WidgetLib
{
	public class VectorChangeEventArgs
	{
		public Vector2 Vector { get; set; }

		public VectorChangeEventArgs(Vector2 vector)
		{
			Vector = vector;
		}
	}
}
