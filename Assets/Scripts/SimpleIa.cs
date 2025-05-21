using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleIA : MonoBehaviour
{
    public Transform pointB; // Définissable dans l’inspecteur

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (pointB != null && agent.isOnNavMesh)
        {
            agent.SetDestination(pointB.position);
        }
        else
        {
            Debug.LogWarning("Point B non défini ou NavMeshAgent non actif sur le NavMesh.");
        }
    }
}