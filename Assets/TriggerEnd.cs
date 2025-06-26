using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TriggerEnd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static int positionCounter = 1;
    private static List<GameObject> alreadyFinished = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        GameObject racer = other.gameObject;

        // Vérifie que ce coureur n’est pas déjà arrivé
        if (alreadyFinished.Contains(racer))
            return;

        alreadyFinished.Add(racer);

        string racerType = racer.CompareTag("Player") ? "Joueur" : "IA";
        int position = positionCounter++;

        Debug.Log($"{racerType} '{racer.name}' a terminé en position {position} !");
        if (racer.CompareTag("Player"))
        {
            // Par exemple : charger une scène de fin
            SceneManager.LoadScene("EndRaceScene"); // <-- Remplace "SceneFin" par le nom réel
        }
        // Tu peux aussi appeler un UIManager ici pour afficher sur l'écran
    }
}
