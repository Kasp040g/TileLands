using Isometric_test_1.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric_test_1
{
    public class GameManager
    {
        private readonly Map _map = new();
        private readonly DebugManager _debugManager = new();


        public void Update(GameTime gameTime)
        {
            InputManager.Update();
            _map.Update();
            _debugManager.Update(gameTime);
        }

        public void Draw()
        {
            _map.Draw();
            _debugManager.Draw();
        }
    }
}
