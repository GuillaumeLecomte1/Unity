using UnityEngine;
using UnityEngine.UI;

public class SetupUI : MonoBehaviour
{
    [SerializeField] private Button previousBikeButton;
    [SerializeField] private Button nextBikeButton;
    [SerializeField] private Text bikeNameText;

    void SetupUIPositions()
    {
        // BikeDisplayArea au centre
        RectTransform displayArea = GetComponent<RectTransform>();
        displayArea.anchoredPosition = Vector2.zero;
        
        // Boutons Previous/Next
        RectTransform prevButton = previousBikeButton.GetComponent<RectTransform>();
        prevButton.anchoredPosition = new Vector2(-200, 0);
        
        RectTransform nextButton = nextBikeButton.GetComponent<RectTransform>();
        nextButton.anchoredPosition = new Vector2(200, 0);
        
        // Nom de la moto en haut
        RectTransform nameText = bikeNameText.GetComponent<RectTransform>();
        nameText.anchoredPosition = new Vector2(0, 100);
    }
} 