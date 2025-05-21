using UnityEngine;
using KikiNgao.SimpleBikeControl;

public class BikeController : MonoBehaviour
{
    // Référence au script SimpleBike
    public SimpleBike simpleBike;

    // Vitesse maximale d'accélération
    public float maxAcceleration = 20f;

    // Vitesse d'incrémentation
    public float accelerationRate = 2f;

    void Update()
    {
        // S'assurer que la référence est bien assignée
        if (simpleBike != null)
        {
            if (simpleBike.powerUpSpeed < maxAcceleration)
            {
                simpleBike.powerUpSpeed += accelerationRate * Time.deltaTime;
                simpleBike.powerUpSpeed = Mathf.Min(simpleBike.powerUpSpeed, maxAcceleration);
            }
        }
        else
        {
            Debug.LogWarning("Référence à SimpleBike non assignée dans BikeController.");
        }
    }
}