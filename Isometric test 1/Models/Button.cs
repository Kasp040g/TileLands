using System;
using System.Collections.Generic;

namespace TileLands
{
    public class Button
    {
        #region Fields
        // Mouse click and hover
        public event EventHandler OnClick;
        private bool _mouseIsHovering;

        // button data
        private Rectangle _rectangle;
        private Vector2 _origin;
        private Vector2 _scale;     
        
        // List of sprites for button
        private Texture2D[] _btnSprites = new Texture2D[2];

        // Properties
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Single sprite button constructor
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="pos"></param>
        public Button(Texture2D tex, Vector2 pos)
        {
            Texture = tex;
            Position = pos;
            _origin = new(tex.Width / 2, tex.Height / 2);
            _scale = Vector2.One;

            _rectangle = new((int)(pos.X - _origin.X), (int)(pos.Y - _origin.Y), tex.Width, tex.Height);

        }

        /// <summary>
        /// Constructor for changing sprite when button is clicked
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="tex2"></param>
        /// <param name="pos"></param>
        public Button(Texture2D tex, Texture2D tex2, Vector2 pos)
        {
            _btnSprites[0] = tex;
            _btnSprites[1] = tex2;

            Texture = tex;

            Position = pos;
            _origin = new(tex.Width / 2, tex.Height / 2);
            _scale = Vector2.One;

            _rectangle = new((int)(pos.X - _origin.X), (int)(pos.Y - _origin.Y), tex.Width, tex.Height);
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Update button
        /// </summary>
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
                // if the rectangles of mouse and a button intersect, invoke the subsribed method defined in menustate when the button is instatiated
                OnClick?.Invoke(this, EventArgs.Empty);

                // Sprite change when clicked and bool change state
                ChangeIcon(Globals._musicIsPaused || Globals._soundEffectsMuted);
            }
        }

        /// <summary>
        /// Changing the icon of the button
        /// </summary>
        /// <param name="change"></param>
        private void ChangeIcon(bool change)
        {
            if(!change && _btnSprites[0] != null)
                Texture = _btnSprites[0];
            else if(change && _btnSprites[1] != null)
                Texture = _btnSprites[1];
        }

        /// <summary>
        /// Draw the button 
        /// called in the states the button is used
        /// </summary>
        public void Draw()
        {
            var _btnColor = Color.White;

            if(_mouseIsHovering)
                _btnColor = Color.BlanchedAlmond;

            Globals.SpriteBatch.Draw(Texture, Position, null, _btnColor, 0f, _origin, _scale, SpriteEffects.None, 1f);
        }
    }
    #endregion Methods
}
