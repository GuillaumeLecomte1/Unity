using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BikeData[] bikes;

    void Start()
    {
        int selectedBikeIndex = PlayerPrefs.GetInt("SelectedBikeIndex", 0);
        // Instancier la moto sélectionnée
        Instantiate(bikes[selectedBikeIndex].bikeModel, transform.position, transform.rotation);
    }
} 