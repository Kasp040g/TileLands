using System.Collections.Generic;

namespace Isometric_test_1
{
    public class GameWorld : Game
    {
        // Init. essential variables for the game
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager _gameManager;
        private List<ScrollingBackground> _scrollingBackgrounds;

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

            // TODO : merge into assets
            Sprites.Load(Content);

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

            //Transfer content to be global
            Globals.Content = Content;



            //Loads the List of Scrolling backgrounds, and gives them their speed values and layer value
            _scrollingBackgrounds = new List<ScrollingBackground>()
            {
                new ScrollingBackground(Content.Load<Texture2D>("BackGrounds/Clouds_Fast"), 18f, true)
                {
                  Layer = 0.99f,
                },

                new ScrollingBackground(Content.Load<Texture2D>("BackGrounds/Clouds_Slow"), 25f, true)
                {
                  Layer = 0.8f,
                }
            };

            ////Load audio files
            //Assets.Audio.LoadAudio();
            
            ////Plays and repeats the background music
            //MediaPlayer.Play(Audio.BackgroundMusic);
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Volume = 1f;
            

            

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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            //Quick Menu acces using escape
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _gameManager.ChangeState(ScreenStates.Menu);

            //Calls for globals to update to keep track of time elapsed
            Globals.Update(gameTime);

            //Calls for game manager to update
            _gameManager.Update(gameTime);

            //loops the backgrounds
            foreach (var sb in _scrollingBackgrounds)
                sb.Update(gameTime);

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

            
            foreach (var sb in _scrollingBackgrounds)
                sb.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            _spriteBatch.Begin();
            _gameManager.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}