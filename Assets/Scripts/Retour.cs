using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Retour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string sceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Aucun nom de scène renseigné dans SceneLoaderOnClick.");
        }
    }
}