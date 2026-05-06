using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerScore;
    public int aiScore;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    public GameObject winGameCanvas;
    public GameObject gameOverCanvas;
    public AudioSource musicSource;
    public Button restartButtonWin;
    public Button restartButtonLose;


    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip scoreSound;
    private bool gameEnded = false;
    public void PlayerPoint()
    {
        JuiceText(playerScoreText);
        playerScore++;
        playerScoreText.text = playerScore.ToString();
        EndGame();

    }
    

    public void AIPoint()
    {
        JuiceText(aiScoreText);
        aiScore++;
        aiScoreText.text = aiScore.ToString();
        EndGame();
        
    }

    void JuiceText(TextMeshProUGUI text)
    {
        audioSource.PlayOneShot(scoreSound);
        text.transform.DOKill();
        text.transform.localScale = Vector3.one;
        text.transform.DOScale(1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
        text.DOColor(Color.yellow, 0.5f).SetLoops(2, LoopType.Yoyo);
    }

    void EndGame()
    {
        if (gameEnded) return;

        if (playerScore >= 5)
        {
            gameEnded = true; // Impede que o jogo continue
            // Aguarda animações antes de finalizar tudo
            StartCoroutine(EndSequenceWin());

           
        }

        else if (aiScore >= 5)

        {
            gameEnded = true; // Impede que o jogo continue
            StartCoroutine(EndSequenceLose());
        }

    }

    IEnumerator EndSequenceWin()
    {
        // espera terminar os tweens (juice do texto)
        yield return new WaitForSeconds(0.5f);

        // fade out da música
        yield return StartCoroutine(FadeOutMusic(0.5f));

        gameEnded = true;

        winGameCanvas.SetActive(true);

        // agora sim pausa o jogo
        Time.timeScale = 0f;
    }

    IEnumerator EndSequenceLose()
    {
        // espera terminar os tweens (juice do texto)
        yield return new WaitForSeconds(0.5f);
        // fade out da música
        yield return StartCoroutine(FadeOutMusic(0.5f));
        gameEnded = true;

        gameOverCanvas.SetActive(true);
        // Mostra a tela de derrota
        // winGameCanvas.SetActive(true); // Substitua por uma tela de derrota se tiver
        // agora sim pausa o jogo
        Time.timeScale = 0f;
    }

    IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        DOTween.KillAll(); // mata tweens ativos
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        restartButtonWin.onClick.AddListener(RestartGame);
        restartButtonLose.onClick.AddListener(RestartGame);
    }
}

