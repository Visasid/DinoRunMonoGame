using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace DinoRun
{
	internal class Obstacle
	{
		public Vector2 obstPos;
		public Texture2DRegion texture;

		public void InitObstacle(Texture2DRegion sprite, GraphicsDeviceManager _graphics)
		{
			obstPos = new Vector2 (_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight / 2 - 60);
			texture = sprite;
		}
	}
}
