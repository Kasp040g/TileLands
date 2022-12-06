using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileLands
{
    public class Button
    {
        public event EventHandler OnClick;

        // button data
        private Rectangle _rectangle;
        private Vector2 origin;
        private Vector2 scale;

        // Properties
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; protected set; }

        public Button(Texture2D tex, Vector2 pos)
        {
            Texture = tex;
            Position = pos;
            origin = new(tex.Width / 2, tex.Height / 2);
            scale = Vector2.One;

            _rectangle = new((int)(pos.X - origin.X), (int)(pos.Y - origin.Y), tex.Width, tex.Height);
        }

        public void Update()
        {
            if(InputManager.MouseLeftClicked && _rectangle.Contains(InputManager.MouseRectangle))
            {
                OnClick?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 1f);
        }
    }
}
