using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using System;

namespace DinoRun
{
    public class Game1 : Game
    {
		private float gameSpeed;
		private Player player;
        private Floor floor;
        
		private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

		public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
			gameSpeed = 100f;
			player = new Player();
            floor = new Floor();
			base.Initialize();
		}

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player.InitializePlayer(Content.Load<Texture2D>("dino"), _graphics);
            floor.InitFloor(Content.Load<Texture2D>("ground"), Content.Load<Texture2D>("cactuses"), _graphics, this);
		}

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			// The time since Update was called last.
			float updatedBallSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Update(gameTime);
            floor.Update(gameTime);
			base.Update(gameTime);
			gameSpeed += 0.01f;
		}

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

			Texture2DRegion currentFrameTexture = player.dinoSheet.TextureAtlas[player._runAnimController.CurrentFrame];

			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			_spriteBatch.Draw(currentFrameTexture, player.playerPos, Color.White, 0.0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0.0f);
            floor.FloorDraw(_spriteBatch);
			_spriteBatch.End();

            base.Draw(gameTime);
        }

        public float GetGameSpeed()
        {
            return gameSpeed;
        }
    }
}
