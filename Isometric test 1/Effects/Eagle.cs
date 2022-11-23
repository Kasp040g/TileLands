using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric_test_1
{
    public class Eagle
    {
        protected AnimationManager _animationManager;
                
        protected Dictionary<string, Animation> _animations;

        protected Texture2D _texture;


        public float Speed = 1f;

        public Vector2 Velocity;

        protected Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if(_animationManager != null)
                {
                    _animationManager.Position = _position;
                }
            }
        }               
        
        public Eagle(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value); // .first = set to first animation 
        }

        public void Update(GameTime gameTime, List<Eagle> coins)
        {
            SetAnimations();

            _animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if(_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("SHITS FUCKED!!");
        }

        protected void SetAnimations()
        {
            _animationManager.Play(_animations["Bird_ss"]);  //call for spritesheet name

        }

    }
}
