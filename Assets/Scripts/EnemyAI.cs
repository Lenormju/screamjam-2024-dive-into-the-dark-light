using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private bool isPatrolling = false;
    private int currentPatrolPoint = 1;
    private Vector3 patrollingPos1;
    [SerializeField] private Vector3 patrollingPos2;
    private SphereCollider scentCollider;
    [SerializeField] private float killRange = 3f;
    [SerializeField] private SphereCollider killCollider;

    private NavMeshAgent _agent;
    private bool chasingPlayer = false;
    void Start() {
        patrollingPos1 = this.transform.position;
        _agent = GetComponent<NavMeshAgent>();
        scentCollider = GetComponent<SphereCollider>();
        killCollider.radius = killRange;
        scentCollider.radius = scentRange;
        GameManager.Instance.Player.OnNoise += onNoiseEmitted;
    }
    void Update() {
        if (!_agent.isOnNavMesh) return;

        _timerWaitOnPlace -= Time.deltaTime;
        if (!hasTarget && _timerWaitOnPlace < 0) {
            chasingPlayer = false;
            hasTarget = false;
        }

        SetTarget();
    }

    void SetTarget() {
        if (_timerWaitOnPlace > 0) { // Wait if lost player
            _agent.ResetPath();
            return;
        }

        if (chasingPlayer) { // Chasing player
            Debug.Log("Target player");
            _agent.destination = GameManager.Instance.Player.transform.position;
        } else if (hasTarget && !chasingPlayer) { // Chasing noise
            if (soundTarget != null && hasTarget) {
                Debug.Log("Target sound");
                _agent.destination = soundTarget.position;
                if (IsCloseEnough(transform.position, soundTarget.position)) {
                    LostTarget();
                    soundTarget = null;
                    _agent.ResetPath();
                }
            }
        } else if (isPatrolling) { // Default patrolling
            HandlingSwitchPatrolPoint();
            Debug.Log("Patrolling to " + currentPatrolPoint);
            if (currentPatrolPoint == 1) {
                _agent.destination = patrollingPos1;
            } else {
                _agent.destination = patrollingPos1 + patrollingPos2;
            }
        }
    }
    void OnDestroy() {
        GameManager.Instance.Player.OnNoise -= onNoiseEmitted;
    }

    void HandlingSwitchPatrolPoint() {
        if (IsCloseEnough(transform.position, patrollingPos1)) {
            currentPatrolPoint = 2;
        } 
        if (IsCloseEnough(transform.position, patrollingPos1 + patrollingPos2)) {
            currentPatrolPoint = 1;
        }
    }

    [SerializeField] private float distCloseEnough = 1f;
    [SerializeField] private float distCloseEnoughSound = 10f;
    bool IsCloseEnough(Vector3 pos1, Vector3 pos2) {
        pos1.y = 0;
        pos2.y = 0;
        return Vector3.Distance(pos1, pos2) < distCloseEnough;
    }

    private Transform soundTarget = null;
    void onNoiseEmitted(Transform noise, float intensity) {
        float hearRange = intensity * distCloseEnoughSound * 5;
        if (Vector3.Distance(this.transform.position, noise.position) < hearRange)
        {
            Debug.Log("SOUND DISTANCE");
            hasTarget = true;
            _timerWaitOnPlace = 0;
            soundTarget = noise;
        }
    }

    [SerializeField] private float scentRange = 5f;
    [SerializeField] private float timeWaitOnPlaceWhenLostTarget = 3f;
    private float _timerWaitOnPlace = 0;
    private bool hasTarget = false;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            chasingPlayer = true;
            hasTarget = true;
            _timerWaitOnPlace = 0;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Lost" + other.tag);
            LostTarget();
        }
    }

    private void LostTarget() {
        hasTarget = false;
        chasingPlayer = false;
        _timerWaitOnPlace = timeWaitOnPlaceWhenLostTarget;
    }
}
