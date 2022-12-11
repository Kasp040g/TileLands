using System;

namespace TileLands
{
    [Serializable]
    public class ScoreManager
    {
        public int LevelXDone { get; set; }
        public bool EndlessUnlocked { get; set; }
    }
}
