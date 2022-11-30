
namespace Isometric_test_1
{
    public class GameState : State
    {
        // Instantiate map
        public static Map _map = new();

        //Animations
        private Eagle _bird_ss = new(new(Globals.Bounds.Y / 2, 100));        

        public override void Update(GameManager gm)
        {
            _map.Update();
            _bird_ss.Update();
        }

        public override void Draw(GameManager gm)
        {
            _map.Draw();
            _bird_ss.Draw();
        }
    }
}
