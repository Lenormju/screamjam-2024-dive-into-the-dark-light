using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;
    private NavMeshAgent _agent;
    
    void Start() {
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update() {
        if (_agent.isOnNavMesh) {
            _agent.destination = _playerPos.position;
        } else {
            _agent.destination = this.transform.position;
        }
    }
}
