using System.Collections.Generic;
using System.Windows.Forms;

namespace TileLands
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
            AddButton(new(Assets.Sprites.Btn_Big, new(x, y))).OnClick += gm.Play;
            AddButton(new(Assets.Sprites.Btn_Big, new(x, y + Assets.Sprites.Btn_Big.Height * (float)1.5))).OnClick += gm.Restart;
        }

        private Button AddButton(Button button)
        {
            _buttons.Add(button);
            return button;
        }

        public override void Update(GameManager gm)
        {            
            foreach(var button in _buttons)
            {
                button.Update();
            }
        }

        public override void Draw(GameManager gm)
        {
            // Draw Background
            Globals.SpriteBatch.Draw(Assets.Sprites.MenuScreen, Vector2.Zero, Color.White);

            // Draw Buttons
            foreach(var button in _buttons)
            {
                button.Draw();
            }
        }
    }
}
