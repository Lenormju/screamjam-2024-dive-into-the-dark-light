using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    public Slider PushSlider;
    [SerializeField] private float amountToOpen = 0;
    private float pushAmount = 0;
    [SerializeField] private float amountOnClick = 10;
    [SerializeField] private float losingOverTimeAmount = 10;
    public AudioSource doorAudioSource;
    public AudioClip doorOpeningSound;
    public AudioClip doorClosingSound;
    public AudioClip doorLockedSound;


    [SerializeField] private int nb_keys_needed = 0;
    private bool is_in_range = false;
    private bool is_door_open = false;
    [SerializeField] private bool is_unlocked = false;
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
                if (is_unlocked)
                {
                    animator.SetTrigger("openingDoor");
                    is_door_open = true;
                    doorAudioSource.PlayOneShot(doorOpeningSound);
                }
                else if (GameManager.Instance.nb_keys < nb_keys_needed) {
                    //Debug.Log("pas assez de keys, still locked");
                    doorAudioSource.PlayOneShot(doorLockedSound);
                } 
                else if (pushAmount >= amountToOpen && GameManager.Instance.nb_keys >= nb_keys_needed)
                {
                    is_unlocked = true;
                    GameManager.Instance.RemoveKey(nb_keys_needed);
                    animator.SetTrigger("openingDoor");
                    is_door_open = true;
                    doorAudioSource.PlayOneShot(doorOpeningSound);
                }
                
            }
            else if (pushAmount <= 0)
            {
                //Debug.Log("closing");
                animator.SetTrigger("closingDoor");
                is_door_open = false;
                doorAudioSource.PlayOneShot(doorClosingSound);
            }
        }
        if (pushAmount > 0)  // losing push while time passes (QTE)
        {
            pushAmount -= Time.deltaTime * losingOverTimeAmount;
            if (pushAmount < 0)
            {
                pushAmount = 0;
            }
        }
        if (PushSlider != null && !is_door_open && amountToOpen > 0 && pushAmount > 0)
        {
            PushSlider.gameObject.SetActive(true);
            PushSlider.value = pushAmount;
            GameManager.Instance.StartBangingDoorMusic();
        }
        else if (PushSlider != null) // stop QTE
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
