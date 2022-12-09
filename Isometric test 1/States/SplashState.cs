
namespace TileLands
{
    public class SplashState : State
    {
        public SplashState()
        {
        }

        public override void Update(GameManager gm)
        {
            if(InputManager.SpacePressed || InputManager.MouseLeftClicked)
            {
                gm.ChangeState(ScreenStates.Menu);
            }
        }

        public override void Draw(GameManager gm)
        {            
            Globals.SpriteBatch.Draw(Assets.Sprites.SplashScreen, Vector2.Zero, Color.White);
        }
    }
}
