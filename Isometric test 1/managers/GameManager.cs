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

        //Animations
        private Eagle _bird_ss = new(new(GameWorld.ScreenWidth,100));

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
    }
}
