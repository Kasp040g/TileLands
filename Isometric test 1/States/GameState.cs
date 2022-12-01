
namespace Isometric_test_1
{
    public class GameState : State
    {
        // Instantiate map
        public static Map _map = new();

               

        public override void Update(GameManager gm)
        {
            _map.Update();
            
        }

        public override void Draw(GameManager gm)
        {
            _map.Draw();
            
        }
    }
}
