
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Isometric_test_1
{
    public class GameManager
    {
        // Init States
        private State _state;

        // Instantiate DebugManager
        private DebugManager _debugManager = new();

        // Save/Load
        private ScoreManager _sm;
        private const string _savePath = "testScore.json";
        public static Map _map = new();
        private bool _saveFileCreated;

        //Animations
        private Eagle _bird_ss = new(new(Globals.Bounds.Y / 2, 100));

        public GameManager()
        {
            // Init
            StateManager.Init(this);

            // state
            ChangeState(GameStates.Splash);
        }

        public void Init()
        {
            _sm = new()
            {
                Level1Done = false,
                Level2Done = false,
                Level3Done = false,
                Level4Done = false,
                Level5Done = false,
                EndlessUnlocked = false,
                Score = 1000,
            };

            // load
            if(_saveFileCreated)
            {
                _sm = load();
                Trace.WriteLine($"{_sm.Level1Done} {_sm.Level2Done} {_sm.Level3Done} {_sm.Level4Done} {_sm.Level5Done} {_sm.EndlessUnlocked} {_sm.Score}");
            }

            // save
            save(_sm);
        }

        public void ChangeState(GameStates state)
        {
            _state = StateManager.States[state];
        }

        public void Update(GameTime gameTime)
        {
            InputManager.Update();
            _map.Update();
            _debugManager.Update(gameTime);
            _bird_ss.Update();
            _state.Update(this);
        }

        public void Draw()
        {
            _map.Draw();
            _bird_ss.Draw();
            _debugManager.Draw();
            _state.Draw(this);
        }

        #region Button Methods
        public void Play(object sender, EventArgs e)
        {
            ChangeState(GameStates.Game);
        }

        public void Quit(object sender, EventArgs e)
        {

        }
        #endregion Button Methods

        private void save(ScoreManager sm)
        {
            string SaveThis = JsonSerializer.Serialize<ScoreManager>(sm);
            Trace.WriteLine(SaveThis);
            File.WriteAllText(_savePath, SaveThis);
            // File.AppendAllLines ?? for adding data??
            _saveFileCreated = true;
        }

        private ScoreManager load()
        {
            var loadedData = File.ReadAllText(_savePath);  // read alllines output is a string array readall text is just a string
            return JsonSerializer.Deserialize<ScoreManager>(loadedData);
        }
    }
}

