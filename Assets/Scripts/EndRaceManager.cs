using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndRaceManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI raceTimeText;
    [SerializeField] private TextMeshProUGUI congratulationsText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        // Récupérer les informations de la course
        string playerName = PlayerPrefs.GetString("PlayerName", "Joueur Inconnu");
        float raceTime = PlayerPrefs.GetFloat("RaceTime", 0f);

        // Afficher les informations
        if (playerNameText != null)
            playerNameText.text = $"Pilote : {playerName}";

        if (raceTimeText != null)
            raceTimeText.text = $"Temps : {FormatTime(raceTime)}";

        if (congratulationsText != null)
            congratulationsText.text = "Course Terminée !";

        // Configuration des boutons
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartRace);

        if (menuButton != null)
            menuButton.onClick.AddListener(ReturnToMenu);
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        int milliseconds = (int)((timeInSeconds * 100) % 100);
        return $"{minutes:00}:{seconds:00}.{milliseconds:00}";
    }

    private void RestartRace()
    {
        SceneManager.LoadScene("GameScene"); // Remplacez par le nom de votre scène de course
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu"); // Remplacez par le nom de votre scène de menu
    }
} 