using Microsoft.Xna.Framework;

namespace WidgetLib
{
	public class Vector3ChangeEventArgs
	{
		public Vector3 Vector { get; set; }

		public Vector3ChangeEventArgs(Vector3 vector)
		{
			Vector = vector;
		}
	}
}
