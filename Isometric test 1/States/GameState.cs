using System.Collections.Generic;

namespace Isometric_test_1
{
    public class GameState : State
    {
        // Instantiate map
        public static Map _map = new();

        private List<Button> _buttons = new();

        public GameState(GameManager gm)
        {
            // Init Screen Bound
            var y = 50;
            var x = 200;
            var btn_offset = 100;

            //Bottons
            AddButton(new(Assets.Sprites.Btn_Small, new(x, y ))).OnClick += _map.ResetLevel;
            AddButton(new(Assets.Sprites.Btn_Small, new(x + btn_offset, y))).OnClick += gm.ToggleMusic;
            AddButton(new(Assets.Sprites.Btn_Small, new(x + btn_offset + btn_offset, y + 300))).OnClick += gm.ToggleSoundEffect;
            
        }

        private Button AddButton(Button button)
        {
            _buttons.Add(button);
            return button;
        }

        public override void Update(GameManager gm)
        {
            _map.Update();
            
        }

        public override void Draw(GameManager gm)
        {
            _map.Draw();
            
        }
    }
}
