using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Slider PushSlider;
    [SerializeField] private float amountToOpen = 0;
    private float pushAmount = 0;
    [SerializeField] private float amountOnClick = 10;
    [SerializeField] private float losingOverTimeAmount = 10;


    [SerializeField] private int nb_keys_needed = 0;
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
        if (is_in_range && Input.GetMouseButtonDown(0))
        {
            if (!is_door_open)
            {
                pushAmount += amountOnClick;
                if (pushAmount >= amountToOpen && GameManager.Instance.nb_keys >= nb_keys_needed)
                {
                    animator.SetTrigger("openingDoor");
                    is_door_open = true;
                }
            }
            else if (pushAmount <= 0)
            {
                animator.SetTrigger("closingDoor");
                is_door_open = false;
            }
        }
        if (pushAmount > 0)
        {
            pushAmount -= Time.deltaTime * losingOverTimeAmount;
            if (pushAmount < 0)
            {
                pushAmount = 0;
            }
        }
        if (PushSlider != null && amountToOpen > 0 && pushAmount > 0)
        {
            PushSlider.gameObject.SetActive(true);
            PushSlider.value = pushAmount;
        }
        else if (PushSlider != null)
        {
            PushSlider.gameObject.SetActive(false);
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
