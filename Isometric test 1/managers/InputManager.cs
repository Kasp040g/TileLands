
namespace TileLands
{
    public static class InputManager
    {
        // Input states        
        private static MouseState _lastMouseState;
        private static KeyboardState _lastKeyboardState;

        // Direction and Position
        private static Vector2 _direction;
        public static Vector2 Direction => _direction;
        //public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();

        public static Point MousePosition => Mouse.GetState().Position;

        // Keyboard properties
        public static bool SpacePressed { get; private set; }

        // Mouse properties
        public static Rectangle MouseRectangle { get; private set; }
        public static bool MouseRightClicked { get; private set; }
        public static bool MouseLeftClicked { get; private set; }

        public static void Update()
        {
            // Storing Input states
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var onscreen = mouseState.X >= 0 && mouseState.X < Globals.SpriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth
                        && mouseState.Y >= 0 && mouseState.Y < Globals.SpriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight;

            // Mouse Handling
            MouseLeftClicked = (mouseState.LeftButton == ButtonState.Pressed) && (_lastMouseState.LeftButton == ButtonState.Released) && onscreen;
            MouseRightClicked = (mouseState.RightButton == ButtonState.Pressed) && (_lastMouseState.RightButton == ButtonState.Released) && onscreen;
            MouseRectangle = new(mouseState.X, mouseState.Y, 1, 1);

            // Key Handling
            SpacePressed = _lastKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space);

            // Reseting Input States
            _lastMouseState = mouseState;
            _lastKeyboardState = keyboardState;
        }
    }
}
