using System.Collections.Generic;

namespace TileLands
{
    public class GameState : State
    {
        // Instantiate map
        public static Map _map = new();

        private List<Button> _buttons = new();

        public GameState(GameManager gm)
        {
            // Init Screen Bound
            var y = 75;
            var btn_offset = 125;
            var x = GameWorld.ScreenWidth / 2 - btn_offset ;
            

            //Bottons
            AddButton(new(Assets.Sprites.Btn_Small, new(x, y ))).OnClick += _map.ResetLevel;
            AddButton(new(Assets.Sprites.Btn_Small, new(x + btn_offset, y))).OnClick += gm.ToggleMusic;
            AddButton(new(Assets.Sprites.Btn_Small, new(x + btn_offset + btn_offset, y))).OnClick += gm.ToggleSoundEffect;            
        }

        private Button AddButton(Button button)
        {
            _buttons.Add(button);
            return button;
        }

        public override void Update(GameManager gm)
        {
            _map.Update();

            // Update button
            foreach(var button in _buttons)
            {
                button.Update();
            }
        }

        public override void Draw(GameManager gm)
        {
            _map.Draw();

            // Draw Buttons
            foreach(var button in _buttons)
            {
                button.Draw();
            }
        }
    }
}
