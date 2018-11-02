using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VFVGAVFAF.src;

namespace VFVGAVFAF
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
		private ECS _ecs;
		private FrameCounter _frameCounter = new FrameCounter();
		private SpriteFont _font;

		public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
			graphics.SynchronizeWithVerticalRetrace = false;
			IsFixedTimeStep = false;

			_ecs = new ECS();
			_ecs.InitiliseSettings();

			graphics.PreferredBackBufferWidth = _ecs.Settings.Width;  // set this value to the desired width of your window
			graphics.PreferredBackBufferHeight = _ecs.Settings.Height;   // set this value to the desired height of your window
			graphics.ApplyChanges();

			IsMouseVisible = true;

			base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

			_ecs.SpriteBatch = spriteBatch;
			_ecs.Content = Content;
			_font = Content.Load<SpriteFont>("Fonts/score");
			_ecs.Initialse(graphics);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			_ecs.Step(gameTime.ElapsedGameTime.TotalSeconds);

			base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

			spriteBatch.Begin(transformMatrix: _ecs.ScaleMatrix);
			_ecs.Render(gameTime.ElapsedGameTime.TotalSeconds);

			var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			_frameCounter.Update(deltaTime);

			var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);

			spriteBatch.DrawString(_font, fps, new Vector2(1, 1), Color.Red);

			spriteBatch.End();

			base.Draw(gameTime);
        }
    }
}
