using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric_test_1
{
    public enum GameStates
    {
        Splash,
        Menu,
        Game,

    }

    public static class StateManager
    {
        public static Dictionary<GameStates, State> States { get; } = new();

        public static void Init(GameManager gm)
        {
            States.Clear();
            States.Add(GameStates.Splash, new SplashState());
            States.Add(GameStates.Menu, new MenuState(gm));
            States.Add(GameStates.Game, new GameState());


        }
    }
}
