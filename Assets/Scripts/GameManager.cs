using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    public int playerScore; // Pontuação do jogador principal
    public int aiScore; // Pontuação da IA ou Player 2
    public TextMeshProUGUI playerScoreText; // Texto da pontuação do jogador
    public TextMeshProUGUI aiScoreText; // Texto da pontuação da IA ou Player 2

    [Header("Canvas")]
    public GameObject mainMenuCanvas; // Canvas do menu principal
    public GameObject gameplayCanvas; // Canvas com HUD do gameplay
    public GameObject winGameCanvas; // Canvas de vitória
    public GameObject gameOverCanvas; // Canvas de derrota

    [Header("Gameplay Objects")]
    public GameObject gameplayRoot; // Objeto pai de todos os elementos jogáveis
    public GameObject paddleAI; // Paddle da IA
    public GameObject paddlePlayer2; // Paddle do Player 2

    [Header("Buttons")]
    public Button restartButtonWin; // Botão de restart da tela de vitória
    public Button restartButtonLose; // Botão de restart da tela de derrota

    [Header("Audio")]
    public AudioSource musicSource; // Música de fundo
    public AudioSource audioSource; // Fonte de áudio dos efeitos
    public AudioClip scoreSound; // Som ao marcar ponto

    private bool gameEnded = false; // Impede pontuação/finalização duplicada

    private void Start()
    {
        Time.timeScale = 1f; // Garante que o jogo começa despausado

        gameplayCanvas.SetActive(false); // Esconde HUD do gameplay no início
        winGameCanvas.SetActive(false); // Esconde tela de vitória no início
        gameOverCanvas.SetActive(false); // Esconde tela de derrota no início

        gameplayRoot.SetActive(false); // Impede o jogo de iniciar antes do menu
        paddleAI.SetActive(false); // IA começa desativada até escolher modo
        paddlePlayer2.SetActive(false); // Player 2 começa desativado até escolher modo

        restartButtonWin.onClick.AddListener(RestartGame); // Liga botão de vitória ao restart
        restartButtonLose.onClick.AddListener(RestartGame); // Liga botão de derrota ao restart
    }

    public void StartGame()
    {
        mainMenuCanvas.SetActive(false); // Esconde o menu principal
        gameplayCanvas.SetActive(true); // Mostra HUD do gameplay

        playerScoreText.gameObject.SetActive(true); // Mostra score do Player 1
        aiScoreText.gameObject.SetActive(true); // Mostra score da IA ou Player 2

        paddleAI.SetActive(GameMode.SelectedMode == GameMode.Mode.PlayerVsAI); // Ativa IA se modo for contra IA
        paddlePlayer2.SetActive(GameMode.SelectedMode == GameMode.Mode.PlayerVsPlayer); // Ativa Player 2 se modo for PvP
        gameplayRoot.SetActive(true); // Ativa os elementos jogáveis
    }

    public void PlayerPoint()
    {
        if (gameEnded) return; // Impede ponto após fim de jogo

        JuiceText(playerScoreText); // Anima texto do Player 1
        playerScore++; // Soma ponto ao Player 1
        playerScoreText.text = playerScore.ToString(); // Atualiza texto
        EndGame(); // Verifica fim de jogo
    }

    public void AIPoint()
    {
        if (gameEnded) return; // Impede ponto após fim de jogo

        JuiceText(aiScoreText); // Anima texto da IA/Player 2
        aiScore++; // Soma ponto à IA/Player 2
        aiScoreText.text = aiScore.ToString(); // Atualiza texto
        EndGame(); // Verifica fim de jogo
    }

    private void JuiceText(TextMeshProUGUI text)
    {
        audioSource.PlayOneShot(scoreSound); // Toca som de ponto
        text.transform.DOKill(); // Cancela animações antigas do texto
        text.transform.localScale = Vector3.one; // Reseta escala do texto
        text.transform.DOScale(1.5f, 0.2f).SetLoops(2, LoopType.Yoyo); // Faz efeito de pulse
        text.DOColor(Color.yellow, 0.5f).SetLoops(2, LoopType.Yoyo); // Pisca o texto em amarelo
    }

    private void EndGame()
    {
        if (gameEnded) return; // Evita finalizar mais de uma vez

        if (playerScore >= 5)
        {
            gameEnded = true; // Marca jogo como finalizado
            StartCoroutine(EndSequenceWin()); // Inicia sequência de vitória
        }
        else if (aiScore >= 5)
        {
            gameEnded = true; // Marca jogo como finalizado
            StartCoroutine(EndSequenceLose()); // Inicia sequência de derrota
        }
    }

    private IEnumerator EndSequenceWin()
    {
        yield return new WaitForSeconds(0.5f); // Espera animação do score terminar
        yield return StartCoroutine(FadeOutMusic(0.5f)); // Faz fade out da música

        winGameCanvas.SetActive(true); // Mostra tela de vitória
        Time.timeScale = 0f; // Pausa o jogo
    }

    private IEnumerator EndSequenceLose()
    {
        yield return new WaitForSeconds(0.5f); // Espera animação do score terminar
        yield return StartCoroutine(FadeOutMusic(0.5f)); // Faz fade out da música

        gameOverCanvas.SetActive(true); // Mostra tela de derrota
        Time.timeScale = 0f; // Pausa o jogo
    }

    private IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume; // Guarda volume inicial

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration; // Reduz volume gradualmente
            yield return null; // Espera próximo frame
        }

        musicSource.Stop(); // Para música
        musicSource.volume = startVolume; // Restaura volume para próximo jogo
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Despausa o jogo
        DOTween.KillAll(); // Mata animações DOTween ativas
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarrega cena atual
    }
}