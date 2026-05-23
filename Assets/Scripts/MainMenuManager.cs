using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager; // Referência ao GameManager da cena

    public void PlayVsAI()
    {
        GameMode.SelectedMode = GameMode.Mode.PlayerVsAI; // Define modo Player vs IA
        gameManager.StartGame(); // Solicita ao GameManager iniciar o jogo
    }

    public void PlayVsPlayer()
    {
        GameMode.SelectedMode = GameMode.Mode.PlayerVsPlayer; // Define modo Player vs Player
        gameManager.StartGame(); // Solicita ao GameManager iniciar o jogo
    }

    public void QuitGame()
    {
        Application.Quit(); // Fecha o jogo na build

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para o Play Mode dentro da Unity
#endif
    }
}