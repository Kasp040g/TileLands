namespace TileLands
{
    /// <summary>
    /// Eagle Animation Class
    /// </summary>
    internal class Eagle
    {
        private static Texture2D _texture;
        private Vector2 _position;
        private Animation _anim;
        private Vector2 _eagleSpeed = new Vector2(-1, 0);

        public Eagle(Vector2 pos)
        {
            _texture ??= Assets.Sprites.Eagle_ss;
            _anim = new(_texture, 14, 1, 0.1f, 1, 1, 0.1f);
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
