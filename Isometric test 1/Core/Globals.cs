
namespace Isometric_test_1
{
    public static class Globals
    {
        // Global sound effect bool
        public static bool _soundEffectsMuted;

        ///Global variables for use in the project by all objects
        public static float TotalSeconds { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static Point Bounds { get; set; }            // Screen Bounderies
        public static SpriteFont FontTest { get; set; }
        public static bool DebugModeToggled { get; set; }   // Debug
        




       
        /// <summary>
        /// Update total seconds elapsed
        /// </summary>
        /// <param name="gt"></param>
        public static void Update(GameTime gt)
        {
            TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
