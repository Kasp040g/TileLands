using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.Direct3D9;

namespace Isometric_test_1
{
    public class Sprites : Component
    {
        protected float _layer { get; set; }

        protected Texture2D _texture;


        public struct Library
        {
            // Tiles
            public static Texture2D tileGrassBlockEmpty = Globals.Content.Load<Texture2D>("tile0");
            public static Texture2D tileGrassBlockGrass = Globals.Content.Load<Texture2D>("tile1");
            public static Texture2D tileGrassBlockBush = Globals.Content.Load<Texture2D>("tile2");
            public static Texture2D tileGrassBlockTree = Globals.Content.Load<Texture2D>("tile3");
            public static Texture2D tileEmpty = Globals.Content.Load<Texture2D>("tile5");
        }



        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
            }
        }

        public Vector2 Position;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        public Sprites(Texture2D texture)
        {
            _texture = texture;
        }
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, Position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, Layer);
        }


    }
}
