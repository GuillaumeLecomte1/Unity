using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleIA : MonoBehaviour
{
    public Transform pointB; // Définissable dans l’inspecteur
    public float speedVariator = 1.0f; // Nouvelle variable pour la vitesse

    private NavMeshAgent agent;

    public void SetEndPoint(Transform endpoint)
    {
        pointB = endpoint;
        agent = GetComponent<NavMeshAgent>();

        if (pointB != null && agent.isOnNavMesh)
        {
            agent.speed = agent.speed * speedVariator; // Applique la variation de vitesse
            agent.SetDestination(pointB.position);
        }
        else
        {
            Debug.LogWarning("Point B non défini ou NavMeshAgent non actif sur le NavMesh.");
        }
    }
}