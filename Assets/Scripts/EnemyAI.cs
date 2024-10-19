using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private bool isPatrolling = false;
    private int currentPatrolPoint = 1;
    [SerializeField] private Vector3 patrollingPos1;
    [SerializeField] private Vector3 patrollingPos2;

    private NavMeshAgent _agent;
    private bool hasDetectedPlayer = true;
    void Start() {
        _agent = GetComponent<NavMeshAgent>();
        //GameManager.Instance.Player.OnNoise += onNoiseEmitted;
    }
    void Update() {
        if (!_agent.isOnNavMesh) return; 

        if (hasDetectedPlayer) {
            _agent.destination = GameManager.Instance.Player.transform.position;
        } else if (isPatrolling) {
            HandlingSwitchPatrolPoint();

            if (currentPatrolPoint == 1) {
                _agent.destination = patrollingPos1;
            } else {
                _agent.destination = patrollingPos2;
            }
        }
    }
    void OnDestroy() {
        //GameManager.Instance.Player.OnNoise -= onNoiseEmitted;
    }

    void HandlingSwitchPatrolPoint() {
        if (IsCloseEnough(transform.position, patrollingPos1)) {
            currentPatrolPoint = 2;
        } 
        if (IsCloseEnough(transform.position, patrollingPos2)) {
            currentPatrolPoint = 1;
        }
    }

    [SerializeField] private float distCloseEnough = 1f;
    bool IsCloseEnough(Vector3 pos1, Vector3 pos2) {
        pos1.y = 0;
        pos2.y = 0;
        return Vector3.Distance(pos1, pos2) < distCloseEnough;
    }

    void onNoiseEmitted(Transform noise, float intensity) {
        Debug.Log("noise received: " + noise);
    }

}
