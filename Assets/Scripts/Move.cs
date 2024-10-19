using UnityEngine;

public class Move : MonoBehaviour
{
    public float SpeedPlayer = 3;
    public float SensibilityCamera = 5;

    // Use this for initialization
    void Start()
    {
        //Set Cursor to not be visible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, z);
        transform.Translate(movement.normalized * SpeedPlayer * Time.deltaTime);

        float horizontalCam = SensibilityCamera * Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalCam, 0);
    }
}
