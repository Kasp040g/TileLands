using System.Collections.Generic;

namespace TileLands
{
    public class MenuState : State
    {
        private List<Button> _buttons = new();

        /// <summary>
        /// Menustate Constructor which will add buttons to the screen
        /// </summary>
        /// <param name="gm"></param>
        public MenuState(GameManager gm)
        {
            // Init Screen Bound
            var y = Globals.Bounds.Y / 2;
            var x = Globals.Bounds.X / 2;

            //Bottons            
            AddButton(new(Assets.Sprites.Btn_Play, new(x, y))).OnClick += gm.Play;            
            AddButton(new(Assets.Sprites.Btn_Quit, new(x, y + Assets.Sprites.Btn_Quit.Height * (float)1.5f))).OnClick += gm.Quit;
        }
        /// <summary>
        /// Add button to the list of buttons
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
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
