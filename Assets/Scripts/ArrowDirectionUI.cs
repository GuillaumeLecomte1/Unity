using UnityEngine;
using UnityEngine.UI;

public class ArrowDirectionUI : MonoBehaviour
{
    public Transform player;       // La moto
    public Transform target;       // L’arche d’arrivée
    public RectTransform arrowUI;  // L’image de flèche dans l'UI

    void Update()
    {
        if (!player || !target || !arrowUI) return;

        // Direction depuis le joueur vers la cible
        Vector3 dirToTarget = (target.position - player.position).normalized;

        // Projection dans l’espace caméra (espace local à la caméra)
        Vector3 camRelativeDir = Camera.main.transform.InverseTransformDirection(dirToTarget);

        // Calcul de l’angle dans le plan écran (2D)
        float angle = Mathf.Atan2(camRelativeDir.x, camRelativeDir.z) * Mathf.Rad2Deg;

        // Appliquer la rotation sur l’axe Z
        arrowUI.rotation = Quaternion.Euler(0, 0, -angle);
    }
}