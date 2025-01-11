using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Collisions.Layers;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tweening;
using System;

namespace DinoRun
{
	internal class Player
	{
		public Vector2 playerPos { get; set; }

		private readonly Tweener _tweener = new Tweener();

		public SpriteSheet dinoSheet;
		public AnimationController _runAnimController;
		private bool isJumping = false;
		private float jumpTimer = 0;

		public void InitializePlayer(Texture2D dinoTex, GraphicsDeviceManager _graphics)
		{
			playerPos = new Vector2(_graphics.PreferredBackBufferWidth / 4, _graphics.PreferredBackBufferHeight / 2 - 40);

			Texture2D dinoTexture = dinoTex;
			Texture2DAtlas atlas = Texture2DAtlas.Create("Atlas/dino", dinoTexture, 59, 48);
			dinoSheet = new SpriteSheet("SpriteSheet/dino", atlas);

			// Run animation
			dinoSheet.DefineAnimation("run", builder =>
			{
				builder.IsLooping(true)
						.AddFrame(regionIndex: 1, duration: TimeSpan.FromSeconds(0.1))
						.AddFrame(2, TimeSpan.FromSeconds(0.1));
			});
			SpriteSheetAnimation runAnim = dinoSheet.GetAnimation("run");
			_runAnimController = new AnimationController(runAnim);

			_tweener.TweenTo(target: this, expression: player => playerPos, toValue: new Vector2(_graphics.PreferredBackBufferWidth / 4, 100), duration: 0.7f, delay: 0)
			.RepeatForever(repeatDelay: 0f)
			.AutoReverse()
				.Easing(EasingFunctions.Linear);
		}

		public void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isJumping)
			{
				// Jumping
				isJumping = true;
				jumpTimer = 1.4f;
			}
			if (isJumping)
			{
				_runAnimController.Pause();
				_tweener.Update(gameTime.GetElapsedSeconds());
				jumpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (jumpTimer <= 0) isJumping = false;
			}
			else _runAnimController.Unpause();

			_runAnimController.Update(gameTime);
		}
	}
}
