

namespace TileLands
{
    /// <summary>
    /// Parent class for assets and scrolling background
    /// </summary>
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
