using UnityEngine;
using UnityEngine.UI;

public class UISetup : MonoBehaviour
{
    public void SetupUIElements()
    {
        // Configurer le Canvas
        Canvas canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // BikeDisplayArea
        RectTransform bikeArea = transform.Find("BikeDisplayArea").GetComponent<RectTransform>();
        bikeArea.anchoredPosition = Vector2.zero;
        bikeArea.sizeDelta = new Vector2(400, 400);

        // Boutons
        ConfigureButton("PreviousButton", new Vector2(-300, 0), "<");
        ConfigureButton("NextButton", new Vector2(300, 0), ">");
        ConfigureButton("PlayButton", new Vector2(-100, -200), "JOUER");
        ConfigureButton("QuitButton", new Vector2(100, -200), "QUITTER");

        // Texte du nom de la moto
        ConfigureText("BikeNameText", new Vector2(0, 200), "SÉLECTIONNEZ VOTRE MOTO");
    }

    private void ConfigureButton(string name, Vector2 position, string text)
    {
        Transform buttonTransform = transform.Find(name);
        if (buttonTransform != null)
        {
            RectTransform rect = buttonTransform.GetComponent<RectTransform>();
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(160, 40);

            Text buttonText = buttonTransform.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = text;
                buttonText.fontSize = 24;
            }
        }
    }

    private void ConfigureText(string name, Vector2 position, string text)
    {
        Transform textTransform = transform.Find(name);
        if (textTransform != null)
        {
            RectTransform rect = textTransform.GetComponent<RectTransform>();
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(400, 50);

            Text uiText = textTransform.GetComponent<Text>();
            if (uiText != null)
            {
                uiText.text = text;
                uiText.fontSize = 36;
                uiText.alignment = TextAnchor.MiddleCenter;
            }
        }
    }
} 