using UnityEngine;

namespace DSS
{
    public static class GameController
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadGame()
        {
            Application.targetFrameRate = 60;
        }
    }
}