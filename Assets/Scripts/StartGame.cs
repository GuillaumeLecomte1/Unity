using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class StartGame : MonoBehaviour
{
    [Header("Nom de la scène à charger")]
    public string sceneName = "scene Jules";

    [Header("Audio optionnel au clic")]
    public AudioSource clickSound;

    private Button startButton;
    private bool hasLaunched = false;

    void Awake()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(OnStartClicked);
    }

    void OnStartClicked()
    {
        if (hasLaunched) return; // Empêche le double-clic

        hasLaunched = true;

        if (clickSound != null)
            clickSound.Play();

        // Délai pour laisser le son jouer, sinon charger direct
        float delay = (clickSound != null) ? clickSound.clip.length : 0f;
        Invoke(nameof(LoadGameScene), delay);
    }

    void LoadGameScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneName);
    }
}