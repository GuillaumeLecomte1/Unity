using UnityEngine;
using System.Collections.Generic;

public class RandomStartEndPoints : MonoBehaviour
{
    public static RandomStartEndPoints instance;

    public Transform[] startPoints;
    public Transform[] endPoints;
    public GameObject player; // référence au joueur
    public GameObject endMarker;

    public Transform CurrentStartPoint;
    public Transform CurrentEndPoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Choix aléatoire d'un point de fin commun
        CurrentEndPoint = endPoints[Random.Range(0, endPoints.Length)];
        if (endMarker != null)
        {
            endMarker.transform.position = CurrentEndPoint.position;
        }

        // Récupération de toutes les motos (enfants de "Motos")
        GameObject motosContainer = GameObject.Find("Motos");
        if (motosContainer == null)
        {
            Debug.LogError("Aucun GameObject 'Motos' trouvé dans la scène.");
            return;
        }

        List<Transform> availableStarts = new List<Transform>(startPoints);

        foreach (Transform moto in motosContainer.transform)
        {
            if (availableStarts.Count == 0)
            {
                Debug.LogWarning("Pas assez de points de départ pour toutes les motos !");
                break;
            }

            // Choix d’un point de départ unique aléatoire
            int randomIndex = Random.Range(0, availableStarts.Count);
            Transform chosenStart = availableStarts[randomIndex];
            availableStarts.RemoveAt(randomIndex);

            // Déplacement de la moto
            Rigidbody rb = moto.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = chosenStart.position;
                rb.rotation = chosenStart.rotation;
                rb.isKinematic = false;
            }
            else
            {
                moto.position = chosenStart.position;
                moto.rotation = chosenStart.rotation;
            }

            // Si c’est le joueur, on mémorise le point de départ
            if (moto.CompareTag("Player"))
            {
                player = moto.gameObject;
                CurrentStartPoint = chosenStart;
            }

            // Si la moto a une IA, on lui assigne la destination
            SimpleIA ia = moto.GetComponent<SimpleIA>();
            if (ia != null)
            {
                ia.SetEndPoint(CurrentEndPoint);
            }
        }
    }
}