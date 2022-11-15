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


        public void Update()
        {
            InputManager.Update();
            _map.Update();
        }

        public void Draw()
        {
            _map.Draw();
        }
    }
}
