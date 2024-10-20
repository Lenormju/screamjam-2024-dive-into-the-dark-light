using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public event Action<Transform, float> OnNoise;

    //public AudioResource audioWalkOnStone1;  // unavailable
    public AudioResource audioWalkOnStone2;
    public AudioResource audioWalkOnStone3;
    public AudioResource audioWalkOnStone4;
    public AudioResource audioWalkOnBones1;
    public AudioResource audioWalkOnBones2;
    public AudioResource audioWalkOnBones3;
    public AudioResource audioWalkOnBones4;
    public AudioSource playerWalking;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    [SerializeField] private InputAction move;
    //[SerializeField] private InputAction run;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        move.Enable();
        // Lock cursor
        GameManager.Instance.SetCursorActive(false);
    }

    bool isRunning = false;
    float verticalInput = 0;
    float horizontalInput = 0;

    void Update()
    {
        // Handle Input
        //if (move.triggered)
        //{
        //    Debug.Log($"Action triggered!" + move.ReadValue<Vector2>());
            
        //}
        verticalInput = move.ReadValue<Vector2>().y;
        horizontalInput = move.ReadValue<Vector2>().x;

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run (disable for now, we dont use it from a game design perspective
        //isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * verticalInput /*Input.GetAxis("Vertical")*/ : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * horizontalInput /*Input.GetAxis("Horizontal")*/ : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // on va raycaster depuis le personnage vers le bas, et voir si on hit un ground
        Vector3 origin = this.transform.position;
        Vector3 direction = Vector3.down;

        RaycastHit hit;
        bool hasHitSomething = Physics.Raycast(origin, direction, out hit);

        bool isGroundBoneHit = false;  // by default
        bool isGroundStoneHit = false; // by default
        bool isNoisy = false;  // by default
        float noiseVolume = 0.0F;  // by default
        string groundCategory = "none by default";  // by default
        if (hasHitSomething) {
            //Debug.Log("hit something: " + hit.transform + " " + hit.point);
            if (hit.transform.tag == "Bones") {
                //Debug.Log("hit groundBone");
                isGroundBoneHit = true;
                noiseVolume = 0.8F;
                isNoisy = true;
                groundCategory = hit.transform.tag;
            } else if (hit.transform.tag == "Stone") {
                //Debug.Log("hit groundStone");
                isGroundStoneHit = true;
                noiseVolume = 0.2F;
                isNoisy = true;
                groundCategory = hit.transform.tag;
            } else {
                //Debug.Log("hit something else than groundBone/groundStone");
            }
        } else {
            //Debug.Log("pas hit");
        }

        Vector3 moveDirectionWithoutY = new Vector3(moveDirection.x, 0, moveDirection.z);
        bool isActuallyMoving = !(characterController.isGrounded && (moveDirectionWithoutY == Vector3.zero));

        // si on marche sur quelque chose de bruyant, alors on envoie le son aux ennemis
        if (isNoisy) {
            //Debug.Log(moveDirection);
            if (!isActuallyMoving) {
                // immobile
            } else {
                //Debug.Log("crounch");
                OnNoise?.Invoke(hit.transform, noiseVolume);
            }
        } else {
            // rien
        }

        System.Random rnd = new System.Random();
        // sélectionner le bruit à jouer
        AudioResource audioToPlay = null;  // by default
        if (isGroundBoneHit) {
            int sound  = rnd.Next(1, 5);  
            if (sound == 1) {
                audioToPlay = audioWalkOnBones1;
            } else if (sound == 2) {
                audioToPlay = audioWalkOnBones2;
            } else if (sound == 3) {
                audioToPlay = audioWalkOnBones3;
            } else if (sound == 4) {
                audioToPlay = audioWalkOnBones4;
            } else {
                Debug.Log("error value from random");
            }
        } else if (isGroundStoneHit) {
            int sound  = rnd.Next(1, 4);  
            if (sound == 1) {
                audioToPlay = audioWalkOnStone2;
            } else if (sound == 2) {
                audioToPlay = audioWalkOnStone3;
            } else if (sound == 3) {
                audioToPlay = audioWalkOnStone4;
            } else {
                Debug.Log("error value from random");
            }
        } else {
            // pas de ground bruyant, pas de bruit
        }
        //Debug.Log("isNoisy=" + isNoisy + " isActuallyMoving=" + isActuallyMoving + " isPlaying=" + playerWalking.isPlaying);
        if (isNoisy && isActuallyMoving) {
            string currentGroundWalkingCategory = GameManager.Instance.currentGroundWalkingCategory;
            if (playerWalking.isPlaying && (currentGroundWalkingCategory == groundCategory)) {
                //Debug.Log("keep playing");
            } else {
                playerWalking.resource = audioToPlay;
                GameManager.Instance.currentGroundWalkingCategory = groundCategory;
                playerWalking.Play();
                //Debug.Log("play " + audioToPlay);
            }
        } else {
            playerWalking.Stop();
            //Debug.Log("stop");
        }
    }

}
