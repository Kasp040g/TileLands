using System.Collections.Generic;

namespace Isometric_test_1
{
    public class MenuState : State
    {

        private List<Button> _buttons = new();
        public MenuState(GameManager gm)
        {
            // Init Screen Bound
            var y = Globals.Bounds.Y / 2;
            var x = Globals.Bounds.X / 2;

            //Bottons            
            AddButton(new(Sprites.Btn_Big, new(x, y))).OnClick += gm.Play;
            AddButton(new(Sprites.Btn_Big, new(x, y + 300))).OnClick += gm.Quit;
        }

        private Button AddButton(Button button)
        {
            _buttons.Add(button);
            return button;
        }

        public override void Update(GameManager gm)
        {
            //private static SpriteFont _font;
            //private static Vector2 _textPosition;
            //private static string _text = "";

            //_text = "Play";
            //var size = _font.MeasureString(_text);
            //_textPosition = new((Globals.Bounds.X - size.X) / 2, _position.Y + (_texture.Height / 4));

            foreach(var button in _buttons)
            {
                button.Update();
            }
        }

        public override void Draw(GameManager gm)
        {
            // Draw Background
            Globals.SpriteBatch.Draw(Sprites.MenuScreen, Vector2.Zero, Color.White);

            // Draw Buttons
            foreach(var button in _buttons)
            {
                button.Draw();
            }
        }
    }
}
