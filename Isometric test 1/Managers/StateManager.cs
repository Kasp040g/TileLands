using System.Collections.Generic;

namespace TileLands
{
    public enum ScreenStates
    {
        Splash,
        Menu,
        Game,
    }

    public static class StateManager
    {
        public static Dictionary<ScreenStates, State> States { get; } = new();

        public static void Init(GameManager gm)
        {
            States.Clear();
            States.Add(ScreenStates.Splash, new SplashState());
            States.Add(ScreenStates.Menu, new MenuState(gm));
            States.Add(ScreenStates.Game, new GameState(gm));


        }
    }
}
