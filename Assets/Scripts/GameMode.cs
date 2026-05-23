using UnityEngine;

public class GameMode : MonoBehaviour
{
   
        public enum Mode
        {
            PlayerVsAI,
            PlayerVsPlayer
        }

        public static Mode SelectedMode = Mode.PlayerVsAI;
   
}
