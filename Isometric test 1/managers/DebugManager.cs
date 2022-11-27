using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Isometric_test_1.Tile;

namespace Isometric_test_1.managers
{
    internal class DebugManager
    {
        double _frameRate = 0;                  // Keeps track of the current frame rate of the game

        bool _canPressF3 = true;                // Checks whether or not the F3 button can be pressed again


        /// <summary>
        /// Debug manager constructer, creates an instance of the debug manager
        /// </summary>
        public DebugManager()
        {
            // Turn of debug mode
            Globals.DebugModeToggled = false;
        }


        /// <summary>
        /// Updates the debug manager, is called in gamemanager's update
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Get the keyboard state to check inputs
            KeyboardState _keyboardState = Keyboard.GetState();

            // Check for debug input (F3) and if it's allowed to be pressed again
            if (_keyboardState.IsKeyDown(Keys.F3) && _canPressF3 == true)
            {
                //Toggles the debug mode
                if (Globals.DebugModeToggled == false)
                {
                    Globals.DebugModeToggled = true;
                }
                else
                {
                    Globals.DebugModeToggled = false;
                }

                // F3 can't be pressed until the button is released
                _canPressF3 = false;
            }
            else if (_keyboardState.IsKeyUp(Keys.F3))       // F3 is released
            {
                // F3 can now be pressed again
                _canPressF3 = true;
            }

            // Update the frame rate tracker
            _frameRate = (1 / Globals.TotalSeconds);
        }


        /// <summary>
        /// Debug manager draw method, is called in gamemanager's draw event
        /// </summary>
        public void Draw()
        {
            // Check if debug mode is toggled
            if (Globals.DebugModeToggled)
            {
                // Draws the frame rate out to the screen
                Globals.SpriteBatch.DrawString(Globals.FontTest, $"{_frameRate}", new Vector2(20,20), Color.Black);
            }
        }
    }
}
