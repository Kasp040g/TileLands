using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace Isometric_test_1
{
    public class GameWorld : Game
    {
        //Setup essential variables for the game
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Rendering and Texture
        private RenderTarget2D _renderBuffer;   // will draw before it is drawn on screen
        private Rectangle _renderRectangle;     // where rendertarget2d will be drawn 
        //private Texture2D _texture;

        //init Gamemanager
        private GameManager _gameManager;

        public static int ScreenWidth = 640;
        public static int ScreenHeight = 480;

        private List<ScrollingBackground> _scrollingBackgrounds;

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

            //allow window rezizing
            Window.AllowUserResizing = true;

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
            //insta. Render buffer
            _renderBuffer = new RenderTarget2D(GraphicsDevice, 1920, 1080);       // rendertarget is 1920, 1080

            // Delegate method that will run when window size is changed            
            Window.ClientSizeChanged += OnWindowSizeChange;     // add and subscribe to deleagte method, will be called when window is changed
            OnWindowSizeChange(null, null);                     // (no object, no event)

            //Set game window size
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            //Transfer content to be global
            Globals.Content = Content;

            //Instantiate game manager
            _gameManager = new();

            //Call game base initialization
            base.Initialize();
        }

        private void OnWindowSizeChange(object sender, EventArgs e)
        {
            //first we find screen width and hight
            var width = Window.ClientBounds.Width;
            var height = Window.ClientBounds.Height;

            // calculate if we want pillar or letterboxing to fit _renderBuffer on screen
            if(height < width / (float)_renderBuffer.Width * _renderBuffer.Height)          // (skærm bred / render bred) * render høj større end skærm høj hvis den er større kan det ikke passe på skræmen og vi resizer
            {
                width = (int)(height / (float)_renderBuffer.Height * _renderBuffer.Width);  // tegne bred = (skræm høj / render høj) * rende bred  (pillarbox)  ELLER hvis mindre 
            }
            else
            {
                height = (int)(width / (float)_renderBuffer.Width * _renderBuffer.Height); // tegne høj = (skræm bred/ render bred) * render høj (letterbox)
            }

            // calculate new x,y by taking half of screen hight, width and moving screen to fit
            // (new screen size width - render buffer rectangle width) / 2
            var x = (Window.ClientBounds.Width - width) / 2;
            var y = (Window.ClientBounds.Height - height) / 2;
            _renderRectangle = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Loads the contents of the game once
        /// </summary>
        protected override void LoadContent()
        {
            //Creates a new sprite batch for drawing
            _spriteBatch = new SpriteBatch(GraphicsDevice);

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
            // sets GD to draw to render buffer
            GraphicsDevice.SetRenderTarget(_renderBuffer);      
            // clear screen to color
            GraphicsDevice.Clear(Color.Black);

            // tell SB what sorting mode, and clamping
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);

            // foreach for running the parallaxing background 
            foreach(var sb in _scrollingBackgrounds)
                sb.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            // then we draw the gamemanager(the game itself)
            _spriteBatch.Begin();
            _gameManager.Draw();
            _spriteBatch.End();

            // Reset rendertarget to null and tell Draw we want to draw to the screen
            GraphicsDevice.SetRenderTarget(null);                               
            GraphicsDevice.Clear(Color.HotPink);

            _spriteBatch.Begin();
            // we draw all that is put in render buffer to screen
            _spriteBatch.Draw(_renderBuffer, _renderRectangle, Color.White);    
            _spriteBatch.End();

            base.Draw(gameTime);

            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //_spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);

            //foreach(var sb in _scrollingBackgrounds)
            //    sb.Draw(gameTime, _spriteBatch);

            //_spriteBatch.End();

            //_spriteBatch.Begin();
            //_gameManager.Draw();
            //_spriteBatch.End();

            //base.Draw(gameTime);
        }
    }
}