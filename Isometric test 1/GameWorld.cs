using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Isometric_test_1
{
    public class GameWorld : Game
    {
        //Setup essential variables for the game
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager _gameManager;


        /// <summary>
        /// Game world constructer, creates and sets up the game world
        /// </summary>
        public GameWorld()
        {
            //Update graphics device manager
            _graphics = new GraphicsDeviceManager(this);

            //Update content root directory
            Content.RootDirectory = "Content";

            //Makes sure mouse is visible
            IsMouseVisible = true;
        }


        /// <summary>
        /// Game initialization, runs once 
        /// </summary>
        protected override void Initialize()
        {
            //Set game window size
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;

            //Transfer content to be global
            Globals.Content = Content;

            //Instantiate game manager
            _gameManager = new();

            //Call game base initialization
            base.Initialize();
        }


        /// <summary>
        /// Loads the contents of the game once
        /// </summary>
        protected override void LoadContent()
        {
            //Creates a new sprite batch for drawing
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Transfer sprite batch to be global
            Globals.SpriteBatch = _spriteBatch;

            //Create test font
            Globals.FontTest = Globals.Content.Load<SpriteFont>("FontTest");
        }


        /// <summary>
        /// Updates the game, runs once every frame
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            //Quick game exit using escape (DELETE LATER) <<<<<
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Calls for globals to update to keep track of time elapsed
            Globals.Update(gameTime);

            //Calls for game manager to update
            _gameManager.Update();

            //Calls game update
            base.Update(gameTime);
        }


        /// <summary>
        /// Handling of game drawing functionality, run once every frame
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            //Clears the screen and draws it black
            GraphicsDevice.Clear(Color.Black);

            //Call for game managers draw method
            _spriteBatch.Begin();
            _gameManager.Draw();
            _spriteBatch.End();
           
            //Inherited draw from game
            base.Draw(gameTime);
        }
    }
}