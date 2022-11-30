using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric_test_1
{
    public class SplashState : State
    {

        public SplashState()
        {

        }

        public override void Update(GameManager gm)
        {
            if(InputManager.SpacePressed)
            {
                gm.ChangeState(GameStates.Menu);
            }
        }

        public override void Draw(GameManager gm)
        {
            Globals.SpriteBatch.Draw(Sprites.SplashScreen, Vector2.Zero, Color.White);
        }
    }
}
