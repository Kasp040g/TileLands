
namespace TileLands
{
    public class SplashState : State
    {       
        public SplashState()
        {
            // Play Intro song
            MediaPlayer.Play(Assets.Audio.Splash_intro);
        }

        public override void Update(GameManager gm)
        {
            if(MediaPlayer.State != MediaState.Playing)
            {
                if(InputManager.SpacePressed || InputManager.MouseLeftClicked)
                {
                    gm.ChangeState(ScreenStates.Menu);
                }
            }            
        }

        public override void Draw(GameManager gm)
        {            
            Globals.SpriteBatch.Draw(Assets.Sprites.SplashScreen, Vector2.Zero, Color.White);
        }
    }
}
