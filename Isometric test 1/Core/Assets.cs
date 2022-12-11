
namespace TileLands
{
    public class Assets : Component
    {
        private float _layer;
        protected Texture2D _texture;
        public Vector2 Position;

        public float Layer
        {
            get => _layer;
            set => _layer = value;
        }

        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

        #region Structs
        /// <summary>
        /// Sprites
        /// </summary>
        public struct Sprites
        {
            // Backgrounds
            public static Texture2D SplashScreen;
            public static Texture2D MenuScreen;

            // Buttons
            public static Texture2D Btn_Play;
            public static Texture2D Btn_Quit;
            public static Texture2D Btn_Restart;
            public static Texture2D Btn_Toggle_Sound_On;
            public static Texture2D Btn_Toggle_Sound_Off;
            public static Texture2D Btn_Toggle_Music_On;
            public static Texture2D Btn_Toggle_Music_Off;

            // Animations
            public static Texture2D Eagle_ss;
            public static Texture2D Deer_m_run_ss;
            public static Texture2D Deer_f_run_ss;

            // Tile struct members
            public static Texture2D TileGrassBlock1;
            public static Texture2D TileGrassBlock2;
            public static Texture2D TileGrassBlock3;
            public static Texture2D TileGrassBlock4;
            public static Texture2D TileEmpty;

            // Tile objects
            public static Texture2D TileObjectGrass;
            public static Texture2D TileObjectBush;
            public static Texture2D TileObjectTree;
            public static Texture2D TileObjectForest;

            // Backgrounds
            public static Texture2D CloudsFast;
            public static Texture2D CloudsSlow;

            // Overlays
            public static Texture2D Vignette;


            public static void LoadSprites()
            {
                // Backgrounds
                SplashScreen         = Globals.Content.Load<Texture2D>("SplashScreen");
                MenuScreen           = Globals.Content.Load<Texture2D>("MenuScreen");

                // Buttons
                Btn_Play             = Globals.Content.Load<Texture2D>("Button_play");
                Btn_Quit             = Globals.Content.Load<Texture2D>("Button_exit");
                Btn_Restart          = Globals.Content.Load<Texture2D>("Button_restart");
                Btn_Toggle_Sound_On  = Globals.Content.Load<Texture2D>("Button_sound_effects");
                Btn_Toggle_Sound_Off = Globals.Content.Load<Texture2D>("Button_sound_effects_muted");
                Btn_Toggle_Music_On  = Globals.Content.Load<Texture2D>("Button_music");
                Btn_Toggle_Music_Off = Globals.Content.Load<Texture2D>("Button_music_muted");

                // Animations
                Eagle_ss             = Globals.Content.Load<Texture2D>("SingleEagle_ss");
                Deer_m_run_ss        = Globals.Content.Load<Texture2D>("deer_f_run");
                Deer_f_run_ss        = Globals.Content.Load<Texture2D>("deer_m_run");

                // Tile struct members
                TileGrassBlock1      = Globals.Content.Load<Texture2D>("tile0");
                TileGrassBlock2      = Globals.Content.Load<Texture2D>("tile1");
                TileGrassBlock3      = Globals.Content.Load<Texture2D>("tile2");
                TileGrassBlock4      = Globals.Content.Load<Texture2D>("tile3");
                TileEmpty            = Globals.Content.Load<Texture2D>("tile5");

                // Tile Objects
                TileObjectGrass      = Globals.Content.Load<Texture2D>("tileObjectGrass");
                TileObjectBush       = Globals.Content.Load<Texture2D>("tileObjectBush");
                TileObjectTree       = Globals.Content.Load<Texture2D>("tileObjectTree");
                TileObjectForest     = Globals.Content.Load<Texture2D>("tileObjectForest");

                //Backgrounds
                CloudsFast           = Globals.Content.Load<Texture2D>("Backgrounds/Clouds_Fast");
                CloudsSlow           = Globals.Content.Load<Texture2D>("Backgrounds/Clouds_Slow");

                // Overlays
                Vignette             = Globals.Content.Load<Texture2D>("Vignette");
            }
        }


        /// <summary>
        /// Audio struct member
        /// </summary>
        public struct Audio
        {
            // Splash_intro
            public static Song Splash_intro;
            public static Song Splash_loop;
            public static Song Splash_outro;

            // SoundEffects
            public static SoundEffect MergeSound;
            public static SoundEffect WinSound;
            public static SoundEffect ResetSound;

            // Background Music
            public static Song BackgroundMusic;


            // Method to load audio files and assign them to the struct members
            public static void LoadAudio()
            {
                // Splash
                Splash_intro    = Globals.Content.Load<Song>("Audio/BSBIntro");
                Splash_loop     = Globals.Content.Load<Song>("Audio/BSBLoop");
                Splash_outro    = Globals.Content.Load<Song>("Audio/BSBOutro");

                // SoundEffects
                MergeSound      = Globals.Content.Load<SoundEffect>("Audio/Pop_sound_5");
                WinSound        = Globals.Content.Load<SoundEffect>("Audio/WinSound");
                ResetSound      = Globals.Content.Load<SoundEffect>("Audio/ResetSound");

                // Music
                BackgroundMusic = Globals.Content.Load<Song>("Audio/lunar lounging_mp3");
            }
        }
        #endregion Structs

        public Assets(Texture2D texture)
        {
            _texture = texture;
        }
        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, Position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, Layer);
        }
    }
}
