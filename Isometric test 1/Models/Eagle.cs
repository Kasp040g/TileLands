using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric_test_1
{
    internal class Eagle
    {
        private static Texture2D _texture;
        private Vector2 _position;
        private Animation _anim;
        private Vector2 _eagleSpeed = new Vector2(-1,0);

        public Eagle(Vector2 pos)
        {
            _texture ??= Sprites.Eagle_ss;
            _anim = new(_texture, 14, 1, 0.1f, 1);
            _position = pos;
        }

        public void Update()
        {
            _position += _eagleSpeed;
            _anim.Update();
        }

        public void Draw()
        { 
            _anim.Draw(_position);
        }

    }
}
