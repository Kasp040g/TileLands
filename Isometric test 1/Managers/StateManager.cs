using System.Collections.Generic;

namespace TileLands
{
    public enum ScreenStates
    {
        Splash,
        Menu,
        Game,
    }

    /// <summary>
    /// Controls the current state of the game and initialize states in a dictionary
    /// </summary>
    public static class StateManager
    {
        public static Dictionary<ScreenStates, State> States { get; } = new();

        /// <summary>
        /// Init is called in gamemanager
        /// </summary>
        /// <param name="gm"></param>
        public static void Init(GameManager gm)
        {
            States.Clear();
            States.Add(ScreenStates.Splash, new SplashState());
            States.Add(ScreenStates.Menu, new MenuState(gm));
            //States.Add(ScreenStates.Game, new GameState(gm));
        }
    }
}
