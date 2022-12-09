using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace TileLands
{
    public class GameManager
    {
        // Init States
        private State _state;

        // Instantiate DebugManager
        private DebugManager _debugManager = new();

        // Save/Load
        private ScoreManager _sm;
        private const string _savePath = "topsecret.data";
        public bool _saveFileCreated;

        //Background
        public static List<ScrollingBackground> _scrollingBackgrounds;

        

        public int lxd;
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

        public void Init()
        {


            // sound effect not muted
            Globals._soundEffectsMuted = false;

            //Load audio files
            Assets.Audio.LoadAudio();

            //Plays and repeats the background music
            MediaPlayer.Play(Assets.Audio.BackgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1.0f;

            _sm = new()
            {
                LevelXDone = 0,
                EndlessUnlocked = false,
                Score = 0,
            };





            //Loads the List of Scrolling backgrounds, and gives them their speed values and layer value
            _scrollingBackgrounds = new List<ScrollingBackground>()
            {
                new ScrollingBackground(Assets.Sprites.CloudsFast, 18f, true)
                {
                  Layer = 0.99f,
                },

                new ScrollingBackground(Assets.Sprites.CloudsSlow, 25f, true)
                {
                  Layer = 0.8f,
                }
            };
        }

        public void ChangeState(ScreenStates state)
        {
            _state = StateManager.States[state];
        }

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

            if(Keyboard.GetState().IsKeyDown(Keys.Escape) || Globals._quit == true)
            {
                // save
                Save(_sm);
            }
        }

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
        public void Play(object sender, EventArgs e)
        {
            ChangeState(ScreenStates.Game);
        }

        public void Restart(object sender, EventArgs e)
        {
            StateManager.States.Remove(ScreenStates.Game);
            StateManager.States.Add(ScreenStates.Game, new GameState(this));

            ChangeState(ScreenStates.Game);
        }

        public void LoadSave(object sender, EventArgs e)
        {
            if(_saveFileCreated)
            {
                _sm = Load();
                Trace.WriteLine($"{_sm.LevelXDone} {_sm.EndlessUnlocked} {_sm.Score}");
                ChangeState(ScreenStates.Game);
            }
        }

        public void Quit(object sender, EventArgs e)
        {
            Globals._quit = true;
        }

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

        public void ToggleSoundEffect(object sender, EventArgs e)
        {
            if(Globals._soundEffectsMuted)
                Globals._soundEffectsMuted = false;
            else
                Globals._soundEffectsMuted = true;
        }
        #endregion Button Methods

        #region Load/save
        private void Save(ScoreManager sm)
        {
            string SaveThis = JsonSerializer.Serialize<ScoreManager>(sm);
            Trace.WriteLine(SaveThis);
            //File.Encrypt(_savePath); bulshit ntfs shit
            File.AppendAllText(_savePath, SaveThis);

            //File.AppendAllText(_savePath,)
            _saveFileCreated = true;
        }

        private ScoreManager Load()
        {
            //File.Decrypt(_savePath);
            var loadedData = File.ReadAllText(_savePath);  // read alllines output is a string array readall text is just a string
            return JsonSerializer.Deserialize<ScoreManager>(loadedData);
        }

        // Encrypt a file.
        public static void AddEncryption(string FileName)
        {

            File.Encrypt(FileName);
        }

        // Decrypt a file.
        public static void RemoveEncryption(string FileName)
        {
            File.Decrypt(FileName);
        }
        #endregion Load/Save
    }
}

