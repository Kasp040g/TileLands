
namespace TileLands
{
    public class Deer
    {
        private static Texture2D _texture;
        private Vector2 _position;
        private Animation _anim;
        private Vector2 _animSpeed = new Vector2(-1, 0);

        public Deer(Vector2 pos)
        {
            _texture ??= Assets.Sprites.Deer_m_run_ss;
            _anim = new(_texture, 3, 4, 0.1f, 1);
            _position = pos;
        }

        public void Update()
        {
            _position += _animSpeed;
            _anim.Update();
        }

        public void Draw()
        {
            _anim.Draw(_position);
        }
    }
}
