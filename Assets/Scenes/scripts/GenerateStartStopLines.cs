using System.Collections.Generic;
using UnityEngine;

public class GenerateStartStopLines : MonoBehaviour
{
    public GameObject spawnPointPrefab;
    public GameObject endPointPrefab;

    private List<Vector3> predefinedPositions = new List<Vector3> {
        new Vector3(66.226f, 1.934f, -6.571f),
        new Vector3(65.114f, 0.998f, -9.678f),
        new Vector3(70.667f, 0.998f, -4.743f),
    };

    private Vector3 startPoint;
    private Vector3 endPoint;

    void Start()
    {
        if (predefinedPositions.Count < 2)
        {
            Debug.LogError("Pas assez de positions pour choisir un start et un end.");
            return;
        }

        int firstIndex = Random.Range(0, predefinedPositions.Count);
        int secondIndex;

        do
        {
            secondIndex = Random.Range(0, predefinedPositions.Count);
        } while (secondIndex == firstIndex);

        startPoint = predefinedPositions[firstIndex];
        endPoint = predefinedPositions[secondIndex];

        Instantiate(spawnPointPrefab, startPoint, Quaternion.identity);
        Instantiate(endPointPrefab, endPoint, Quaternion.identity);

        Debug.Log($"[START] Coordonnées choisies : {startPoint}");
        Debug.Log($"[END] Coordonnées choisies : {endPoint}");

        Camera.main.transform.position = startPoint;
        Camera.main.transform.LookAt(startPoint);
    }
}
