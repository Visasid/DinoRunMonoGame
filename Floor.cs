using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;

namespace DinoRun
{
	internal class Floor
	{
		private Game1 game;
		private GraphicsDeviceManager _graphics;
		private Random rand = new Random();

		private Texture2D floorTex;
		private Vector2 floorPos;
		private Texture2DAtlas cactusAtlas;
		private List<Obstacle> obstacles;
		private float obstSpawnCD = 3f;
		private float spawnTimer = 1f;
		private List<Vector2> floors;
		private float floorSpawnTime = 3f;
		private float floorTimer = 1f;

		public void InitFloor(Texture2D groundTex, Texture2D cactusTex, GraphicsDeviceManager graphics, Game1 gameManager)
		{
			_graphics = graphics;
			floors = new List<Vector2>();
			obstacles = new List<Obstacle>();
			game = gameManager;
			floorTex = groundTex;
			floorPos = new Vector2(0, _graphics.PreferredBackBufferHeight / 2);
			cactusAtlas = Texture2DAtlas.Create("Atlas/Cactus", cactusTex, 34, 70);
		}

		public void Update(GameTime gameTime)
		{
			float updatedSpeed = game.GetGameSpeed() * (float)gameTime.ElapsedGameTime.TotalSeconds;
			floorPos.X -= updatedSpeed;
			for (int i = 0; i < floors.Count; i++) floors[i] = new Vector2(floors[i].X - updatedSpeed, floors[i].Y);
			for (int i = 0; i < obstacles.Count; i++) obstacles[i].obstPos.X -= updatedSpeed;

			spawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (spawnTimer <= 0)
			{
				Obstacle newObst = new Obstacle();
				newObst.InitObstacle(cactusAtlas[rand.Next(6)], _graphics);
				obstacles.Add(newObst);
				spawnTimer = obstSpawnCD;
			}

			floorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (floorTimer <= 0)
			{
				if (floors.Count == 0) floors.Add(new Vector2(floorPos.X + floorTex.Width, _graphics.PreferredBackBufferHeight / 2));
				else floors.Add(new Vector2(floors[floors.Count - 1].X + floorTex.Width, _graphics.PreferredBackBufferHeight / 2));
				floorTimer = floorSpawnTime;
			}
		}

		public void FloorDraw(SpriteBatch _spriteBatch)
		{
			_spriteBatch.Draw(floorTex, floorPos, null, Color.White, 0.0f, new Vector2(0, 13), new Vector2(1, 1), SpriteEffects.None, 0.0f);
			for (int i = 0; i < floors.Count; i++)
			{
				_spriteBatch.Draw(floorTex, floors[i], null, Color.White, 0.0f, new Vector2(0, 13), new Vector2(1, 1), SpriteEffects.None, 0.0f);
			}
			for (int i = 0; i < obstacles.Count; i++)
			{
				if (obstacles[i].obstPos.X < -120)
				{
					obstacles.Remove(obstacles[i]);
					i++;
				}
				_spriteBatch.Draw(obstacles[i].texture, obstacles[i].obstPos, Color.White, 0.0f, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
			}
		}
	}
}
