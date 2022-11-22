using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric_test_1
{
    public class ScrollingBackground : Component
    {
        /// <summary>
        /// Is the background moving all the time?
        /// </summary>
        private bool _constantSpeed;

        private float _layer;

        private float _scrollingSpeed;

        private List<Sprite> _sprites;


        private float _speed;

        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
                foreach (var sprite in _sprites)
                    sprite.Layer = _layer;
            }
        }


        public ScrollingBackground(Texture2D texture, float scrollingSpeed, bool constantSpeed = false)
            : this(new List<Texture2D>() { texture, texture }, scrollingSpeed, constantSpeed)
        {

        }
        public ScrollingBackground(List<Texture2D> textures, float scrollingSpeed, bool constantSpeed = false)
        {

            _scrollingSpeed = scrollingSpeed;

            _constantSpeed = constantSpeed;

            _sprites = new List<Sprite>();

            for (int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];
                _sprites.Add(new Sprite(texture)
                {
                    Position = new Vector2((i * texture.Width) - 1, GameWorld.ScreenHeight - texture.Height) //låser skærmen til bunden. hvis den skulle låses til toppen skulle der bare stå 0
                });
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            ApplySpeed(gameTime);

            CheckPosition();
        }
        private void ApplySpeed(GameTime gameTime)
        {
            _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            //if (!_constantSpeed || _player.Velocity.X != 0)
            //    _speed *= _player.Velocity.X;
            ////_speed *= _player.Velocity.Y;

            foreach (var sprite in _sprites)
                sprite.Position.X -= _speed;
        }
        private void CheckPosition()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (sprite.Rectangle.Right <= 0)
                {
                    var index = i - 1;

                    if (index < 0)
                        index = _sprites.Count - 1;

                    sprite.Position.X = _sprites[index].Rectangle.Right - (_speed * 2);
                }
            }
        }


    }
}