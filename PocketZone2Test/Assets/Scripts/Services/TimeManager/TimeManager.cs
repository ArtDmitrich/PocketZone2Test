using UnityEngine;

namespace Services.TimeManager
{
    public static class TimeManager
    {
        public static void PauseGame()
        {
            Time.timeScale = 0.0f;
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1.0f;
        }
    }
}
