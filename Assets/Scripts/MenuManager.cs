using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Si BikeData est dans un namespace différent, ajoutez la référence ici
// using VotreNamespace;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene"; // Nom par défaut

    [Header("Motos")]
    [SerializeField] private BikeData[] bikes;
    [SerializeField] private Transform bikeDisplayPosition;
    [SerializeField] private Button previousBikeButton;
    [SerializeField] private Button nextBikeButton;
    [SerializeField] private Text bikeNameText;
    
    [Header("Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [Header("Player Info")]
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private string defaultPlayerName = "Joueur Inconnu";

    private int currentBikeIndex = 0;
    private GameObject currentBikeModel;

    // Tableau des couleurs pour le texte
    private readonly Color[] bikeColors = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green
    };

    private void Awake()
    {
        // Créer des motos temporaires
        CreateTemporaryBikes();
    }

    private void Start()
    {
        ValidateReferences();
        SetupButtonListeners();
        CreateTemporaryBikes();
        DisplayCurrentBike();
        SetupPlayerNameInput();
    }

    private void ValidateReferences()
    {
        if (bikeDisplayPosition == null)
        {
            Debug.LogError("BikeDisplayPosition n'est pas assigné!");
            return;
        }
        if (previousBikeButton == null)
        {
            Debug.LogError("PreviousButton n'est pas assigné!");
            return;
        }
        if (nextBikeButton == null)
        {
            Debug.LogError("NextButton n'est pas assigné!");
            return;
        }
        if (bikeNameText == null)
        {
            Debug.LogError("BikeNameText n'est pas assigné!");
            return;
        }
        if (playButton == null)
        {
            Debug.LogError("PlayButton n'est pas assigné!");
            return;
        }
        if (quitButton == null)
        {
            Debug.LogError("QuitButton n'est pas assigné!");
            return;
        }
        if (playerNameInput == null)
        {
            Debug.LogError("PlayerNameInput n'est pas assigné!");
            return;
        }
    }

    private void SetupButtonListeners()
    {
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        previousBikeButton.onClick.AddListener(ShowPreviousBike);
        nextBikeButton.onClick.AddListener(ShowNextBike);
    }

    private void SetupPlayerNameInput()
    {
        string savedName = PlayerPrefs.GetString("PlayerName", defaultPlayerName);
        playerNameInput.text = savedName;

        playerNameInput.onEndEdit.AddListener(OnPlayerNameChanged);
    }

    private void OnPlayerNameChanged(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            playerNameInput.text = defaultPlayerName;
            newName = defaultPlayerName;
        }

        PlayerPrefs.SetString("PlayerName", newName);
        PlayerPrefs.Save();
    }

    private void DisplayCurrentBike()
    {
        // Détruire le modèle précédent s'il existe
        if (currentBikeModel != null)
        {
            Destroy(currentBikeModel);
        }

        // Instancier le nouveau modèle
        currentBikeModel = Instantiate(bikes[currentBikeIndex].bikeModel, 
            bikeDisplayPosition.position, 
            bikeDisplayPosition.rotation);
        
        // S'assurer que le modèle est actif
        currentBikeModel.SetActive(true);
        
        // Mise à jour du texte et de sa couleur
        if (bikeNameText != null)
        {
            bikeNameText.text = bikes[currentBikeIndex].bikeName;
            bikeNameText.color = bikeColors[currentBikeIndex];
        }
    }

    private void ShowNextBike()
    {
        currentBikeIndex = (currentBikeIndex + 1) % bikes.Length;
        DisplayCurrentBike();
    }

    private void ShowPreviousBike()
    {
        currentBikeIndex = (currentBikeIndex - 1 + bikes.Length) % bikes.Length;
        DisplayCurrentBike();
    }

    private void StartGame()
    {
        string playerName = playerNameInput.text;
        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = defaultPlayerName;
            playerNameInput.text = playerName;
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("SelectedBikeIndex", currentBikeIndex);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(gameSceneName);
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void Update()
    {
        if (currentBikeModel != null)
        {
            currentBikeModel.transform.Rotate(0, 30 * Time.deltaTime, 0); // Rotation continue
        }
    }

    private void CreateTemporaryBikes()
    {
        // Si les bikes sont déjà définis, ne pas les recréer
        if (bikes != null && bikes.Length > 0 && bikes[0].bikeModel != null)
            return;

        bikes = new BikeData[3];
        string[] names = new string[] { "Moto Rouge", "Moto Bleue", "Moto Verte" };

        for (int i = 0; i < 3; i++)
        {
            bikes[i] = new BikeData();
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            cube.transform.localScale = new Vector3(2f, 1f, 4f);
            
            Material material = new Material(Shader.Find("Standard"));
            material.color = bikeColors[i];
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", bikeColors[i] * 0.5f);
            cube.GetComponent<Renderer>().material = material;
            
            bikes[i].bikeModel = cube;
            bikes[i].bikeName = names[i];
            
            cube.SetActive(false);
        }
    }

    private void OnValidate()
    {
        // Vérification dans l'éditeur
        if (bikeDisplayPosition == null)
            Debug.LogWarning("BikeDisplayPosition manquant! Assignez le BikeDisplayArea.");
        if (previousBikeButton == null)
            Debug.LogWarning("PreviousButton manquant!");
        if (nextBikeButton == null)
            Debug.LogWarning("NextButton manquant!");
        if (bikeNameText == null)
            Debug.LogWarning("BikeNameText manquant!");
        if (playButton == null)
            Debug.LogWarning("PlayButton manquant!");
        if (quitButton == null)
            Debug.LogWarning("QuitButton manquant!");
    }
} 