using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace TileLands
{
    /// <summary>
    /// Main class for controlling the game    
    /// </summary>
    public class GameManager
    {
        #region Fields
        // Init States
        private State _state;

        // Instantiate Managers
        private DebugManager _debugManager = new();

        // Save/Load
        private DataManager _dataManager = new();
        private ScoreManager _sm;
        public static string _savePath = "thisisnottopsecret.data";
        public bool _saveFileCreated;

        //Background
        public static List<ScrollingBackground> _scrollingBackgrounds;
        #endregion Fields

        #region Constructor
        /// <summary>
        /// Initialize Statemanager
        /// and set starting state
        /// </summary>
        public GameManager()
        {
            // Init
            StateManager.Init(this);

            // state
            ChangeState(ScreenStates.Splash);

            if(File.Exists(_savePath))
            {
                _saveFileCreated = true;
            }
        }
        #endregion Constructor

        #region Methods
        /// <summary>
        /// Init is calles in Gameworld
        /// </summary>
        public void Init()
        {
            // sound effect not muted
            Globals._soundEffectsMuted = false;

            _sm = new()
            {
                LevelXDone = Globals.LevelXDone,
                EndlessUnlocked = Globals.Unlocked
            };

            //Loads the List of Scrolling backgrounds, and gives them their speed values and layer value
            _scrollingBackgrounds = new List<ScrollingBackground>()
            {
                new ScrollingBackground(Assets.Sprites.CloudsFast, 18f, true) {Layer = 0.99f,},

                new ScrollingBackground(Assets.Sprites.CloudsSlow, 25f, true) {Layer = 0.77f,}
            };
        }

        /// <summary>
        /// Change State
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(ScreenStates state)
        {
            _state = StateManager.States[state];
        }

        /// <summary>
        /// Update the game and the importent managers
        /// Update is called in Gameworld
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            InputManager.Update();
            _debugManager.Update(gameTime);
            _state.Update(this);

            //loops the backgrounds
            foreach(var sb in _scrollingBackgrounds)
            {
                sb.Update(gameTime);
            }

            // Save progress on exit
            if(Keyboard.GetState().IsKeyDown(Keys.Escape) || Globals._quit == true)
            {
                // save
                //Save(_sm);
                _dataManager.BinarySerialize(_sm, _savePath);
                _saveFileCreated = true;
            }
        }

        /// <summary>
        /// Draw scrolling background, debugmanager and states
        /// called in gameworld
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            foreach(var sb in _scrollingBackgrounds)
            {
                sb.Draw(gameTime, Globals.SpriteBatch);
            }

            _debugManager.Draw();
            _state.Draw(this);
        }

        #region Button Methods
        /// <summary>
        /// The Play Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Play(object sender, EventArgs e)
        {
            StateManager.States.Remove(ScreenStates.Game);
            StateManager.States.Add(ScreenStates.Game, new GameState(this));
            ChangeState(ScreenStates.Game);
        }
        /// <summary>
        /// Restart game button (not implemented in this version)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void Restart(object sender, EventArgs e)
        //{
        //    StateManager.States.Remove(ScreenStates.Game);
        //    StateManager.States.Clear();
        //    StateManager.States.Add(ScreenStates.Game, new GameState(this));

        //    ChangeState(ScreenStates.Game);
        //}

        
        public void LoadSave(object sender, EventArgs e)
        {
            if(_saveFileCreated)
            {
                _sm = _dataManager.BinaryDeserialize(_savePath) as ScoreManager;
                
                //_sm = Load();
                //Trace.WriteLine($"{_sm.LevelXDone} {_sm.EndlessUnlocked}");
                Globals.LevelXDone = _sm.LevelXDone;
                Globals.Unlocked = _sm.EndlessUnlocked;
                StateManager.States.Add(ScreenStates.Game, new GameState(this));
                ChangeState(ScreenStates.Game);
            }
        }

        /// <summary>
        /// Quit Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Quit(object sender, EventArgs e)
        {
            Globals._quit = true;
        }

        /// <summary>
        /// Turn on/off music Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ToggleMusic(object sender, EventArgs e)
        {
            if(!Globals._musicIsPaused)
            {
                //MediaPlayer.Play();

                MediaPlayer.Volume = 0f;
                Globals._musicIsPaused = true;
            }
            else
            {
                //MediaPlayer.Pause();

                MediaPlayer.Volume = 1.2f;
                Globals._musicIsPaused = false;
            }
        }

        /// <summary>
        /// Turn on/off Sound effects button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ToggleSoundEffect(object sender, EventArgs e)
        {
            if(Globals._soundEffectsMuted)
                Globals._soundEffectsMuted = false;
            else
                Globals._soundEffectsMuted = true;
        }
        #endregion Button Methods
                
        #region OLD Load/save        
        /// <summary>
        /// Old save function
        /// </summary>
        /// <param name="sm"></param>
        private void Save(ScoreManager sm)
        {
            string SaveThis = JsonSerializer.Serialize<ScoreManager>(sm);
            Trace.WriteLine(SaveThis);
            //File.Encrypt(_savePath); bulshit ntfs shit
            File.AppendAllText(_savePath, SaveThis);

            //File.AppendAllText(_savePath,)
            _saveFileCreated = true;
        }

        /// <summary>
        /// Old load function
        /// </summary>
        /// <returns></returns>
        private ScoreManager Load()
        {
            //File.Decrypt(_savePath);
            var loadedData = File.ReadAllText(_savePath);  // read alllines output is a string array readall text is just a string
            return JsonSerializer.Deserialize<ScoreManager>(loadedData);
        }

        /// <summary>
        /// Encrypt a file with epic leet encryption
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string AddEpicL33tEncryption(string fileName)
        {

            //File.Encrypt(FileName);
            var strLow = fileName.ToLower();
            return fileName.Replace("a", "@").Replace("e", "3").Replace("i", "!").Replace("o", "0").Replace("s", "5").Replace("l", "£").Replace("c", "<").Replace("r", "&");
        }

        /// <summary>
        /// Decrypt a file with epic leet encryption
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string RemoveEpicL33tEncryption(string fileName)
        {
            //File.Decrypt(FileName);
            var strLow = fileName.ToLower();
            return fileName.Replace("@", "a").Replace("3", "e").Replace("!", "1").Replace("0", "o").Replace("5", "s").Replace("£", "l").Replace("<", "c").Replace("&", "r");
        }
        #endregion Load/Save
        #endregion Methods
    }
}
