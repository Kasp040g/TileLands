using System;
using System.Collections.Generic;

namespace TileLands
{
    public class GameWorld : Game
    {
        // Init. essential variables for the game
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager _gameManager;

        public static int ScreenWidth = 1600;
        public static int ScreenHeight = 900;

        //public static int ScreenWidth = 1280;
        //public static int ScreenHeight = 720;

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
            Globals.Bounds = new(1600, 900);
            _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
            _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
            _graphics.ApplyChanges();

            //Transfer content to be global
            Globals.Content = Content;

            //Load audio files
            Assets.Audio.LoadAudio();
            // Load Sprites on start
            Assets.Sprites.LoadSprites();

            //Instantiate game manager and run GameManager's Initialize
            _gameManager = new();
            _gameManager.Init();

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
            // TODO : Quick game exit using escape (SAVE FOR LATER Method) <<<<<
            //if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            if(Globals._quit)
                Exit();

            //Quick Menu acces using escape
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _gameManager.ChangeState(ScreenStates.Menu);

            //Calls for globals to update to keep track of time elapsed
            Globals.Update(gameTime);

            //Calls for game manager to update
            _gameManager.Update(gameTime);

            //Calls game update
            base.Update(gameTime);
        }


        /// <summary>
        /// Handling of game drawing functionality, run once every frame
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);

            _spriteBatch.End();

            _spriteBatch.Begin();
            _gameManager.Draw(gameTime);
            _spriteBatch.Draw(Assets.Sprites.Vignette, Vector2.Zero, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }       
    }
}