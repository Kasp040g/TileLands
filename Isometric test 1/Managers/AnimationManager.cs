using System.Collections.Generic;


namespace TileLands
{
    /// <summary>
    /// class not used in current version
    /// saved for later functionality
    /// </summary>
    public class AnimationManager
    {
        private Dictionary<object, Animation> _anims = new();
        private object _lastKey;

        /// <summary>
        /// Adds animation and bind it to a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="animation"></param>
        public void AddAnimation(object key, Animation animation)
        {
            _anims.Add(key, animation);
            _lastKey ??= key;
        }

        /// <summary>
        /// update animation, if key is pressed display chosen animation, if key not pressed, stop animation and reset animation.
        /// </summary>
        /// <param name="key"></param>
        public void Update(object key)
        {
            if(_anims.TryGetValue(key, out Animation value))
            {
                value.Start();
                _anims[key].Update();
                _lastKey = key;
            }
            else
            {
                _anims[_lastKey].Stop();
                _anims[_lastKey].Reset();
            }
        }

        /// <summary>
        /// draw chosen animation
        /// </summary>
        /// <param name="position"></param>
        public void Draw(Vector2 position)
        {
            _anims[_lastKey].Draw(position);
        }
    }
}
