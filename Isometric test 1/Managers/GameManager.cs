using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Isometric_test_1
{
    public class GameManager
    {
        // Save/Load
        private ScoreManager _sm;
        private const string _savePath = "testScore.json";
        private readonly Map _map = new();

        //Animations
        private Eagle _bird_ss = new(new(GameWorld.ScreenWidth, 100));

        public void Init()
        {
            _sm = new()
            {
                Level3Done = false,
                Score = 1000,
            };           

            // load
           // _sm = load();
            Trace.WriteLine($"{_sm.Level3Done} {_sm.Score}");

            // save
            save(_sm);
        }

        public void Update()
        {
            InputManager.Update();
            _map.Update();
            _bird_ss.Update();

            
        }

        public void Draw()
        {
            _map.Draw();
            _bird_ss.Draw();
        }

        private void save(ScoreManager sm)
        {
            string SaveThis = JsonSerializer.Serialize<ScoreManager>(sm);
            Trace.WriteLine(SaveThis);
            File.WriteAllText(_savePath, SaveThis);
            // File.AppendAllLines ?? for adding data??
        }

        private ScoreManager load()
        {
            var loadedData = File.ReadAllText(_savePath);  // read alllines output is a string array readall text is just a string
            return JsonSerializer.Deserialize<ScoreManager>(loadedData);
        }
    }
}
