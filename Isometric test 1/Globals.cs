using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Isometric_test_1
{
    public static class Globals
    {
        ///Global variables for use in the project by all objects
        public static float TotalSeconds { get; set; }
        public static ContentManager Content { get; set; }

        public static SpriteBatch SpriteBatch { get; set; }

        public static SpriteFont FontTest { get; set; }


        /// <summary>
        /// Update total seconds elapsed
        /// </summary>
        /// <param name="gt"></param>
        public static void Update(GameTime gt)
        {
            TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
