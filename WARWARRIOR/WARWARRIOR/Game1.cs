using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WARWARRIOR
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static ContentManager contentRef;

        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static GAMESTATE gameState;
        double lastEnemy;

        public static double totalSeconds;
        public static double totalMillis;

        Texture2D startTexture;
        Texture2D backgroundTexture;

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
            gameState = GAMESTATE.START_SCREEN;
            contentRef = Content;
            new Player();

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

            startTexture = Content.Load<Texture2D>(@"Textures/StartTexture");
            backgroundTexture = Content.Load<Texture2D>(@"Textures/Background");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            totalSeconds = gameTime.TotalGameTime.TotalSeconds;
            totalMillis = gameTime.TotalGameTime.TotalMilliseconds;

            InputManager.HandleInput(gameState);

            if (gameState == GAMESTATE.PLAYING && (lastEnemy < gameTime.TotalGameTime.TotalSeconds))
            {
                new Enemy();
                lastEnemy = gameTime.TotalGameTime.TotalSeconds + 10.0f;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (gameState == GAMESTATE.PLAYING)
            {
                spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
                for (int i = 0; i < Actor.actors.Count(); i++)
                {
                    Actor.actors[i].Draw(spriteBatch);
                }
            }

            else
            {
                spriteBatch.Draw(startTexture, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void ResetGame()
        {
            gameState = GAMESTATE.START_SCREEN;
            Actor.actors.Clear();
            new Player();
        }
    }
}
