using System;

namespace TileLands
{
    public class Button
    {
        // Mouse click and hover
        public event EventHandler OnClick;
        private bool _mouseIsHovering;

        // button data
        private Rectangle _rectangle;
        private Vector2 _origin;
        private Vector2 _scale;
        private bool _toggle;
        private Texture2D[] _btn_sound = new Texture2D[3];


        // Properties
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }

        public Button(Texture2D tex, Vector2 pos)
        {
            Texture = tex;
            Position = pos;
            _origin = new(tex.Width / 2, tex.Height / 2);
            _scale = Vector2.One;

            _rectangle = new((int)(pos.X - _origin.X), (int)(pos.Y - _origin.Y), tex.Width, tex.Height);

        }
        public Button(Texture2D tex, Texture2D tex2, Vector2 pos)
        {
            _btn_sound[0] = tex;
            _btn_sound[1] = tex2;
            Texture = tex;

            Position = pos;
            _origin = new(tex.Width / 2, tex.Height / 2);
            _scale = Vector2.One;

            _rectangle = new((int)(pos.X - _origin.X), (int)(pos.Y - _origin.Y), tex.Width, tex.Height);
        }

        public void Update()
        {
            // Button Color change when hover 
            _mouseIsHovering = false;
            if(InputManager.MouseRectangle.Intersects(_rectangle))
            {
                _mouseIsHovering = true;
            }

            if(InputManager.MouseLeftClicked && _rectangle.Contains(InputManager.MouseRectangle))
            {
                OnClick?.Invoke(this, EventArgs.Empty);
            }
            if(Globals._musicIsPaused && _btn_sound[0] != null)
                Texture = _btn_sound[0];
            else if(!Globals._musicIsPaused && _btn_sound[1] != null)            
                Texture = _btn_sound[2];


            if(Globals._soundEffectsMuted && _btn_sound[0] != null)            
                Texture = _btn_sound[0];            
            else if(!Globals._soundEffectsMuted && _btn_sound[1] != null)            
                Texture = _btn_sound[1];
           
        }

        public void Draw()
        {
            var _btnColor = Color.White;

            if(_mouseIsHovering)
                _btnColor = Color.BlanchedAlmond;

            Globals.SpriteBatch.Draw(Texture, Position, null, _btnColor, 0f, _origin, _scale, SpriteEffects.None, 1f);
        }
    }
}
