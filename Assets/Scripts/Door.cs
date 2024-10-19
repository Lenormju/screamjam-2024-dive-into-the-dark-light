using UnityEngine;

public class Door : MonoBehaviour
{
    private bool is_in_range = false;
    private bool is_door_open = false;
    private Animator animator;

    /// Start is called on the frame when a script is enabled just before
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_in_range &&Input.GetMouseButtonDown(0))
        {
            if (!is_door_open )
            {
                animator.SetTrigger("openingDoor");
                is_door_open = true;
            }
            else
            {
                animator.SetTrigger("closingDoor");
                is_door_open = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        is_in_range = true;
    }

    void OnTriggerExit(Collider other)
    {
        is_in_range = false;
    }
}
