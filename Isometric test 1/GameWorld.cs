using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Isometric_test_1
{
    public class GameWorld : Game
    {
        //Setup essential variables for the game
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager _gameManager;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        private List<ScrollingBackground> _scrollingBackgrounds;

        private Player _player;
        // Game State 
        public enum GameState { Idle, Start, Play, CheckEnd }
        private GameState _gameState;

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

            // Init first GameState
            _gameState = GameState.Idle;
        }


        /// <summary>
        /// Game initialization, runs once 
        /// </summary>
        protected override void Initialize()
        {
            //Set game window size
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

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

            var boyTexture = Content.Load<Texture2D>("boy");

            _player = new Player(boyTexture)
            {
                Position = new Vector2(500, (ScreenHeight - boyTexture.Height) - 20),
                Layer = 1f,
            };

            _scrollingBackgrounds = new List<ScrollingBackground>()
      {
        new ScrollingBackground(Content.Load<Texture2D>("BackGrounds/Clouds_Fast"), _player, 10f, true)
        {
          Layer = 0.99f,
        },
       
        new ScrollingBackground(Content.Load<Texture2D>("BackGrounds/Clouds_Slow"), _player, 25f, true)
        {
          Layer = 0.8f,
        } };
        

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

            // Game State Switch
            switch(_gameState)
            {
                case GameState.Idle:
                    break;
                case GameState.Start:
                    break;
                case GameState.Play:
                    break;
                case GameState.CheckEnd:
                    break;
                default:
                    break;
            }

            _player.Update(gameTime);

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

            _player.Draw(gameTime, _spriteBatch);

            foreach (var sb in _scrollingBackgrounds)
                sb.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}