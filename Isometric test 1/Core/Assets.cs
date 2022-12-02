
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

        public struct Sprites
        {
            // Backgrounds
            public static Texture2D SplashScreen;
            public static Texture2D MenuScreen;

            // Buttons
            public static Texture2D Btn_Big;
            public static Texture2D Btn_Small;

            // Animations
            public static Texture2D Eagle_ss;
            public static Texture2D Deer_m_run_ss;
            public static Texture2D Deer_f_run_ss;

            // Tile struct members
            public static Texture2D tileGrassBlock1;
            public static Texture2D tileGrassBlock2;
            public static Texture2D tileGrassBlock3;
            public static Texture2D tileGrassBlock4;
            public static Texture2D tileEmpty;

            // Tile objects
            public static Texture2D tileObjectGrass;
            public static Texture2D tileObjectBush;
            public static Texture2D tileObjectTree;
            public static Texture2D tileObjectForest;

            // Backgrounds
            public static Texture2D cloudsFast;
            public static Texture2D cloudsSlow;


            public static void LoadSprites()
            {
                // Backgrounds
                SplashScreen = Globals.Content.Load<Texture2D>("Splash");
                MenuScreen = Globals.Content.Load<Texture2D>("tempmenu");

                // Buttons
                Btn_Big = Globals.Content.Load<Texture2D>("Button");
                Btn_Small = Globals.Content.Load<Texture2D>("Button_square");


                // Animations
                Eagle_ss = Globals.Content.Load<Texture2D>("SingleEagle_ss");
                Deer_m_run_ss = Globals.Content.Load<Texture2D>("deer_f_run");
                Deer_f_run_ss = Globals.Content.Load<Texture2D>("deer_m_run");

                // Tile struct members
                tileGrassBlock1 = Globals.Content.Load<Texture2D>("tile0");
                tileGrassBlock2 = Globals.Content.Load<Texture2D>("tile1");
                tileGrassBlock3 = Globals.Content.Load<Texture2D>("tile2");
                tileGrassBlock4 = Globals.Content.Load<Texture2D>("tile3");
                tileEmpty = Globals.Content.Load<Texture2D>("tile5");

                // Tile Objects
                tileObjectGrass = Globals.Content.Load<Texture2D>("tileObjectGrass");
                tileObjectBush = Globals.Content.Load<Texture2D>("tileObjectBush");
                tileObjectTree = Globals.Content.Load<Texture2D>("tileObjectTree");
                tileObjectForest = Globals.Content.Load<Texture2D>("tileObjectForest");

                //Backgrounds
                cloudsFast = Globals.Content.Load<Texture2D>("Backgrounds/Clouds_Fast");
                cloudsSlow = Globals.Content.Load<Texture2D>("Backgrounds/Clouds_Slow");

        }

            

            

        }


        /// <summary>
        /// Audio struct member
        /// </summary>
        public struct Audio
        {
            // SoundEffects
            public static SoundEffect MergeSound;
            public static SoundEffect WinSound;
            public static SoundEffect ResetSound;

            // Background Music
            public static Song BackgroundMusic;


            // Method to load audio files and assign them to the struct members
            public static void LoadAudio()
            {
                // SoundEffects
                MergeSound = Globals.Content.Load<SoundEffect>("Audio/Pop_sound_5");
                WinSound = Globals.Content.Load<SoundEffect>("Audio/WinSound");
                ResetSound = Globals.Content.Load<SoundEffect>("Audio/ResetSound");

                // Music
                BackgroundMusic = Globals.Content.Load<Song>("Audio/lunar lounging_mp3");

            }
        }

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
