using UnityEngine;
using System;
using UnityEngine.Audio;

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

    public AudioResource audioWalkOnStone;
    public AudioResource audioWalkOnBones;
    public AudioSource playerWalking;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
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
        if (hasHitSomething) {
            //Debug.Log("hit something: " + hit.transform + " " + hit.point);
            if (hit.transform.tag == "Bones") {
                //Debug.Log("hit groundBone");
                isGroundBoneHit = true;
                noiseVolume = 0.8F;
                isNoisy = true;
            } else if (hit.transform.tag == "Stone") {
                //Debug.Log("hit groundStone");
                isGroundStoneHit = true;
                noiseVolume = 0.2F;
                isNoisy = true;
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

        // si on marche, alors on entend du brouit dans les haut-parleurs
        AudioResource audioToPlay = null;  // by default
        if (isGroundBoneHit) {
            audioToPlay = audioWalkOnBones;
        } else if (isGroundStoneHit) {
            audioToPlay = audioWalkOnStone;
        } else {
            // pas de ground bruyant, pas de bruit
        }
        //Debug.Log("isNoisy=" + isNoisy + " isActuallyMoving=" + isActuallyMoving + " isPlaying=" + playerWalking.isPlaying);
        if (playerWalking == null) return;
        if (isNoisy && isActuallyMoving) {
            if (playerWalking.isPlaying) {
                // keep playing
                // TODO: changer de son si on change de ground
                //Debug.Log("already playing");
            } else {
                playerWalking.resource = audioToPlay;
                playerWalking.Play();
                //Debug.Log("play");
            }
        } else {
            playerWalking.Stop();
            //Debug.Log("stop");
        }
    }



    //public float SpeedPlayer = 3;
    //public float SensibilityCamera = 5;
    //private Rigidbody _rb;

    //// Use this for initialization
    //void Start()
    //{
    //    //Set Cursor to not be visible
    //    Cursor.lockState = CursorLockMode.Locked;
    //    _rb = GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3(x, 0, z);
    //    transform.Translate(movement.normalized * SpeedPlayer * Time.deltaTime);
    //    //_rb.MovePosition(this.transform.position + movement.normalized * SpeedPlayer * Time.deltaTime);

    //    float horizontalCam = SensibilityCamera * Input.GetAxis("Mouse X");
    //    transform.Rotate(0, horizontalCam, 0);
    //}
}
