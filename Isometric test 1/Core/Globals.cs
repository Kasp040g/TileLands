
namespace TileLands
{
    public static class Globals
    {
        // Global sound effect bool
        public static bool _soundEffectsMuted;
        // global Music bool
        public static bool _musicIsPaused;
        // Gloabl Bool for exiting the game
        public static bool _quit = false;

        /// Global variables for use in the project by all objects
        public static float TotalSeconds { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static Point Bounds { get; set; }            // Screen Bounderies
        public static SpriteFont FontTest { get; set; }
        public static bool DebugModeToggled { get; set; }   // Debug

        // LoadSave
        public static int LevelXDone { get; set; }
        public static bool Unlocked { get; set; }

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
