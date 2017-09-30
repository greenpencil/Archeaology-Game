using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Archeaology_Game.Menu;
using Archeaology_Game.Discover;
using Archeaology_Game.Dig;

namespace Archeaology_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ArcheaologyGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public enum GameState
        {
            MenuState,
            DigState,
            DiscoverState,
            WriteState,
        }

        public GameState _state;
        GameMenu menu;
        GameDiscovery gameDiscovery;
        GameDig gameDig;
        public Player player;


        public int Width;
        public int Height;
        

        public ArcheaologyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Height = this.Window.ClientBounds.Height;
            Width = this.Window.ClientBounds.Width;

            _state = GameState.MenuState;
            menu = new GameMenu(this);
            gameDiscovery = new GameDiscovery(this);
            gameDig = new GameDig(this);


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;

            menu.Initialize();
            gameDig.Initialize();
            gameDiscovery.Initialize();
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

            // TODO: use this.Content to load your game content her
           
            menu.LoadContent(this.Content);
            gameDig.LoadContent(this.Content);
            gameDiscovery.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            menu.UnloadContent();
            gameDig.UnloadContent();
            gameDiscovery.UnloadContent();
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

            // TODO: Add your update logic here

            if (_state == GameState.MenuState)
            {
                menu.Update(gameTime);
            }
            else if (_state == GameState.DigState)
            {
                gameDig.Update(gameTime);
            }
            else if (_state == GameState.DiscoverState)
            {
                gameDiscovery.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            if (_state == GameState.MenuState)
            {
                menu.Draw(spriteBatch);
            }
            else if (_state == GameState.DigState)
            {
                gameDig.Draw(spriteBatch);
            }
            else if (_state == GameState.DiscoverState)
            {
                gameDiscovery.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
}
