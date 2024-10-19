using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public event Action<Transform> OnNoise;

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

        // Est-ce que le Player marche sur de la boue/os ?
        GameObject ground_bruyant = GameObject.Find("/Ground Bruyant");
        GameObject ground_pas_bruyant = GameObject.Find("/Ground Pas Bruyant");
        bool is_there_a_ground_pas_bruyant = (ground_bruyant != null);
        if (is_there_a_ground_pas_bruyant) {
            // on va raycaster depuis le personnage vers le bas, et voir si on hit le "ground bruyant"
            Vector3 origin = this.transform.position;
            Vector3 direction = Vector3.down;

            RaycastHit hit;
            bool has_hit_something = Physics.Raycast(origin, direction, out hit);

            bool is_ground_bruyant_hit = false;  // by default
            if (has_hit_something) {
                //Debug.Log("hit something: " + hit.transform + " " + hit.point);
                if (hit.transform == ground_bruyant.transform) {
                    //Debug.Log("hit ground_bruyant");
                    is_ground_bruyant_hit = true;
                } else {
                    //Debug.Log("hit something else than ground_bruyant");
                }
            } else {
                //Debug.Log("pas hit");
            }

            if (is_ground_bruyant_hit) {
                //Debug.Log(moveDirection);
                Vector3 moveDirectionWithoutY = new Vector3(moveDirection.x, 0, moveDirection.z);
                if (characterController.isGrounded && (moveDirectionWithoutY == Vector3.zero)) {
                    // immobile
                } else {
                    //Debug.Log("crounch");
                    OnNoise?.Invoke(hit.transform);
                }
            } else {
                // rien
            }
        } else {
            // osef
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
