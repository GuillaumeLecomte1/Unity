using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private int currentBikeIndex = 0;
    private GameObject currentBikeModel;

    private void Awake()
    {
        // Créer des motos temporaires
        CreateTemporaryBikes();
    }

    private void Start()
    {
        // Vérification des références
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

        // Ajouter les listeners aux boutons
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        previousBikeButton.onClick.AddListener(ShowPreviousBike);
        nextBikeButton.onClick.AddListener(ShowNextBike);

        // Créer des motos temporaires si nécessaire
        CreateTemporaryBikes();

        // Afficher la première moto
        DisplayCurrentBike();
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
        
        // Mettre à jour le nom de la moto
        bikeNameText.text = bikes[currentBikeIndex].bikeName;
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
        // Sauvegarder l'index de la moto sélectionnée
        PlayerPrefs.SetInt("SelectedBikeIndex", currentBikeIndex);
        PlayerPrefs.Save();
        
        // Charger la scène de jeu
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
        for (int i = 0; i < 3; i++)
        {
            bikes[i] = new BikeData();
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            // Définir la taille du cube
            cube.transform.localScale = new Vector3(2f, 1f, 4f); // Forme plus proche d'une moto
            
            // Définir la couleur
            Material material = new Material(Shader.Find("Standard"));
            material.color = i == 0 ? Color.red : i == 1 ? Color.blue : Color.green;
            cube.GetComponent<Renderer>().material = material;
            
            bikes[i].bikeModel = cube;
            bikes[i].bikeName = i == 0 ? "Moto Rouge" : i == 1 ? "Moto Bleue" : "Moto Verte";
            
            // Cacher le cube initialement
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