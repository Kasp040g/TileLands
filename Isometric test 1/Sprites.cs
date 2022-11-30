
namespace Isometric_test_1
{
    static class Sprites
    {
        public static Texture2D SplashScreen { get; private set; }
        public static Texture2D MenuScreen { get; private set; }
        public static Texture2D Btn_Big { get; private set; }
        public static Texture2D Btn_Small { get; private set; }
        public static Texture2D Eagle_ss { get; private set; }

        public static void Load(ContentManager content)
        {

            // Screen BackGrounds
            SplashScreen = content.Load<Texture2D>("Splash");
            MenuScreen = content.Load<Texture2D>("tempmenu");

            Btn_Big = content.Load<Texture2D>("Button");
            Btn_Small = content.Load<Texture2D>("Button_square");

            Eagle_ss = content.Load<Texture2D>("SingleEagle_ss");
        }
    }
}
